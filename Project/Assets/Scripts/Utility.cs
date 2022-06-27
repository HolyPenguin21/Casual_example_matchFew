using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Utility
{
    public struct Coords
    {
        public int x { get; }
        public int y { get; }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static GameObject Get_Scene_CanvasObject(string name)
    {
        Canvas[] allCanvases = MonoBehaviour.FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in allCanvases)
            if (canvas.gameObject.name.Contains(name))
                return canvas.gameObject;

        Debug.LogError($"'{name}' Canvas object is not set as variable");
        return null;
    }

    public static Button Get_Scene_Button(Button someVar, string name)
    {
        if (someVar != null) return someVar;

        Button[] allButtons = MonoBehaviour.FindObjectsOfType<Button>();

        foreach (Button button in allButtons)
            if (button.gameObject.name.Contains(name))
                return button;

        Debug.LogError($"'{name}' Button is not set as variable");
        return null;
    }

    public static void Load_Scene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
