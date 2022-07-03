using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Input : MonoBehaviour
{
    Camera sceneCamera;

    GridCell pickedCell;
    GridCell swapToCell;

    Vector3 pickedCellPos;
    Vector3 currentInputPos;

    Utility.Dirrection swapDirrection;

    private void Awake()
    {
        sceneCamera = Camera.main;

        Game_SceneController.instance.onClick_down += Set_PickedCell;
        Game_SceneController.instance.onClick_down += Set_PickedCellPos;
        Game_SceneController.instance.onClick_down += Setup_PickedCell;

        Game_SceneController.instance.onDrag += Set_CurrentPos;
        Game_SceneController.instance.onDrag += Move_PickedCell;

        Game_SceneController.instance.onRelease += Set_SwapDirrection;
        Game_SceneController.instance.onRelease += Set_SwapToCell;

        Game_SceneController.instance.onFalseSwap += Reset_InputVars;

        Game_SceneController.instance.onTrueSwap += SwapCells;
        Game_SceneController.instance.onTrueSwap += Reset_InputVars;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Game_SceneController.instance.Input_onClickDown();
        }

        if (Input.GetKey(KeyCode.Mouse0) && pickedCell != null)
        {
            Game_SceneController.instance.Input_onDrag();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Game_SceneController.instance.Input_onRelease();

            if (IsCellsSwapped())
            {
                Game_SceneController.instance.Input_onTrueSwap();
                Game_SceneController.instance.MatchChecker_onRemove();
            }
            else
            {
                Game_SceneController.instance.Input_onFalseSwap();
            }
        }
    }

    #region On click
    private void Set_PickedCell()
    {
        GameObject cell_obj = Get_ClickedObject();
        pickedCell = Game_SceneController.instance.grid.Get_GridCell_byObj(cell_obj);
    }

    private void Set_PickedCellPos()
    {
        if (pickedCell == null) return;

        pickedCellPos = pickedCell.itemImage_tr.position;
    }

    private void Setup_PickedCell()
    {
        if (pickedCell == null) return;

        pickedCell.PickUp();
    }
    #endregion

    #region On hold
    private void Set_CurrentPos()
    {
        currentInputPos = Get_InputWorldPos();
    }

    private void Move_PickedCell()
    {
        float x_dif = Mathf.Abs(pickedCellPos.x - currentInputPos.x);
        float y_dif = Mathf.Abs(pickedCellPos.y - currentInputPos.y);

        Vector3 movePos;
        if (x_dif > y_dif)
            movePos = new Vector3(currentInputPos.x, pickedCellPos.y, 0);
        else
            movePos = new Vector3(pickedCellPos.x, currentInputPos.y, 0);

        pickedCell.itemImage_tr.position = movePos;
    }
    #endregion

    #region On release
    private void Set_SwapDirrection()
    {
        Game_SceneController scene = Game_SceneController.instance;
        Utility.Dirrection dirrection = Utility.Dirrection.none;

        float x_value = pickedCellPos.x - currentInputPos.x;
        float y_value = pickedCellPos.y - currentInputPos.y;

        float x_dif = Mathf.Abs(x_value);
        float y_dif = Mathf.Abs(y_value);

        if (x_dif > y_dif)
        {
            if (x_value < 0 && x_value < -scene.Get_CellDistance() / 2)
            {
                dirrection = Utility.Dirrection.right;
            }
            else if (x_value > 0 && x_value > scene.Get_CellDistance() / 2)
            {
                dirrection = Utility.Dirrection.left;
            }
        }
        else
        {
            if (y_value < 0 && y_value < -scene.Get_CellDistance() / 2)
            {
                dirrection = Utility.Dirrection.top;
            }
            else if (y_value > 0 && y_value > scene.Get_CellDistance() / 2)
            {
                dirrection = Utility.Dirrection.bottom;
            }
        }

        swapDirrection = dirrection;
    }

    private void Set_SwapToCell()
    {
        switch (swapDirrection)
        {
            case Utility.Dirrection.left:
                if (pickedCell.n_left != null)
                    swapToCell = pickedCell.n_left;
                break;
            case Utility.Dirrection.top:
                if (pickedCell.n_top != null)
                    swapToCell = pickedCell.n_top;
                break;
            case Utility.Dirrection.right:
                if (pickedCell.n_right != null)
                    swapToCell = pickedCell.n_right;
                break;
            case Utility.Dirrection.bottom:
                if (pickedCell.n_bottom != null)
                    swapToCell = pickedCell.n_bottom;
                break;
            case Utility.Dirrection.none:
                swapToCell = null;
                break;
        }
    }

    private void SwapCells()
    {
        Item tempItem = swapToCell.item;

        swapToCell.Set_Item(pickedCell.item);
        pickedCell.Set_Item(tempItem);
    }

    private void Reset_InputVars()
    {
        if(pickedCell != null)
            pickedCell.Drop();

        pickedCell = null;
        swapToCell = null;
        swapDirrection = Utility.Dirrection.none;
    }
    #endregion

    #region Helpers
    private Vector3 Get_InputWorldPos()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit clickHit;

        if (Physics.Raycast(ray, out clickHit, 50.0f))
        {
            return clickHit.point;
        }

        return Vector3.zero;
    }

    private GameObject Get_ClickedObject()
    {
        if (sceneCamera == null)
            sceneCamera = Camera.main;

        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit clickHit;

        if (Physics.Raycast(ray, out clickHit, 50.0f))
        {
            if (clickHit.collider.CompareTag("Cell"))
                return clickHit.collider.gameObject;
        }

        return null;
    }

    private bool IsCellsSwapped()
    {
        if (pickedCell == null || pickedCell.item == null)
            return false;

        if (swapDirrection == Utility.Dirrection.none)
            return false;

        if (swapToCell == null)
            return false;

        return true;
    }
    #endregion
}
