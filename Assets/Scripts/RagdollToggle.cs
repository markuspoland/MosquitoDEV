using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using RDG;

public class RagdollToggle : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    MosqitController mosqitController;
    public Transform rootTransform;
    Vector3 colliderCenter;
    bool collided;
    public GameObject newMosquito;
    RotationKeeper rotationKeeper;
    Transform tempRot { get; set; }
    public TextMeshProUGUI deathText;
    float deathcount;
    public static bool ragdollEnabled;
    bool isCaught;
        
   
    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;

    Revive revival;

    public GameObject bloodFx;
    Transform theNet;
    // Start is called before the first frame update
    void Awake()
    {
        
        ragdollEnabled = false;
        isCaught = false;
        deathcount = 5;
        deathText.gameObject.SetActive(false);
        rotationKeeper = FindObjectOfType<RotationKeeper>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        theNet = GameObject.FindGameObjectWithTag("TheNet").transform;
                
        mosqitController = GetComponent<MosqitController>();

        childrenCollider = GetComponentsInChildren<Collider>();
        childrenRigidbody = GetComponentsInChildren<Rigidbody>();

        revival = GetComponent<Revive>();

        colliderCenter = capsuleCollider.center;
        collided = false;    
    }

    void Start()
    {
        
        RagdollDisabled();
        capsuleCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("CheckPlayerState", 5f, 15f);
        ChangePlayerState();
        if (ragdollEnabled)
        {
            deathcount -= Time.deltaTime;
            int seconds = Convert.ToInt32(deathcount % 60);
            deathText.text = seconds.ToString();

        }

        if (deathcount <= 0)
        {
            deathText.gameObject.SetActive(false);
            MosqitController.playerDead = true;
            GameOver.GameIsOver();
        }

        if (isCaught)
        {
            Vector3 netRot = new Vector3(-theNet.localEulerAngles.y, 0, 0);
            transform.localEulerAngles = new Vector3(Mathf.Lerp(transform.localEulerAngles.x, -theNet.localEulerAngles.y, 0.8f * Time.deltaTime), 0f, 0f);
        }
        //if (collided)
        //{
        //    capsuleCollider.transform.position = rootTransform.position;
        //}
    }

    public void RagdollEnabled()
    {
        MosqitController.isInRagdoll = true;
        ragdollEnabled = true;
        deathText.gameObject.SetActive(true);
        foreach (var collider in childrenCollider)
        {
            collider.enabled = true;
        }
        foreach (var rigidbody in childrenRigidbody)
        {
            rigidbody.detectCollisions = true;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(transform.forward * 15f, ForceMode.Impulse);
            
        }

        //rb.velocity = new Vector3 (transform.position.x, transform.position.y, 3f);

        anim.enabled = false;
        rb.detectCollisions = false;
        rb.isKinematic = true;
        Destroy(capsuleCollider);
        mosqitController.enabled = false;
    }

    public void NetCaught()
    {
        mosqitController.enabled = false;
        
        ragdollEnabled = true;
        deathText.gameObject.SetActive(true);

        //rb.velocity = new Vector3 (transform.position.x, transform.position.y, 3f);
        
        rb.detectCollisions = false;
        rb.isKinematic = true;
        capsuleCollider.enabled = false;
        
    }

    public void RagdollDisabled()
    {
        MosqitController.isInRagdoll = false;
        ragdollEnabled = false;
        foreach (var collider in childrenCollider)
        {
            collider.enabled = false;
        }
        foreach (var rigidbody in childrenRigidbody)
        {
            rigidbody.detectCollisions = false;
            rigidbody.isKinematic = true;
            
        }

        //rb.velocity = new Vector3 (transform.position.x, transform.position.y, 3f);

        
        anim.enabled = true;
        rb.detectCollisions = true;
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        mosqitController.enabled = true;
        
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        rotationKeeper.PlayerRotationY = Quaternion.LookRotation(-transform.forward, transform.up).y * 126.72747f;
        Vibration.Vibrate(40);
        
        if (collision.collider.gameObject.tag == "Godzilla")
        {
            
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            if (levelManager)
            {
                levelManager.CompleteLevel1Objective("Defeat The King Of The Monsters");
            }
            return;
        }

        if (collision.collider.gameObject.tag != "TheNet")
        {
            mosqitController.RevivePitch();
            Instantiate(bloodFx, transform.position, Quaternion.identity);
            RagdollEnabled();
            LevelManager.TakeLife(3);
        } else
        {
            mosqitController.RevivePitch();
            isCaught = true;
            anim.SetBool("caught", isCaught);
            Instantiate(bloodFx, transform.position, Quaternion.identity);
            NetCaught();
            LevelManager.TakeLife(3);
        }
        
        
        
    }

    void ChangePlayerState()
    {

        

        if (revival.revivePoints >= 100)
        {
            //revival.revivePoints = 0;
            //RagdollDisabled();
            Instantiate(PlayerPrefabManager.Instance.PlayerPrefab, rootTransform.position + 10f * Vector3.up, Quaternion.Inverse(Quaternion.identity));
            Destroy(gameObject);

            //Invoke("SetColliderPosition", 0f);

        }
    }

    void SetColliderPosition()
    {
        for (int c = 0; c < childrenCollider.Length - 1; c++)
        {
            for (int r = 0; r < childrenRigidbody.Length - 1; r++)
            {
                if (!childrenCollider[c].enabled && childrenRigidbody[r].isKinematic)
                {
                    capsuleCollider.transform.position = rootTransform.position;
                }
            }
        }
    }

 
    
   
}
