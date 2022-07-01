using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Controller
{
    public GameObject Get_Scene_CanvasObject(string name)
    {
        Canvas[] allCanvases = MonoBehaviour.FindObjectsOfType<Canvas>();

        for (int i = 0; i < allCanvases.Length; i++)
        {
            Canvas canvas = allCanvases[i];

            if (canvas.gameObject.name.Contains(name))
                return canvas.gameObject;
        }

        Debug.Log($"'{name}' Canvas object is not set as variable");
        return null;
    }

    public Button Get_Scene_Button(Button someVar, string name)
    {
        if (someVar != null) return someVar;

        Button[] allButtons = MonoBehaviour.FindObjectsOfType<Button>();

        for (int i = 0; i < allButtons.Length; i++)
        {
            Button button = allButtons[i];

            if (button.gameObject.name.Contains(name))
                return button;
        }

        Debug.Log($"'{name}' Button is not set as variable");
        return null;
    }
}
