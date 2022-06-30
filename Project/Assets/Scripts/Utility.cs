using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Utility
{
    public static float cellDist = 1.5f;

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

    public static void Load_Scene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void Set_Framerate(int fps)
    {
        Application.targetFrameRate = fps;
    }
}
