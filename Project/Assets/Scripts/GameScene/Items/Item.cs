using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public Sprite sprite;
    public Color color;

    public abstract void Destroy();
}
