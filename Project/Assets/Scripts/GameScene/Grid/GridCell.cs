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
    public Transform itemImage_tr;
    public SpriteRenderer itemImage_rend;

    public GridCell(GameObject go, Utility.Coords coords)
    {
        this.go = go;
        this.coords = coords;

        itemImage_tr = this.go.transform.Find("Item").transform.Find("Item_Image");
        itemImage_rend = itemImage_tr.GetComponent<SpriteRenderer>();
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

    public void PickUp()
    {
        Set_RenderValue(1);
        Set_ScaleValue(0.1f);
    }

    public void Drop()
    {
        Set_RenderValue(-1);
        Set_ScaleValue(-0.1f);

        itemImage_tr.localPosition = Vector3.zero;
    }

    private void Set_RenderValue(int value)
    {
        itemImage_rend.sortingOrder += value;
    }

    private void Set_ScaleValue(float value)
    {
        itemImage_tr.localScale = new Vector3(itemImage_tr.localScale.x + value, itemImage_tr.localScale.y + value, 0);
    }
}
