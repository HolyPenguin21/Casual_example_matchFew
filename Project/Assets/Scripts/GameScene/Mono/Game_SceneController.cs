using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_SceneController : MonoBehaviour
{
    public static Game_SceneController instance;

    [Header("Grid settings :")]
    [SerializeField] [Range(3, 5)] private int width = 5;
    [SerializeField] [Range(3, 7)] private int height = 7;
    private float cellDist = 1.5f;

    [Header("Item settings :")]
    public bool changeColor = true;
    public Sprite blueItemImage;
    public Sprite redItemImage;
    public Sprite yellowItemImage;

    [HideInInspector] public GameGrid grid;
    BurgerMenu burgerMenu;
    MatchChecker matchChecker;

    private void Awake()
    {
        Set_AsSingletone();
        Check_ItemImages();

        grid = new GameGrid(width, height, cellDist);
        burgerMenu = new BurgerMenu();
        matchChecker = new MatchChecker();
    }

    private void Check_ItemImages()
    {
        if(blueItemImage == null)
            blueItemImage = Resources.Load<Sprite>("Items/unity_1");

        if (redItemImage == null)
            redItemImage = Resources.Load<Sprite>("Items/unity_1");

        if (yellowItemImage == null)
            yellowItemImage = Resources.Load<Sprite>("Items/unity_1");
    }

    public float Get_CellDistance()
    {
        return cellDist;
    }

    private void Set_AsSingletone()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        GlobalEvents.Clear_Events();
    }
}
