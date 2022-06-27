using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public GameObject go;

    public Utility.Coords coords;

    public GridCell left;
    public GridCell top;
    public GridCell right;
    public GridCell bottom;

    public Item item;

    public GridCell(GameObject go, Utility.Coords coords)
    {
        this.go = go;
        this.coords = coords;
    }

    public void Set_Neighbors(GridCell left, GridCell top, GridCell right, GridCell bottom)
    {
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
    }

    public void Set_Item(Item item)
    {
        this.item = item;
    }
}
