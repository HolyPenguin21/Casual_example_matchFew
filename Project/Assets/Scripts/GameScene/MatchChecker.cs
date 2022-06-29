using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker
{
    public MatchChecker()
    {
        GlobalEvents.onRelease += Check_Match;
    }

    private void Check_Match()
    {
        foreach (GridCell cell in Game_SceneController.instance.grid.Get_Grid())
        {
            HorizontalMatch(cell);
            VerticalMatch(cell);
        }
    }

    private void HorizontalMatch(GridCell cell)
    {
        if (cell.item == null) return;

        List<GridCell> tempList = new List<GridCell>();
        tempList.Add(cell);

        var cell_ItemType = cell.item.GetType();

        if (IsValidCell(cell.n_left))
            if (cell_ItemType == cell.n_left.item.GetType())
            {
                tempList.Add(cell.n_left);
            }

        if (IsValidCell(cell.n_right))
            if (cell_ItemType == cell.n_right.item.GetType())
            {
                tempList.Add(cell.n_right);
            }

        if (tempList.Count >= 3)
        {
            foreach (GridCell matchingCells in tempList)
                matchingCells.Remove_Item();
        }
    }

    private void VerticalMatch(GridCell cell)
    {
        if (cell.item == null) return;

        List<GridCell> tempList = new List<GridCell>();
        tempList.Add(cell);

        var cell_ItemType = cell.item.GetType();

        if (IsValidCell(cell.n_top))
            if (cell_ItemType == cell.n_top.item.GetType())
            {
                tempList.Add(cell.n_top);
            }

        if (IsValidCell(cell.n_bottom))
            if (cell_ItemType == cell.n_bottom.item.GetType())
            {
                tempList.Add(cell.n_bottom);
            }

        if (tempList.Count >= 3)
        {
            foreach (GridCell matchingCells in tempList)
                matchingCells.Remove_Item();
        }
    }

    private bool IsValidCell(GridCell cell)
    {
        if (cell == null) return false;
        if (cell.item == null) return false;
        return true;
    }
}
