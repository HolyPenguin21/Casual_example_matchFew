using UnityEngine;
using UnityEngine.UI;

public class BurgerMenu
{
    GameObject buttonCanvas_obj, menuCanvas_obj;
    Button burgerButton, quitToMenu_button, quitGame_button;

    #region Menu actions
    void OpenCloseMenu()
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

    void Open_Menu()
    {
        menuCanvas_obj.SetActive(true);
    }

    public void Close_Menu()
    {
        menuCanvas_obj.SetActive(false);
    }
    #endregion

    #region Setup

    #region Burger button
    public void Set_BurgerButtonObject(GameObject obj)
    {
        buttonCanvas_obj = obj;

        if (buttonCanvas_obj == null)
        {
            buttonCanvas_obj = MonoBehaviour.Instantiate(Resources.Load("UI/BurgerMenu/BurgerButton_Canvas", typeof(GameObject))) as GameObject;
        }
    }

    public void Set_BurgerButton(Button button)
    {
        burgerButton = button;
        burgerButton.onClick.AddListener(OpenCloseMenu);
    }
    #endregion

    #region Burger menu
    public void Set_BurgerMenuObject(GameObject obj)
    {
        menuCanvas_obj = obj;

        if (menuCanvas_obj == null)
        {
            menuCanvas_obj = MonoBehaviour.Instantiate(Resources.Load("UI/BurgerMenu/BurgerMenu_Canvas", typeof(GameObject))) as GameObject;
        }
    }

    public void Set_QuitToMenuButton(Button button, SceneLoader sceneLoader)
    {
        quitToMenu_button = button;
        quitToMenu_button.onClick.AddListener(() => sceneLoader.Load_Scene(0));
    }

    public void Set_QuitGameButton(Button button, SceneLoader sceneLoader)
    {
        quitGame_button = button;
        quitGame_button.onClick.AddListener(sceneLoader.QuitGame);
    }
    #endregion

    #endregion
}
