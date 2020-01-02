using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bloodsuck : MonoBehaviour
{
    Transform bloodSpot1;
    Transform bloodSpot2;
    Transform bloodSpot3;
    
    Collider[] boyColliders;
    CapsuleCollider capsuleCol;
    MosqitController playerController;
    static Animator anim;
    public static bool IsSucking;

    // Start is called before the first frame update
    private void Awake()
    {
        
        bloodSpot1 = GameObject.FindGameObjectWithTag("BloodSpot1").transform;
    }

    void Start()
    {
        capsuleCol = GetComponent<CapsuleCollider>();
        playerController = GetComponent<MosqitController>();
        anim = GetComponent<Animator>();           
        boyColliders = GameObject.FindGameObjectWithTag("Boy").GetComponentsInChildren<Collider>();
        IsSucking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsSucking)
        {

            foreach (Collider col in boyColliders)
            {
                col.enabled = false;
            }
            
            playerController.enabled = false;


            
                //transform.position = Vector3.Lerp(transform.position, bloodSpot1.position, 6f * Time.deltaTime);
                //transform.rotation = Quaternion.Slerp(transform.rotation, bloodSpot1.rotation, Time.deltaTime);
            
            if (Vector3.Distance(transform.position, bloodSpot1.position) < 20f)
            
                transform.rotation = bloodSpot1.rotation;
                transform.position = bloodSpot1.position;
                StartCoroutine(SuckBlood());
            

        } 
    }

    public static void Suck()
    {
        IsSucking = true;
        
    }

    IEnumerator SuckBlood()
    {
        anim.SetTrigger("Suck");
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Sucking", IsSucking);
    }

    
}
