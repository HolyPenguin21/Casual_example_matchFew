using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker
{
    public MatchChecker()
    {
        Game_SceneController.instance.onRemove += Check_Match;
    }

    private IEnumerator Check_Match()
    {
        GridCell[] grid = Game_SceneController.instance.grid.Get_Grid();

        for (int i = 0; i < grid.Length; i++)
        {
            GridCell cell = grid[i];

            yield return HorizontalMatch(cell);
            yield return VerticalMatch(cell);
        }
    }

    private IEnumerator HorizontalMatch(GridCell cell)
    {
        if (!IsValidCell(cell)) yield break;

        List<GridCell> resultList = new List<GridCell>();
        resultList.Add(cell);

        if (IsValidCell(cell.n_left) && IsItemTypesTheSame(cell.item, cell.n_left.item))
            resultList.Add(cell.n_left);

        if (IsValidCell(cell.n_right) && IsItemTypesTheSame(cell.item, cell.n_right.item))
            resultList.Add(cell.n_right);

        yield return CheckResult_MatchThree(resultList);
    }

    private IEnumerator VerticalMatch(GridCell cell)
    {
        if (!IsValidCell(cell)) yield break;

        List<GridCell> resultList = new List<GridCell>();
        resultList.Add(cell);

        if (IsValidCell(cell.n_top) && IsItemTypesTheSame(cell.item, cell.n_top.item))
            resultList.Add(cell.n_top);

        if (IsValidCell(cell.n_bottom) && IsItemTypesTheSame(cell.item, cell.n_bottom.item))
            resultList.Add(cell.n_bottom);

        yield return CheckResult_MatchThree(resultList);
    }

    private IEnumerator CheckResult_MatchThree(List<GridCell> resultList)
    {
        if (resultList.Count < 3)
        {
            yield break;
        }

        foreach (GridCell matchingCells in resultList)
        {
            matchingCells.Remove_Item();
        }
    }

    private bool IsValidCell(GridCell cell)
    {
        if (cell == null) return false;
        if (cell.item == null) return false;
        return true;
    }

    private bool IsItemTypesTheSame(Item a, Item b)
    {
        System.Type a_type = a.GetType();
        System.Type b_type = b.GetType();

        if (a_type == b_type) return true;
        else return false;
    }
}
