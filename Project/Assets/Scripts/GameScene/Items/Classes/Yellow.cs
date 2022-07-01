using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow : Item
{
    public Yellow()
    {
        sprite = Game_SceneController.instance.yellowItemImage;
        color = Color.yellow;
    }

    public override void Destroy()
    {
        Debug.Log("Yellow item destroyed");
    }
}
