using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static List<Image> lives;
    public List<string> level1Objectives;
    public GameObject objectiveNotification;
    Image objectiveNotificationImage;
    public TextMeshProUGUI objectiveNotificationText;
    public List<Image> livesImage;
    static Animator livesAnim;
    AudioSource audioSource;
    [SerializeField] AudioClip objectiveSound;
    [SerializeField] AudioClip statSound;
    [SerializeField] AudioClip scoreSound;
    [SerializeField] GameObject levelComplete;
    [SerializeField] GameObject BonusStat;
    [SerializeField] GameObject ObjectiveStat;
    [SerializeField] GameObject objectivePoints;
    [SerializeField] GameObject ReviveStat;
    [SerializeField] GameObject revivePoints;
    [SerializeField] GameObject levelTimer;
    [SerializeField] GameObject timeBonus;
    [SerializeField] GameObject levelScore;
    [SerializeField] GameObject highscore;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject continueGame;
    Timer timer;

    string currentLevel;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        objectiveNotificationText.text = null;
        objectiveNotification.SetActive(false);
        lives = livesImage;

        levelComplete.SetActive(false);
        BonusStat.SetActive(false);
        ObjectiveStat.SetActive(false);
        objectivePoints.SetActive(false);
        ReviveStat.SetActive(false);
        revivePoints.SetActive(false);
        levelTimer.SetActive(false);
        timeBonus.SetActive(false);
        levelScore.SetActive(false);
        highscore.SetActive(false);
        menu.SetActive(false);
        continueGame.SetActive(false);
    }

    void Start()
    {
        GameManager.Instance.LoadScore();
        GameManager.Instance.levelCoinPoints = 0;
        GameManager.Instance.levelRevivesCount = 0;
        GameManager.Instance.reviveBonus = 80;
        GameManager.Instance.timeBonus = 0;
        GameManager.Instance.ObjectivesCompleted = 0;
        GameManager.Instance.objectivePoints = 0;
        timer = FindObjectOfType<Timer>();
        currentLevel = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public static void TakeLife(int liveId)
    {
        livesAnim = lives[liveId].GetComponent<Animator>();
        livesAnim.SetTrigger("Died");
    }

    public void CompleteLevel1Objective (string obj)
    {
        objectiveNotification.SetActive(true);

        if (level1Objectives.Count != 0)
        {
            foreach (string objective in level1Objectives.ToArray())
            {
                if (obj.Equals(objective))
                {
                    audioSource.PlayOneShot(objectiveSound);
                    objectiveNotificationText.text = obj;
                    int index = GetObjectiveName(obj, level1Objectives);
                    level1Objectives.RemoveAt(index);
                    StartCoroutine(DisableObjectiveNotification());
                    GameManager.Instance.ObjectivesCompleted++;
                    GameManager.Instance.objectivePoints += 50;
                }
            }
        } else
        {
            return;
        }
    }

    private int GetObjectiveName(string name, List<string> objectives)
    {
        int index = objectives.FindIndex(s => string.Equals(s, name, StringComparison.InvariantCultureIgnoreCase));

        return index;
    }

    IEnumerator DisableObjectiveNotification()
    {
        yield return new WaitForSeconds(2f);
        objectiveNotification.SetActive(false);
    }

    public void ShowLevelStats()
    {
        timer.CalculateLevelBonus();

        switch (currentLevel)
        {
            case "TheRoom":
                GameManager.Instance.level1Points += GameManager.Instance.levelCoinPoints + GameManager.Instance.reviveBonus + GameManager.Instance.timeBonus + GameManager.Instance.objectivePoints;
                break;
                                   
        }
        
        GameManager.Instance.UpdateHighscore();
        StartCoroutine(ShowStats());
        GameManager.Instance.SaveScore();
    }

    IEnumerator ShowStats()
    {
        levelComplete.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(1f);
        BonusStat.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        ObjectiveStat.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        objectivePoints.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        ReviveStat.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        revivePoints.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        levelTimer.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        timeBonus.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        levelScore.SetActive(true);
        audioSource.PlayOneShot(statSound);
        yield return new WaitForSeconds(0.5f);
        highscore.SetActive(true);
        menu.SetActive(true);
        continueGame.SetActive(true);
        audioSource.PlayOneShot(scoreSound);

    }

    public void BackToMenu()
    {
        GameManager.Instance.ChangeScene(GameManager.GameScene.Menu);
    }

    public void End()
    {
        GameManager.Instance.ChangeScene(GameManager.GameScene.End);
    }
}
