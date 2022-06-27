using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public GridCell cell;
    public Sprite sprite;

    public void Set_ItemImage(Color color)
    {
        SpriteRenderer spriteRenderer = cell.go.transform.Find("Item").transform.Find("Item_Image").GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        if (Game_SceneController.instance.changeColor)
            spriteRenderer.color = color;
    }

    public abstract void Destroy();
}
