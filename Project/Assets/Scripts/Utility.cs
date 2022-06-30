using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Utility
{
    public enum Dirrection {none, left, top, right, bottom};

    public struct Coords
    {
        public int x { get; }
        public int y { get; }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    // Move into new SceneManager class
    public static void Load_Scene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
