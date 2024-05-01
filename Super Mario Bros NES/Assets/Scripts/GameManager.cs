using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // This is to create a singleton of this GameManager
    // Singletons are classes that can have only one instance of it in the entire game's lifetime and are publicly accessible by any game object
    public static GameManager instance { get; private set; }

    public int world { get; private set; }
    public int level { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            // If an instance of this GameManager is already there, then destroy this instance immediately and use that previous instance
            DestroyImmediate(gameObject);
        }
        else
        {
            // If an instance of this GameManager is not there, then set the instance to this class
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            // When this GameManager object is destroyed, set the instance to null
            // When this game object becomes active again, another instance of it will be created by the definitions under Awake() where it reassigns itself
            instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    private void NewGame()
    {
        lives = 5;
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int level)
    {
        // this.world is the global world variable that is defined in this class on the very first above
        // This is done so that there won't be any name clash with the parameters of this function
        this.world = world;
        this.level = level;

        // String formatting to load appropriate levels
        // This works provided that the names of all scenes are pre-defined in the same format
        SceneManager.LoadScene($"{world}-{level}");

    }

    public void NextLevel()
    {
        if (level == 4)
        {
            level = 1;
            LoadLevel(world + 1, level);
        }
        else
        {
            LoadLevel(world, level + 1);
        }
        
    }


    // Overloading MarioDeath (or ResetLevel) using a float variable "delay" to make it not reset the level instantly when Mario dies
    public void MarioDeath(float delay)
    {
        Invoke(nameof(MarioDeath), delay);
    }


    // Resets level after Mario dies
    public void MarioDeath()
    {
        lives--;

        if (lives > 0)
        {
            LoadLevel(world, level);
        }
        else
        {
            GameOver();    
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");

        // To delay a new game for 3 seconds
        Invoke(nameof(NewGame), 3f);
    }

    public void AddCoin()
    {
        coins++;

        if (coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void AddLife()
    {
        lives++;
    }


}
