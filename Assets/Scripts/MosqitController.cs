﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MosqitController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    CapsuleCollider playerCol;
    [SerializeField] float upForce;
    float movementForwardSpeed = 50f;
    float tiltAmountForward = 0;
    float tiltAmountSide = 0;
    float tiltVelocityForward;
    float tiltVelocitySide;
    public float wantedYRotation { get; set; }
    public float currentYRotation { get; set; }
    float rotateAmountByKeys = 3.5f;
    float rotationYVelocity;
    public static bool playerDead;
    public static bool isFlipping;
    RotationKeeper rotationKeeper;
    AudioSource mosquitoAudio;
    float startPitch;
    float desiredPitch = 0.75f;
    float pitchDown = 0.05f;
    float defaultPan = 0f;
    float desiredPanLeft = -0.25f;
    float desiredPanRight = 0.25f;
    float panDown = 0.05f;

    Button flipButton;
    Button dashLeftButton;
    Button dashRightButton;
                    
    protected Joystick flyHandle;

    float dashSpeed;
    float dashTime;
    float startDashTime;
    bool dashingLeft;
    bool dashingRight;

    
    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;       
        currentYRotation = 90f;
        wantedYRotation = 90f;
        rotationKeeper = FindObjectOfType<RotationKeeper>();
        isFlipping = false;
        playerCol = GetComponent<CapsuleCollider>();
        flyHandle = GameObject.FindGameObjectWithTag("JoystickVert").GetComponent<Joystick>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        flipButton = GameObject.FindGameObjectWithTag("Flip").GetComponent<Button>();
        flipButton.onClick.AddListener(Flip);
        dashSpeed = 200f;
        startDashTime = 0.2f;
        dashTime = startDashTime;
        dashLeftButton = GameObject.FindGameObjectWithTag("LeftRoll").GetComponent<Button>();
        dashLeftButton.onClick.AddListener(DashLeftClick);
        dashingLeft = false;
        dashingRight = false;
        dashRightButton = GameObject.FindGameObjectWithTag("RightRoll").GetComponent<Button>();
        dashRightButton.onClick.AddListener(DashRightClick);
        mosquitoAudio = GetComponent<AudioSource>();
        startPitch = mosquitoAudio.pitch;
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    private void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();

        rb.AddRelativeForce(Vector3.up * upForce);
        rb.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation - 0.5f, tiltAmountSide));

        if (dashingLeft)
        {
            DashLeft();
            //dashing = false;
        }

        if (dashingRight)
        {
            DashRight();
        }

    }

    private void Rotation()
    {
        

        if (flyHandle.Horizontal < 0)
        {
            wantedYRotation += flyHandle.Horizontal * rotateAmountByKeys;
            PitchDown();
            PanLeft();
        } 

        if (flyHandle.Horizontal > 0)
        {
            wantedYRotation += flyHandle.Horizontal * rotateAmountByKeys;
            PitchDown();
            PanRight();
        }

        if (flyHandle.Horizontal >= -0.2 && flyHandle.Horizontal <= 0.2)
        {
            ResetPitch();
            ResetPan();
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            wantedYRotation += rotateAmountByKeys;
        }

        
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

    }

    private void MovementForward()
    {

        rb.velocity = transform.forward * movementForwardSpeed;
        tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 30 * -Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.2f);
        tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, 30 * -Input.GetAxis("Horizontal"), ref tiltVelocitySide, 0.1f);

        tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 70 * -flyHandle.Vertical, ref tiltVelocityForward, 0.2f);
        tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, 50 * -flyHandle.Horizontal, ref tiltVelocitySide, 0.1f);

    }

    private void MovementUpDown()
    {
        if (flyHandle.Vertical > 0)
        {
            upForce = flyHandle.Vertical * 800f;
            
        }
        else if (flyHandle.Vertical < 0)
        {
            upForce = flyHandle.Vertical * 900f;
        } else
        {
            upForce = Mathf.Lerp(upForce, 0f, 0.5f);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            upForce = 300f;
        } else if (Input.GetAxis("Vertical") < 0)
        {
            upForce = -300f;
        } else
        {
            upForce = Mathf.Lerp(upForce, 0f, 0.5f);
        }

    }

    public void Flip()
    {
        rotationKeeper.PlayerRotationY = Quaternion.LookRotation(-transform.forward, transform.up).y * 126.72747f;
        StartCoroutine("BackFlip");
                
    }

    IEnumerator BackFlip()
    {
        
        anim.applyRootMotion = true;
        isFlipping = true;
        //rb.isKinematic = true;
        
        anim.SetTrigger("backflip");
        //yield return new WaitForSeconds(0.3f);
        anim.applyRootMotion = false;
        wantedYRotation = rotationKeeper.PlayerRotationY;
        //rb.isKinematic = false;
        isFlipping = false;
        yield return new WaitForEndOfFrame();
    }
    
    void DashLeftClick()
    {
        
        anim.SetTrigger("dashleft");
        dashingLeft = true;
    }

    void DashRightClick()
    {
        anim.SetTrigger("dashright");
        dashingRight = true;
    }

    void DashLeft()
    {
        Vector3 velocity = rb.velocity;
        if (dashTime <= 0)
        {
            
            rb.velocity = velocity;
            dashTime = startDashTime;
            dashingLeft = false;
        }
        else
        {
            dashTime -= Time.fixedDeltaTime;
            rb.velocity = velocity + -transform.right * dashSpeed;
        }
    }

    void DashRight()
    {
        Vector3 velocity = rb.velocity;
        if (dashTime <= 0)
        {

            rb.velocity = velocity;
            dashTime = startDashTime;
            dashingRight = false;
        }
        else
        {
            dashTime -= Time.fixedDeltaTime;
            rb.velocity = velocity + transform.right * dashSpeed;
        }
    }

    void PitchDown()
    {
        mosquitoAudio.pitch -= pitchDown;
        
        if (mosquitoAudio.pitch <= desiredPitch)
        {
            mosquitoAudio.pitch = desiredPitch;
        }
    }

    void PanLeft()
    {
        mosquitoAudio.panStereo -= panDown;
        if (mosquitoAudio.panStereo <= desiredPanLeft)
        {
            mosquitoAudio.panStereo = desiredPanLeft;
        }
    }

    void PanRight()
    {
        mosquitoAudio.panStereo += panDown;
        if (mosquitoAudio.panStereo >= desiredPanRight)
        {
            mosquitoAudio.panStereo = desiredPanRight;
        }
    }

    void ResetPitch()
    {
        if (mosquitoAudio.pitch != startPitch)
        {
            mosquitoAudio.pitch += pitchDown;
            if (mosquitoAudio.pitch >= startPitch)
            {
                mosquitoAudio.pitch = startPitch;
            }
        }
    }

    void ResetPan()
    {
        if (mosquitoAudio.panStereo <= defaultPan)
        {
            mosquitoAudio.panStereo += panDown;
            if (mosquitoAudio.panStereo >= defaultPan)
            {
                mosquitoAudio.panStereo = defaultPan;
            }
        } else if (mosquitoAudio.panStereo >= defaultPan)
        {
            mosquitoAudio.panStereo -= panDown;
            if (mosquitoAudio.panStereo <= defaultPan)
            {
                mosquitoAudio.panStereo = defaultPan;
            }
        }
    }


} 
