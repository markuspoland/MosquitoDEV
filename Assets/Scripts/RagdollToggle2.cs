using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RagdollToggle2 : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    MosqitController mosqitController;
    Transform rootTransform;
    Vector3 colliderCenter;
    bool collided;
    public GameObject newMosquito;
    RotationKeeper rotationKeeper;
    public TextMeshProUGUI deathText;
    float deathcount;
    public static bool ragdollEnabled;
    bool isCaught;
    Transform theNet;
    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;

    Revive revival;
    
    // Start is called before the first frame update
    void Awake()
    {
        isCaught = false;
        theNet = GameObject.FindGameObjectWithTag("TheNet").transform;
        deathText = GameObject.FindGameObjectWithTag("DeathCount").GetComponent<TextMeshProUGUI>();
        ragdollEnabled = false;
        deathcount = 5;
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
    }

    public void RagdollEnabled()
    {
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

        if (collision.collider.gameObject.tag != "TheNet")
        {
            GameObject blood = Instantiate(Resources.Load("CFX2_Blood", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
            RagdollEnabled();
        }
        else
        {
            isCaught = true;
            anim.SetBool("caught", isCaught);
            GameObject blood = Instantiate(Resources.Load("CFX2_Blood", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
            NetCaught();
        }

    }

    void ChangePlayerState()
    {

        

        if (revival.revivePoints >= 100)
        {            
            Instantiate(PlayerPrefabManager.Instance.PlayerPrefab2, rootTransform.position + 10f * Vector3.up, Quaternion.Inverse(Quaternion.identity));
            Destroy(gameObject);
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
