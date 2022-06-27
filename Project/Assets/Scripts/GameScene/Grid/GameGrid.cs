using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    GridCell[] grid;

    int width;
    int heigth;

    float cellDist = 1.5f;

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

    private void Create_Grid()
    {
        int cellCount = width * heigth;
        grid = new GridCell[cellCount];

        int current = 0;
        for (int y = 0; y < heigth; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xPos = x * cellDist;
                float yPos = y * cellDist;

                Vector3 pos = new Vector3(xPos, yPos, 0);

                GameObject cell_obj = MonoBehaviour.Instantiate(cellPrefab, pos, Quaternion.identity, gridHolder);
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
                    currentCell.item = new Blue(currentCell);
                    break;
                case 1:
                    currentCell.item = new Red(currentCell);
                    break;
                case 2:
                    currentCell.item = new Yellow(currentCell);
                    break;
            }
        }
    }

    private void CenterGrid()
    {
        float xValue = (5 - width) * (cellDist / 2);
        float yValue = (7 - heigth) * (cellDist / 2);

        gridHolder.position = new Vector3(xValue, yValue, 0);
    }

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

    #region Setup
    private void Setup_GridHolder()
    {
        gridHolder = new GameObject("GridHolder").transform;
    }

    private void Setup_CellPrefab()
    {
        cellPrefab = Resources.Load("GameObjects/Cell", typeof(GameObject)) as GameObject;
    }
    #endregion
}
