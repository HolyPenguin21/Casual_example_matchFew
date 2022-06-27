using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Item
{
    public Red(GridCell cell)
    {
        this.cell = cell;
        sprite = Game_SceneController.instance.redItemImage;

        Set_ItemImage(Color.red);
    }

    public override void Destroy()
    {
        Debug.Log("Red item destroyed");
    }
}
