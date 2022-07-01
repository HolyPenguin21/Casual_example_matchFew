using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void Load_Scene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void QuitGame()
    {
        // Save something before quit
        Application.Quit();
    }
}
