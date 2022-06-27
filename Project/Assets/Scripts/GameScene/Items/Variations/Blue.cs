using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Item
{
    public Blue(GridCell cell)
    {
        this.cell = cell;
        sprite = Game_SceneController.instance.blueItemImage;

        Set_ItemImage(Color.blue);
    }

    public override void Destroy()
    {
        Debug.Log("Blue item destroyed");
    }
}
