using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEvents
{
    public delegate void OnClickDown();
    public static event OnClickDown onClick_down;

    public delegate void OnDrag();
    public static event OnDrag onDrag;

    public delegate void OnRelease();
    public static event OnRelease onRelease;

    public static void Input_onClickDown()
    {
        onClick_down?.Invoke();
    }

    public static void Input_onDrag()
    {
        onDrag?.Invoke();
    }

    public static void Input_onRelease()
    {
        onRelease?.Invoke();
    }

    public static void Clear_Events()
    {
        onClick_down = null;
        onDrag = null;
        onRelease = null;
    }
}
