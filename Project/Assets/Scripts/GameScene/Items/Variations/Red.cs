using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Item
{
    public Red()
    {
        sprite = Game_SceneController.instance.redItemImage;
        color = Color.red;
    }

    public override void Destroy()
    {
        Debug.Log("Red item destroyed");
    }
}
