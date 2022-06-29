using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Input : MonoBehaviour
{
    private Camera sceneCamera;

    private Vector3 click_StartPos;
    private Vector3 click_CurrentPos;
    private Vector3 click_EndPos;

    private GridCell pickedCell;

    private void Awake()
    {
        sceneCamera = Camera.main;

        GlobalEvents.onClick_down += Get_ClickedCell;
        GlobalEvents.onClick_down += Setup_PickedCell;
        GlobalEvents.onClick_down += Get_StartPos;

        GlobalEvents.onDrag += Get_CurrentPos;
        GlobalEvents.onDrag += Move_PickedCell;

        GlobalEvents.onClick_up += Reset_Input;
    }

    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            GlobalEvents.Input_onClickDown();

        if(Input.GetKey(KeyCode.Mouse0))
            GlobalEvents.Input_onDrag();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            GlobalEvents.Input_onClickUp(pickedCell, Get_DragDirrection());
    }

    private void Get_ClickedCell()
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

    private void Get_StartPos()
    {
        if (pickedCell == null) return;

        click_StartPos = pickedCell.itemImage_tr.position;
    }

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

    private void Reset_Input(GridCell cell, Utility.Dirrection dirrection)
    {
        if (cell == null) return;

        cell.Drop();

        if (pickedCell == cell)
            pickedCell = null;
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

    private void OnDisable()
    {
        GlobalEvents.Clear_Events();
    }

    //private void OnDisable()
    //{
    //    GlobalEvents.onClick_down -= Get_StartPos;
    //    GlobalEvents.onClick_down -= Setup_PickedCell;
    //    GlobalEvents.onClick_down -= Get_ClickedCell;

    //    GlobalEvents.onDrag -= Move_PickedCell;
    //    GlobalEvents.onDrag -= Get_CurrentPos;

    //    GlobalEvents.onClick_up -= Reset_Input;
    //}

    //private void Input_PC()
    //{
    //    Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit clickHit;

    //    if (Physics.Raycast(ray, out clickHit, 20.0f))
    //    {
    //        if (clickHit.collider.CompareTag("Cell"))
    //        {
    //            Debug.Log($"Cell clicked.");
    //        }
    //    }
    //}

    //private void Input_Mobile()
    //{
    //    foreach (Touch touch in Input.touches)
    //    {
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            Ray ray = sceneCamera.ScreenPointToRay(touch.position);
    //            RaycastHit clickHit;

    //            if (Physics.Raycast(ray, out clickHit, 20f))
    //            {
    //                if (clickHit.collider.CompareTag("Cell"))
    //                {
    //                    Debug.Log($"Cell clicked.");
    //                }
    //            }
    //        }
    //    }
    //}
}
