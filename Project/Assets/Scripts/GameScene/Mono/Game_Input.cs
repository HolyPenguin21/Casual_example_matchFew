using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Input : MonoBehaviour
{
    Camera sceneCamera;

    Vector3 click_StartPos;
    Vector3 click_CurrentPos;
    GridCell pickedCell;

    Utility.Dirrection swapDirrection;

    private void Awake()
    {
        sceneCamera = Camera.main;

        Game_SceneController.instance.onClick_down += Get_PickedCell;
        Game_SceneController.instance.onClick_down += Get_StartPos;
        Game_SceneController.instance.onClick_down += Setup_PickedCell;

        Game_SceneController.instance.onDrag += Get_CurrentPos;
        Game_SceneController.instance.onDrag += Move_PickedCell;

        //Game_SceneController.instance.onRelease += Drop_PickedCell;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Game_SceneController.instance.Input_onClickDown();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Game_SceneController.instance.Input_onDrag();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnRelease();
        }
    }

    #region On click
    private void Get_PickedCell()
    {
        GameObject cell_obj = Get_ClickedObject();
        pickedCell = Game_SceneController.instance.grid.Get_GridCell_byObj(cell_obj);
    }

    private void Get_StartPos()
    {
        if (pickedCell == null) return;

        click_StartPos = pickedCell.itemImage_tr.position;
    }

    private void Setup_PickedCell()
    {
        if (pickedCell == null) return;

        pickedCell.PickUp();
    }
    #endregion

    #region On hold
    private void Get_CurrentPos()
    {
        click_CurrentPos = Get_InputWorldPos();
    }

    private void Move_PickedCell()
    {
        if (pickedCell == null) return;

        float x_dif = Mathf.Abs(click_StartPos.x - click_CurrentPos.x);
        float y_dif = Mathf.Abs(click_StartPos.y - click_CurrentPos.y);

        Vector3 movePos;
        if (x_dif > y_dif)
            movePos = new Vector3(click_CurrentPos.x, click_StartPos.y, 0);
        else
            movePos = new Vector3(click_StartPos.x, click_CurrentPos.y, 0);

        pickedCell.itemImage_tr.position = movePos;
    }
    #endregion

    #region On release
    // Test
    private void OnRelease()
    {
        if (pickedCell == null || pickedCell.item == null)
            return;

        swapDirrection = Get_SwapDirrection();
        if (swapDirrection == Utility.Dirrection.none)
        {
            Drop_PickedCell();
        }
        else
        {
            SwapCells();
            Drop_PickedCell();
            //MatchChecker.Check_Match();
            //StartCoroutine(DestroyAnimation);
        }
    }

    private void SwapCells()
    {
        GridCell secondCell = null;
        switch (swapDirrection)
        {
            case Utility.Dirrection.left:
                if (pickedCell.n_left != null)
                    secondCell = pickedCell.n_left;
                break;
            case Utility.Dirrection.top:
                if (pickedCell.n_top != null)
                    secondCell = pickedCell.n_top;
                break;
            case Utility.Dirrection.right:
                if (pickedCell.n_right != null)
                    secondCell = pickedCell.n_right;
                break;
            case Utility.Dirrection.bottom:
                if (pickedCell.n_bottom != null)
                    secondCell = pickedCell.n_bottom;
                break;
        }

        if (secondCell == null)
        {
            Drop_PickedCell();
            return;
        }

        Item tempItem = secondCell.item;
        secondCell.Set_Item(pickedCell.item);
        pickedCell.Set_Item(tempItem);
    }

    private void Drop_PickedCell()
    {
        pickedCell.Drop();
        pickedCell = null;
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

    private Utility.Dirrection Get_SwapDirrection()
    {
        Game_SceneController scene = Game_SceneController.instance;
        Utility.Dirrection dirrection = Utility.Dirrection.none;

        float x_value = click_StartPos.x - click_CurrentPos.x;
        float y_value = click_StartPos.y - click_CurrentPos.y;

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

        return dirrection;
    }
    #endregion
}
