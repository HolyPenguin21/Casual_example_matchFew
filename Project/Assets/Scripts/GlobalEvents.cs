using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEvents
{
    public delegate void OnClickDown();
    public static event OnClickDown onClick_down;

    public static void Input_onClickDown()
    {
        onClick_down?.Invoke();
    }

    public delegate void OnDrag();
    public static event OnDrag onDrag;

    public static void Input_onDrag()
    {
        onDrag?.Invoke();
    }

    public delegate void OnClickUp(GridCell cell, Utility.Dirrection dirrection);
    public static event OnClickUp onClick_up;

    public static void Input_onClickUp(GridCell cell, Utility.Dirrection dirrection)
    {
        onClick_up?.Invoke(cell, dirrection);
    }

    public static void Clear_Events()
    {
        onClick_down = null;
        onDrag = null;
        onClick_up = null;
    }
}
