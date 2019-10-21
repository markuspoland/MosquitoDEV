using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{
    Rigidbody playerRb;
    bool playerDying;
    public float revivePoints { get; set; }
    Button reviveButton;
    Image reviveImage;


    void Start()
    {
        revivePoints = 0f;
        playerDying = false;
        playerRb = GetComponent<Rigidbody>();
        reviveButton = GameObject.FindGameObjectWithTag("ReviveButton").GetComponent<Button>();
        reviveImage = reviveButton.GetComponentInChildren<Image>();
        reviveButton.onClick.AddListener(RevivePlayer);
        reviveButton.gameObject.SetActive(false);
                
    }

    // Update is called once per frame
    void Update()
    {
        //if (!playerRb)
        //{
        //    playerRb = GameObject.FindGameObjectWithTag("PlayerRevived").GetComponent<Rigidbody>();
        //}
        ////} else if (!playerRb)
        ////{
        ////    playerRb = GameObject.FindGameObjectWithTag("PlayerRevived1").GetComponent<Rigidbody>();
        ////} else
        ////{
        ////    playerRb = GameObject.FindGameObjectWithTag("PlayerRevived2").GetComponent<Rigidbody>();
        ////}

        if (playerRb.isKinematic && !MosqitController.isFlipping)
        {
            playerDying = true;
        }

        else
        {
            playerDying = false;
        }

        if (playerDying)
        {
            
            reviveButton.gameObject.SetActive(true);
            
        }

        if (revivePoints > 0)
        {
            revivePoints -= 40f * Time.deltaTime;
        }
                
        reviveImage.fillAmount = revivePoints / 100f;

        if (MosqitController.playerDead)
        {
            reviveButton.gameObject.SetActive(false);
        }
    }

    public void RevivePlayer()
    {
        
        revivePoints += 15f;

        
    }
}
