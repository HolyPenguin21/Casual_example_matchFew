using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    GridCell[] grid;

    int width;
    int heigth;

    Transform gridHolder;
    GameObject cellPrefab;

    public GameGrid(int width, int height)
    {
        this.width = width;
        this.heigth = height;

        Setup_GridHolder();
        Setup_CellPrefab();

        Create_Grid();
        Assign_Neighbors();
        Assign_CellItems();

        CenterGrid();
    }

    #region Setup
    private void Setup_GridHolder()
    {
        gridHolder = new GameObject("GridHolder").transform;
    }

    private void Setup_CellPrefab()
    {
        cellPrefab = Resources.Load("GameObjects/Cell", typeof(GameObject)) as GameObject;
    }

    private void Create_Grid()
    {
        int cellCount = width * heigth;
        grid = new GridCell[cellCount];

        int current = 0;
        for (int y = 0; y < heigth; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xPos = x * Utility.cellDist;
                float yPos = y * Utility.cellDist;

                Vector3 pos = new Vector3(xPos, yPos, 0);

                GameObject cell_obj = MonoBehaviour.Instantiate(cellPrefab, pos, Quaternion.identity, gridHolder);
                cell_obj.name = $"{x}-{y}";

                Utility.Coords coords = new Utility.Coords(x, y);
                GridCell cell = new GridCell(cell_obj, coords);

                grid[current] = cell;
                current++;
            }
        }
    }

    private void Assign_Neighbors()
    {
        foreach (GridCell currentCell in grid)
        {
            GridCell left = Get_GridCell_byCoords(currentCell.coords.x - 1, currentCell.coords.y);
            GridCell top = Get_GridCell_byCoords(currentCell.coords.x, currentCell.coords.y + 1);
            GridCell right = Get_GridCell_byCoords(currentCell.coords.x + 1, currentCell.coords.y);
            GridCell bottom = Get_GridCell_byCoords(currentCell.coords.x, currentCell.coords.y - 1);

            currentCell.Set_Neighbors(left, top, right, bottom);
        }
    }

    private void Assign_CellItems()
    {
        foreach (GridCell currentCell in grid)
        {
            int rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    currentCell.Set_Item(new Blue());
                    break;
                case 1:
                    currentCell.Set_Item(new Red());
                    break;
                case 2:
                    currentCell.Set_Item(new Yellow());
                    break;
            }
        }
    }

    private void CenterGrid()
    {
        float xValue = (5 - width) * (Utility.cellDist / 2);
        float yValue = (7 - heigth) * (Utility.cellDist / 2);

        gridHolder.position = new Vector3(xValue, yValue, 0);
    }
    #endregion

    #region Helpers
    private GridCell Get_GridCell_byCoords(int x, int y)
    {
        foreach (GridCell cell in grid)
        {
            if (cell.coords.x == x && cell.coords.y == y)
            {
                return cell;
            }
        }

        return null;
    }

    public GridCell Get_GridCell_byObj(GameObject obj)
    {
        foreach (GridCell cell in grid)
            if (cell.go == obj)
                return cell;

        Debug.LogError($"Cell with {obj} not found.");
        return null;
    }

    public GridCell[] Get_Grid()
    {
        return grid;
    }
    #endregion
}
