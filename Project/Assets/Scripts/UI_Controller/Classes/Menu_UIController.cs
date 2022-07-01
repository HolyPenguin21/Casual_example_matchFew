using UnityEngine.UI;

public class Menu_UIController : UI_Controller
{
    Button startGame_button, quitGame_button;

    public void Setup_StartButton(SceneLoader sceneLoader)
    {
        startGame_button = Get_Scene_Button(startGame_button, "Start");
        startGame_button.onClick.AddListener(() => sceneLoader.Load_Scene(1));
    }

    public void Setup_QuitButton(SceneLoader sceneLoader)
    {
        quitGame_button = Get_Scene_Button(quitGame_button, "Quit");
        quitGame_button.onClick.AddListener(sceneLoader.QuitGame);
    }
}
