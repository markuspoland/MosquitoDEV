using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class KillPlayer : MonoBehaviour
{
    MosqitController playerController;
    CapsuleCollider playerCol;
    Collider[] boyColliders;
    public Bloodspot bloodspot;

    void Start()
    {
        
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

    
}
