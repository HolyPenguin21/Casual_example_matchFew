using UnityEngine;
using UnityEngine.UI;

public class Game_UIController : UI_Controller
{
    BurgerMenu burgerMenu;

    public Game_UIController(SceneLoader sceneLoader)
    {
        Setup_BurgerMenu(sceneLoader);
    }

    private void Setup_BurgerMenu(SceneLoader sceneLoader)
    {
        burgerMenu = new BurgerMenu();

        GameObject burgerButtonCanvas_obj = Get_Scene_CanvasObject("BurgerButton");
        burgerMenu.Set_BurgerButtonObject(burgerButtonCanvas_obj);

        Button burgerButton = Get_Scene_Button(null, "Burger");
        burgerMenu.Set_BurgerButton(burgerButton);

        GameObject burgerMenuCanvas_obj = Get_Scene_CanvasObject("BurgerMenu");
        burgerMenu.Set_BurgerMenuObject(burgerMenuCanvas_obj);

        Button quitToMenuButton = Get_Scene_Button(null, "QuitToMenu");
        burgerMenu.Set_QuitToMenuButton(quitToMenuButton, sceneLoader);

        Button quitGameButton = Get_Scene_Button(null, "QuitGame");
        burgerMenu.Set_QuitGameButton(quitGameButton, sceneLoader);

        burgerMenu.Close_Menu();
    }
}
