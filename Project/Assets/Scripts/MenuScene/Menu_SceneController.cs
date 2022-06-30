using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SceneController : MonoBehaviour
{
    public Button startGame_button;
    public Button quitGame_button;

    private void Awake()
    {
        Setup_StartButton();
        Setup_QuitButton();

        Utility.Set_Framerate(30);
    }

    private void Setup_StartButton()
    {
        startGame_button = Utility_UI.Get_Scene_Button(startGame_button, "Start");
        startGame_button.onClick.AddListener(Button_StartGame);
    }

    private void Setup_QuitButton()
    {
        quitGame_button = Utility_UI.Get_Scene_Button(quitGame_button, "Quit");
        quitGame_button.onClick.AddListener(Utility.QuitGame);
    }

    public void Button_StartGame()
    {
        Utility.Load_Scene(1);
    }
}
