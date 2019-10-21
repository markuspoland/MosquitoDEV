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
    // Start is called before the first frame update
    void Start()
    {
        suckButton = GameObject.FindGameObjectWithTag("SuckButton").GetComponent<Button>();
        suckButton.onClick.AddListener(Bloodsuck.Suck);
        suckButton.gameObject.SetActive(false);
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

            if (distanceFromBloodSpot < 20f && !MosqitController.playerDead)
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
            suckButton.gameObject.SetActive(false);
        }
    }

    void GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
    }

}
