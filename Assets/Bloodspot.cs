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

    public Image bloodFrame;
    public Image bloodFill;
    // Start is called before the first frame update
    void Start()
    {
        suckButton = GameObject.FindGameObjectWithTag("SuckButton").GetComponent<Button>();
        suckButton.onClick.AddListener(Bloodsuck.Suck);
        suckButton.gameObject.SetActive(false);
        bloodFrame.gameObject.SetActive(false);
        bloodFill.gameObject.SetActive(false);
        InvokeRepeating("GetPlayer", 0f, 5f);
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
        yield return new WaitForSeconds(2.5f);
        if (bloodFill.fillAmount < 1f)
        {
            bloodFill.fillAmount += 0.03f * Time.deltaTime;
        }
    }

}
