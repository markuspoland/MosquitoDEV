using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        TheRoom,
        End,
        LastADScene
    }

    string currentScene;
    public static float preloadTime = 5f;

    public int levelCoinPoints;
    public float levelTime;
    public int timeBonus;
    public int levelRevivesCount;
    public int reviveBonus;
    public int ObjectivesCompleted;
    public int objectivePoints;
    public string levelTimerText;

    public int level1Points;
    public int level2Points;
    public int level3Points;
    public int tempHighscore;
    public int highscore;

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

    public void UpdateHighscore()
    {
        tempHighscore = level1Points + level2Points + level3Points;

        if (tempHighscore > highscore)
        {
            highscore = tempHighscore;
            tempHighscore = 0;
        }
    }
    
    public void SaveScore()
    {
        string filePath = Application.persistentDataPath + "/mosscr.scr";
        BinaryFormatter bf = new BinaryFormatter();
        SaveData saveData = new SaveData();
        saveData.score = highscore;
        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
        bf.Serialize(fs, saveData);
        fs.Close();
    }

    public void LoadScore()
    {
        
        string filePath = Application.persistentDataPath + "/mosscr.scr";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath, FileMode.Open);
            SaveData saveData = new SaveData();
            saveData = bf.Deserialize(fs) as SaveData;
            fs.Close();
            highscore = saveData.score;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int score;
}