﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class CoinScript : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Collider collider;
    AudioSource audioSource;
    [SerializeField] BonusCounter bonusCounter;
    [SerializeField] AudioClip coinSound;
    [SerializeField] ParticleSystem coinParticle;
    [SerializeField] GameObject coinPoints;
    [SerializeField] GameObject extraCoinPoints;
    float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(1.5f, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vibration.Vibrate(40);
            Instantiate(coinParticle, transform.position, Quaternion.identity);
            if (gameObject.tag == "Coin") {
                Instantiate(coinPoints, transform.position, Quaternion.identity);
                bonusCounter.UpdateBonusScore(10);
                GameManager.Instance.levelCoinPoints += 10;
            } else
            {
                Instantiate(extraCoinPoints, transform.position, Quaternion.identity);
                bonusCounter.UpdateBonusScore(20);
                GameManager.Instance.levelCoinPoints += 20;
            }
            
            
            audioSource.PlayOneShot(coinSound);
            meshRenderer.enabled = false;
            collider.enabled = false;
            Destroy(gameObject, coinSound.length);
        }
    }
}
