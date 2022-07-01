using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    public void Set_Framerate(int fps)
    {
        Application.targetFrameRate = fps;
    }
}
