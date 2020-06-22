using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneActivator1 : MonoBehaviour
{
    Animator anim;
    CapsuleCollider collider;
    Rigidbody rb;
    MosqitController controller;
    RagdollToggle1 ragdoll;
    public ParticleSystem reviveParticle;
    AudioSource audioSource;
    [SerializeField] AudioClip reviveSound;
    RotationKeeper rotationKeeper;

    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(reviveParticle, transform.position, Quaternion.identity);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(reviveSound);


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
        GameManager.Instance.reviveBonus -= 20;

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
        gameObject.AddComponent<RagdollToggle1>();
        ragdoll = GetComponent<RagdollToggle1>();
    }

}
