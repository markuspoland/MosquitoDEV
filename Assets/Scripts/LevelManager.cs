using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

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
    [SerializeField] GameObject levelComplete;
    [SerializeField] GameObject BonusStat;
    [SerializeField] GameObject ObjectiveStat;
    [SerializeField] GameObject ReviveStat;
    [SerializeField] GameObject levelTimer;
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
        ReviveStat.SetActive(false);
        levelTimer.SetActive(false);
    }

    void Start()
    {
        GameManager.Instance.levelCoinPoints = 0;
        GameManager.Instance.levelRevivesCount = 0;
        GameManager.Instance.ObjectivesCompleted = 0;
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
        StartCoroutine(ShowStats());
    }

    IEnumerator ShowStats()
    {
        levelComplete.SetActive(true);
        yield return new WaitForSeconds(1f);
        BonusStat.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ObjectiveStat.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ReviveStat.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        levelTimer.SetActive(true);
    }
}
