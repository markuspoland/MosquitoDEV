using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameManager).Name;
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }

    public enum GameScene
    {
        Preload,
        Intro,
        Menu,
        TheRoom
    }

    string currentScene;
    public static float preloadTime = 5f;

    public int levelCoinPoints;
    public float levelTime;
    public int levelRevivesCount;
    public int ObjectivesCompleted;
    public string levelTimerText;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        currentScene = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(GameScene gameScene)
    {
        SceneManager.LoadScene(gameScene.ToString());
    }

    
}
