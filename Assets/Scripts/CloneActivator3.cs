using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneActivator3 : MonoBehaviour
{
    Animator anim;
    CapsuleCollider collider;
    Rigidbody rb;
    MosqitController controller;
    RagdollToggle3 ragdoll;
    public ParticleSystem reviveParticle;
    RotationKeeper rotationKeeper;
    
    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(reviveParticle, transform.position, Quaternion.identity);
        childrenCollider = GetComponentsInChildren<Collider>();
        childrenRigidbody = GetComponentsInChildren<Rigidbody>();

        foreach (var collider in childrenCollider)
        {
            collider.enabled = false;
        }
        foreach (var rigidbody in childrenRigidbody)
        {
            rigidbody.detectCollisions = false;
            rigidbody.isKinematic = true;

        }

        GameManager.Instance.levelRevivesCount++;

        anim = GetComponent<Animator>();
        anim.enabled = true;
        collider = GetComponent<CapsuleCollider>();
        collider.enabled = true;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        controller = GetComponent<MosqitController>();
        controller.enabled = true;
        rotationKeeper = FindObjectOfType<RotationKeeper>();
        controller.currentYRotation = rotationKeeper.PlayerRotationY;
        controller.wantedYRotation = rotationKeeper.PlayerRotationY;
        gameObject.tag = "Player";
        
        Invoke("AddRagdoll", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddRagdoll()
    {
        gameObject.AddComponent<RagdollToggle3>();
        ragdoll = GetComponent<RagdollToggle3>();
    }

   
}
