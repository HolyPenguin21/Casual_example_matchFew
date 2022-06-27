using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow : Item
{
    public Yellow(GridCell cell)
    {
        this.cell = cell;
        sprite = Game_SceneController.instance.yellowItemImage;

        Set_ItemImage(Color.yellow);
    }

    public override void Destroy()
    {
        Debug.Log("Yellow item destroyed");
    }
}
