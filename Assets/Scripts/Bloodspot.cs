using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bloodspot : MonoBehaviour
{
    Button suckButton;
    float distanceFromBloodSpot;
    Transform player;
    Animator playerAnim;
    CapsuleCollider playerCol;
    bool first;
    float timer;

    public GameObject[] Swipes;

    public Image bloodFrame;
    public Image bloodFill;

    public Image[] gameUI;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3f;
        suckButton = GameObject.FindGameObjectWithTag("SuckButton").GetComponent<Button>();
        suckButton.onClick.AddListener(Bloodsuck.Suck);
        suckButton.gameObject.SetActive(false);
        bloodFrame.gameObject.SetActive(false);
        bloodFill.gameObject.SetActive(false);
        InvokeRepeating("GetPlayer", 0f, 5f);
        first = false;
        foreach (GameObject swipeObject in Swipes)
        {
            swipeObject.SetActive(false);
        }
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
    }

    void GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
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
            
            bloodFill.fillAmount += 0.03f * Time.deltaTime;
        }
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

    
}
