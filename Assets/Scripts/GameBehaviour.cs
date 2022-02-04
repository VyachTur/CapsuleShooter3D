using UnityEngine;

public class GameBehaviour : MonoBehaviour, IManager
{
    public const int maxItems = 6;
    private string labelText = $"Collect all {maxItems} items and win your freedom!";
    private bool showWinScreen = false;

    private int _itemsCollected = 0;

    public bool showLossScreen = false;

    private string _state;
     public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;

    public static void Print(string newText)
     {
        Debug.Log(newText);
     }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        Debug.Log(_state);

        HeroBehaviour playerBehavior = GameObject.Find("Hero").GetComponent<HeroBehaviour>();

        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }


    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;

            if (_itemsCollected >= maxItems)
            {
                ShowGuiInfo("You’ve found all the items!", 0f);
                showWinScreen = true;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    private int _playerHP = 12;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            if (_playerHP <= 0)
            {
                ShowGuiInfo("You want another life with that?", 0f);
                showLossScreen = true;
            }
            else
            {
                labelText = "Ouch... that’s got hurt.";
            }

            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    private void ShowGuiInfo(string labelText, float pause)
    {
        this.labelText = labelText;
        Time.timeScale = pause;
    }


    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);

        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (GUI.Button(new Rect(Screen.width - 120, 20, 100, 50), "Quit"))
        {
            Utilities.QuitGame();
        }

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel();
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }
}
