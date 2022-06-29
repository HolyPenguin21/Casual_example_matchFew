using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Input : MonoBehaviour
{
    private Camera sceneCamera;

    private Vector3 click_StartPos;
    private Vector3 click_CurrentPos;

    private GridCell pickedCell;

    private void Awake()
    {
        sceneCamera = Camera.main;

        GlobalEvents.onClick_down += Set_PickedCell;
        GlobalEvents.onClick_down += Setup_PickedCell;
        GlobalEvents.onClick_down += Get_StartPos;

        GlobalEvents.onDrag += Get_CurrentPos;
        GlobalEvents.onDrag += Move_PickedCell;

        GlobalEvents.onRelease += Drop_PickedCell;
    }

    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GlobalEvents.Input_onClickDown();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            GlobalEvents.Input_onDrag();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SwapTiles(pickedCell, Get_DragDirrection());
            GlobalEvents.Input_onRelease();
        }
    }

    private void Set_PickedCell()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit clickHit;

        if (Physics.Raycast(ray, out clickHit, 50.0f))
        {
            if (clickHit.collider.CompareTag("Cell"))
            {
                pickedCell = Game_SceneController.instance.grid.Get_GridCell_byObj(clickHit.collider.gameObject);
            }
        }
    }

    private void Setup_PickedCell()
    {
        if (pickedCell == null) return;

        pickedCell.PickUp();
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

    private void SwapTiles(GridCell cell, Utility.Dirrection dirrection)
    {
        if (cell == null) return;
        if (cell.item == null) return;
        if (dirrection == Utility.Dirrection.none) return;

        GridCell secondCell = null;
        Item tempItem = null;

        switch (dirrection)
        {
            case Utility.Dirrection.left:
                if (cell.n_left != null)
                    secondCell = cell.n_left;
                break;
            case Utility.Dirrection.top:
                if (cell.n_top != null)
                    secondCell = cell.n_top;
                break;
            case Utility.Dirrection.right:
                if (cell.n_right != null)
                    secondCell = cell.n_right;
                break;
            case Utility.Dirrection.bottom:
                if (cell.n_bottom != null)
                    secondCell = cell.n_bottom;
                break;
        }

        if (secondCell == null) return;

        tempItem = secondCell.item;
        secondCell.Set_Item(cell.item);
        cell.Set_Item(tempItem);
    }

    private void Drop_PickedCell()
    {
        if (pickedCell == null) return;

        pickedCell.Drop();
        pickedCell = null;
    }

    private void Get_StartPos()
    {
        if (pickedCell == null) return;

        click_StartPos = pickedCell.itemImage_tr.position;
    }

    private void Get_CurrentPos()
    {
        click_CurrentPos = Get_InputWorldPos();
    }

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

    private Utility.Dirrection Get_DragDirrection()
    {
        Utility.Dirrection dirrection = Utility.Dirrection.none;

        float x_dif = Mathf.Abs(click_StartPos.x - click_CurrentPos.x);
        float y_dif = Mathf.Abs(click_StartPos.y - click_CurrentPos.y);

        float x_value = click_StartPos.x - click_CurrentPos.x;
        float y_value = click_StartPos.y - click_CurrentPos.y;

        if (x_dif > y_dif)
        {
            if (x_value < 0 && x_value < -Utility.cellDist / 2)
            {
                dirrection = Utility.Dirrection.right;
            }
            else if (x_value > 0 && x_value > Utility.cellDist / 2)
            {
                dirrection = Utility.Dirrection.left;
            }
        }
        else
        {
            if (y_value < 0 && y_value < -Utility.cellDist / 2)
            {
                dirrection = Utility.Dirrection.top;
            }
            else if (y_value > 0 && y_value > Utility.cellDist / 2)
            {
                dirrection = Utility.Dirrection.bottom;
            }
        }

        return dirrection;
    }
}
