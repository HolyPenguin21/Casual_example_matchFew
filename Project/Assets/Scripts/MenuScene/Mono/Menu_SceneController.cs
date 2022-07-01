using UnityEngine;

public class Menu_SceneController : MonoBehaviour
{
    GameSettings gameSettings;
    SceneLoader sceneLoader;
    Menu_UIController uIController;

    private void Awake()
    {
        gameSettings = new GameSettings();
        sceneLoader = new SceneLoader();
        uIController = new Menu_UIController();

        Set_GameSettings();
        Set_SceneUI();
    }

    #region Scene Setters
    private void Set_GameSettings()
    {
        if (gameSettings == null)
        {
            Debug.LogError("'GameSettings' component is missing.");
            return;
        }

        gameSettings.Set_Framerate(30);
    }

    private void Set_SceneUI()
    {
        if (uIController == null)
        {
            Debug.LogError("'UIController' component is missing.");
            return;
        }

        if (sceneLoader == null)
        {
            Debug.LogError("'SceneLoader' component is missing.");
            return;
        }

        uIController.Setup_StartButton(sceneLoader);
        uIController.Setup_QuitButton(sceneLoader);
    }
    #endregion
}
