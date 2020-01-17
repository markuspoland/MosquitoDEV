using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using RDG;

public class Bloodspot : MonoBehaviour
{
    Button suckButton;
    float distanceFromBloodSpot;
    Transform player;
    MosqitController playerController;
    Animator playerAnim;
    [SerializeField] Animator boyAnim;
    Collider[] boyColliders;
    CapsuleCollider playerCol;
    bool first;
    float timer;
    Bloodsuck bloodsuck;
    public static bool boyIsDead;

    public GameObject[] Swipes;

    public Image bloodFrame;
    public Image bloodFill;

    public Image[] gameUI;
    public Image[] playerLivesImage;

    public CinemachineVirtualCamera suckingCamera;
    public CinemachineVirtualCamera boyDeathCamera;

    public static bool swipeFailed;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3f;
        suckButton = GameObject.FindGameObjectWithTag("SuckButton").GetComponent<Button>();
        suckButton.onClick.AddListener(Bloodsuck.Suck);
        suckButton.gameObject.SetActive(false);
        bloodFrame.gameObject.SetActive(false);
        bloodFill.gameObject.SetActive(false);
        boyColliders = GameObject.FindGameObjectWithTag("Boy").GetComponentsInChildren<Collider>();
        InvokeRepeating("GetPlayer", 0f, 5f);
        first = false;
        foreach (GameObject swipeObject in Swipes)
        {
            swipeObject.SetActive(false);
        }
        swipeFailed = false;
        boyIsDead = false;
    }

    private void OnEnable()
    {
        EventManager.StartListening("BoyDeath", BoyDeath);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(RageLight.enraged);
        //if (RageLight.enraged)
        //{
        if (player != null)
        {
            distanceFromBloodSpot = Vector3.Distance(transform.position, player.position);

            if (distanceFromBloodSpot < 20f && !MosqitController.playerDead && !MosqitController.isInRagdoll)
            {
                suckButton.gameObject.SetActive(true);
            }
            else
            {
                suckButton.gameObject.SetActive(false);
            }
        }
        //}

        if (Bloodsuck.IsSucking)
        {
            playerCol.enabled = false;
            suckButton.gameObject.SetActive(false);
            bloodFrame.gameObject.SetActive(true);
            bloodFill.gameObject.SetActive(true);
            EnableSwipe();
            
            StartCoroutine("FillBlood");

        } 

        if (swipeFailed)
        {
            StopCoroutine("FillBlood");
            boyAnim.SetTrigger("Kill");
            StartCoroutine("EnablePlayer");
            timer = 3f;          
        }
    }


    void GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MosqitController>();
            playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
    }

    IEnumerator FillBlood()
    {
        foreach (Image img in gameUI)
        {
            img.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(2.5f);
        
        if (bloodFill.fillAmount < 1f)
        {
            
            bloodFill.fillAmount += 0.04f * Time.deltaTime;
        } else if (bloodFill.fillAmount >= 1f)
        {
            EventManager.TriggerEvent("BoyDeath");
            yield break;
        }
    }

    IEnumerator EnablePlayer()
    {
        yield return new WaitForSeconds(1.24f);
        bloodsuck = GameObject.FindGameObjectWithTag("Player").GetComponent<Bloodsuck>();
        
        
        if (playerCol)
        {
            playerCol.enabled = true;
        }
        
        playerController.enabled = true;

        
        foreach (GameObject swipeObject in Swipes)
        {
            swipeObject.SetActive(false);
        }

        swipeFailed = false;

        foreach (Image img in gameUI)
        {
            img.gameObject.SetActive(true);
        }

        suckButton.gameObject.SetActive(false);
        bloodFrame.gameObject.SetActive(false);
        bloodFill.gameObject.SetActive(false);
    }

    void EnableSwipe()
    {

            timer -= Time.deltaTime;


        if (timer <= 0)
        {
            int randomSwipe = Random.Range(0, 2);
            Swipes[randomSwipe].SetActive(true);
            timer = 3f;
        }

       

    }

    void BoyDeath()
    {

        boyAnim.SetTrigger("Die");
        Destroy(player.gameObject);
        suckButton.gameObject.SetActive(false);
        bloodFrame.gameObject.SetActive(false);
        bloodFill.gameObject.SetActive(false);
        foreach (Image img in gameUI)
        {
            img.gameObject.SetActive(false);
        }
        foreach (Image img in playerLivesImage)
        {
            img.gameObject.SetActive(false);
        }

        CancelInvoke();
        boyIsDead = true;
        suckingCamera.enabled = false;
        boyDeathCamera.enabled = true;
        gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        EventManager.StopListening("BoyDeath", BoyDeath);
    }

}
