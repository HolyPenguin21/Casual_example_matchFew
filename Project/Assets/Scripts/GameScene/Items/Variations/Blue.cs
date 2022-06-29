using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Item
{
    public Blue()
    {
        sprite = Game_SceneController.instance.blueItemImage;
        color = Color.blue;
    }

    public override void Destroy()
    {
        Debug.Log("Blue item destroyed");
    }
}
