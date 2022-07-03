using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    SceneLoader sceneLoader;
    Game_UIController uIController;
    MatchChecker matchChecker;

    public delegate void OnClickDown();
    public event OnClickDown onClick_down;

    public delegate void OnDrag();
    public event OnDrag onDrag;

    public delegate void OnRelease();
    public event OnRelease onRelease;

    public delegate void OnFalseSwap();
    public event OnFalseSwap onFalseSwap;

    public delegate void OnTrueSwap();
    public event OnTrueSwap onTrueSwap;

    public delegate IEnumerator OnRemove();
    public event OnRemove onRemove;

    private void Awake()
    {
        Set_AsSingletone();
        Check_ItemImages();

        sceneLoader = new SceneLoader();
        uIController = new Game_UIController(sceneLoader);
        grid = new GameGrid(width, height, cellDist);
        matchChecker = new MatchChecker();
    }

    #region Events
    public void Input_onClickDown()
    {
        onClick_down?.Invoke();
    }

    public void Input_onDrag()
    {
        onDrag?.Invoke();
    }

    public void Input_onRelease()
    {
        onRelease?.Invoke();
    }

    public void Input_onFalseSwap()
    {
        onFalseSwap?.Invoke();
    }

    public void Input_onTrueSwap()
    {
        onTrueSwap?.Invoke();
    }

    public void MatchChecker_onRemove()
    {
        StartCoroutine(onRemove?.Invoke());
    }
    #endregion

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
        onClick_down = null;
        onDrag = null;
        onRelease = null;
        onFalseSwap = null;
        onTrueSwap = null;
        onRemove = null;

        StopAllCoroutines();
    }
}
