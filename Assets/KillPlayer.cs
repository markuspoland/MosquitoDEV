using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Bloodsuck.IsSucking = false;
        Debug.Log("KILL KILL KILL");
                boyColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in boyColliders)
        {
            col.enabled = true;
        }

        bloodspot.enabled = false;
        
    }

    void CheckBloodspot()
    {
        if (!bloodspot.enabled)
        {
            bloodspot.enabled = true;
        }
    }

    
}
