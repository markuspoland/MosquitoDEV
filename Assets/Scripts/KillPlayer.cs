﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class KillPlayer : MonoBehaviour
{
    MosqitController playerController;
    CapsuleCollider playerCol;
    Collider[] boyColliders;
    public Bloodspot bloodspot;
    public GameObject blood;
    

    void Start()
    {
        blood.SetActive(false);
        InvokeRepeating("CheckBloodspot", 20f, 1.5f);
    }
    // Start is called before the first frame update
    public void Kill()
    {
        
        Vibration.Vibrate(150);
        Bloodsuck.IsSucking = false;
        Debug.Log("KILL KILL KILL");
        bloodspot.enabled = false;
        boyColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in boyColliders)
        {
            col.enabled = true;
        }
                        
    }

    void CheckBloodspot()
    {
        if (!bloodspot.enabled)
        {
            bloodspot.enabled = true;
        }
    }

    public void EnableStats()
    {
                
        LevelManager levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        levelManager.ShowLevelStats();
    }

    public void SpillBlood()
    {
        blood.SetActive(true);
    }
}
