using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera vcam1;
    CinemachineVirtualCamera vcam2;
    CinemachineVirtualCamera vcam3;
    CinemachineVirtualCamera vcam4;
    Bloodsuck bloodSuck;
    
    Rigidbody playerRb;

    private void Awake()
    {
        vcam1 = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        vcam2 = GameObject.Find("RagdollCamera").GetComponent<CinemachineVirtualCamera>();
        vcam3 = GameObject.Find("SuckingCamera").GetComponent<CinemachineVirtualCamera>();
        vcam4 = GameObject.Find("BoyDeathCamera").GetComponent<CinemachineVirtualCamera>();

        SetPlayer();

        vcam2.enabled = false;
        vcam3.enabled = false;
        vcam4.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(GameObject.FindGameObjectWithTag("Player").tag);

        //if (!playerRb)
        //{
        //    Invoke("SetPlayer", 0f);
        //}
        //} else if (!playerRb)
        //{
        //    Invoke("SetPlayer1", 0f);

        //} else
        //{
        //    Invoke("SetPlayer2", 0f);
        //}

        

        
        //else
        //{
        //    vcam1.enabled = true;
        //    vcam2.enabled = false;
        //}
    }

    private void FixedUpdate()
    {
        if (playerRb.isKinematic == true)
        {
            Invoke("ChangeCamera", 0f);
        }
        else
        {
            Invoke("InititalCamera", 0f);
        }

        
            if (Bloodsuck.IsSucking)
            {
                Invoke("EnableSuckCamera1", 0f);
            }
        
            
        
    }

    void ChangeCamera()
    {
        SetPlayer();
        EnableDeathCam();
    }

    void InititalCamera()
    {
        SetPlayer();
        EnablePlayerCam();
    }

    void SetPlayer()
    {
        bloodSuck = GameObject.FindGameObjectWithTag("Player").GetComponent<Bloodsuck>();
        vcam1.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        vcam1.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        vcam2.Follow = GameObject.FindGameObjectWithTag("RootJoint").transform;
        vcam2.LookAt = GameObject.FindGameObjectWithTag("RootJoint").transform;
    }

    void EnablePlayerCam()
    {
        vcam1.enabled = true;
        vcam2.enabled = false;
    }

    void EnableDeathCam()
    {
        vcam1.enabled = false;
        vcam2.enabled = true;
    }

    void EnableSuckCamera1()
    {
        vcam1.enabled = false;
        vcam2.enabled = false;
        vcam3.enabled = true;
        vcam4.enabled = false;
    }

    void EnableBoyDeathCamera()
    {
        vcam1.enabled = false;
        vcam2.enabled = false;
        vcam3.enabled = false;
        vcam4.enabled = true;
    }

    //void SetPlayer1()
    //{
    //    playerRb = GameObject.FindGameObjectWithTag("PlayerRevived1").GetComponent<Rigidbody>();
    //    vcam1.Follow = GameObject.FindGameObjectWithTag("PlayerRevived1").transform;
    //    vcam1.LookAt = GameObject.FindGameObjectWithTag("PlayerRevived1").transform;
    //    vcam2.Follow = GameObject.FindGameObjectWithTag("RootJoint").transform;
    //    vcam2.LookAt = GameObject.FindGameObjectWithTag("RootJoint").transform;
    //    vcam1.enabled = true;
    //    vcam2.enabled = false;
    //}

    //void SetPlayer2()
    //{
    //    playerRb = GameObject.FindGameObjectWithTag("PlayerRevived2").GetComponent<Rigidbody>();
    //    vcam1.Follow = GameObject.FindGameObjectWithTag("PlayerRevived2").transform;
    //    vcam1.LookAt = GameObject.FindGameObjectWithTag("PlayerRevived2").transform;
    //    vcam2.Follow = GameObject.FindGameObjectWithTag("RootJoint").transform;
    //    vcam2.LookAt = GameObject.FindGameObjectWithTag("RootJoint").transform;
    //    vcam1.enabled = true;
    //    vcam2.enabled = false;
    //}
}
