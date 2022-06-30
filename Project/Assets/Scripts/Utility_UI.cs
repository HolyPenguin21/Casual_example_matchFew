using UnityEngine;
using UnityEngine.UI;

public static class Utility_UI
{
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
}
