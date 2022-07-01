using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Controller
{
    public GameObject Get_Scene_CanvasObject(string name)
    {
        Canvas[] allCanvases = MonoBehaviour.FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in allCanvases)
            if (canvas.gameObject.name.Contains(name))
                return canvas.gameObject;

        Debug.Log($"'{name}' Canvas object is not set as variable");
        return null;
    }

    public Button Get_Scene_Button(Button someVar, string name)
    {
        if (someVar != null) return someVar;

        Button[] allButtons = MonoBehaviour.FindObjectsOfType<Button>();

        foreach (Button button in allButtons)
            if (button.gameObject.name.Contains(name))
                return button;

        Debug.Log($"'{name}' Button is not set as variable");
        return null;
    }
}
