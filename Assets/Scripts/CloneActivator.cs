using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneActivator : MonoBehaviour
{
    Animator anim;
    CapsuleCollider collider;
    Rigidbody rb;
    MosqitController controller;
    RagdollToggle ragdoll;

    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;
    // Start is called before the first frame update
    void Start()
    {
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


        anim = GetComponent<Animator>();
        anim.enabled = true;
        collider = GetComponent<CapsuleCollider>();
        collider.enabled = true;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        controller = GetComponent<MosqitController>();
        
        controller.enabled = true;
        gameObject.tag = "Player";

        Invoke("AddRagdoll", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddRagdoll()
    {
        gameObject.AddComponent<RagdollToggle>();
        ragdoll = GetComponent<RagdollToggle>();
    }


}
