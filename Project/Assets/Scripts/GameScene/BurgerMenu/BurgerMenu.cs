using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerMenu
{
    GameObject buttonCanvas_obj;
    GameObject menuCanvas_obj;
    Button burgerButton;
    Button quitToMenu_button;
    Button quitGame_button;

    public BurgerMenu()
    {
        Setup_BurgerButton();
        Setup_BurgerMenu();

        Close_Menu();
    }

    #region Menu actions
    private void OpenCloseMenu()
    {
        if (menuCanvas_obj.activeInHierarchy)
        {
            Close_Menu();
        }
        else
        {
            Open_Menu();
        }
    }

    private void Open_Menu()
    {
        menuCanvas_obj.SetActive(true);
    }

    private void Close_Menu()
    {
        menuCanvas_obj.SetActive(false);
    }
    #endregion

    #region Setup
    private void Setup_BurgerButton()
    {
        buttonCanvas_obj = Utility_UI.Get_Scene_CanvasObject("BurgerButton");
        if (buttonCanvas_obj == null)
        {
            buttonCanvas_obj = MonoBehaviour.Instantiate(Resources.Load("UI/BurgerMenu/BurgerButton_Canvas", typeof(GameObject))) as GameObject;
        }

        burgerButton = Utility_UI.Get_Scene_Button(burgerButton, "Burger");
        burgerButton.onClick.AddListener(OpenCloseMenu);
    }

    private void Setup_BurgerMenu()
    {
        menuCanvas_obj = Utility_UI.Get_Scene_CanvasObject("BurgerMenu");
        if (menuCanvas_obj == null)
        {
            menuCanvas_obj = MonoBehaviour.Instantiate(Resources.Load("UI/BurgerMenu/BurgerMenu_Canvas", typeof(GameObject))) as GameObject;
        }

        Setup_QuitToMenuButton();
        Setup_QuitButton();
    }

    private void Setup_QuitToMenuButton()
    {
        quitToMenu_button = Utility_UI.Get_Scene_Button(quitToMenu_button, "QuitToMenu");
        quitToMenu_button.onClick.AddListener(() => Utility.Load_Scene(0));
    }

    private void Setup_QuitButton()
    {
        quitGame_button = Utility_UI.Get_Scene_Button(quitGame_button, "QuitGame");
        quitGame_button.onClick.AddListener(Utility.QuitGame);
    }
    #endregion
}
