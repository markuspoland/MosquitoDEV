using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using RDG;

public class RagdollToggle3 : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    MosqitController mosqitController;
    Transform rootTransform;
    Vector3 colliderCenter;
    bool collided;
    public GameObject newMosquito;
    Button reviveButton;
    RotationKeeper rotationKeeper;
    public bool IsDead { get; set; } = false;
    public TextMeshProUGUI deathText;
    float deathcount;
    bool ragdollEnabled;



    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;
    Revive revival;

    
    // Start is called before the first frame update
    void Awake()
    {
        deathText = GameObject.FindGameObjectWithTag("DeathCount").GetComponent<TextMeshProUGUI>();
        deathText.gameObject.SetActive(false);
        rotationKeeper = FindObjectOfType<RotationKeeper>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rootTransform = GameObject.FindGameObjectWithTag("RootJoint").transform;

        mosqitController = GetComponent<MosqitController>();

        childrenCollider = GetComponentsInChildren<Collider>();
        childrenRigidbody = GetComponentsInChildren<Rigidbody>();

        revival = GetComponent<Revive>();

        colliderCenter = capsuleCollider.center;
        collided = false;
        reviveButton = GameObject.FindGameObjectWithTag("ReviveButton").GetComponent<Button>();

        ChangePlayerState();
        
    }

    void Start()
    {
        
        RagdollDisabled();
        capsuleCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            mosqitController.DeadSound();
            MosqitController.playerDead = true;
            GameOver.GameIsOver();
        }
    }

    public void RagdollEnabled()
    {


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

    public void RagdollDisabled()
    {
        
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

        if (collision.collider.gameObject.tag == "Godzilla")
        {

            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            if (levelManager)
            {
                levelManager.CompleteLevel1Objective("Defeat The King Of The Monsters");
            }
            return;
        }

        Vibration.Vibrate(40);
        GameObject blood = Instantiate(Resources.Load("CFX2_Blood", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        IsDead = true;
        RagdollEnabled();
        Debug.Log("COLLIDED!");
        LevelManager.TakeLife(0);
    }

    void ChangePlayerState()
    {            
            
     Destroy(revival);
     reviveButton.gameObject.SetActive(false);

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
