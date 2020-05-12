using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
    public static bool isInRagdoll;
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
    Button brakeButton;
    Button dashLeftButton;
    Button dashRightButton;

    BrakeHandler brakeHandler;
    DiveHandler diveHandler;

    Slider staminaSlider;
    float stamina = 2f;
                    
    protected Joystick flyHandle;

    float dashSpeed;
    float dashTime;
    float startDashTime;
    bool dashingLeft;
    bool dashingRight;
    float cooldown;

    float startVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        isInRagdoll = false;
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
        brakeButton = GameObject.FindGameObjectWithTag("Brake").GetComponent<Button>();
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
        cooldown = 0f;
        brakeHandler = FindObjectOfType<BrakeHandler>().GetComponent<BrakeHandler>();
        diveHandler = FindObjectOfType<DiveHandler>().GetComponent<DiveHandler>();
        staminaSlider = GameObject.FindGameObjectWithTag("Stamina").GetComponent<Slider>();
        startVolume = mosquitoAudio.volume;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0f)
        {
            cooldown = 0f;
        }

        HandleBrakeAndAccelerate();



        staminaSlider.value = stamina / 2;
        Debug.Log("Stamina: " + stamina);

        
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

    void HandleBrakeAndAccelerate()
    {
        if (brakeHandler.BrakePressed())
        {
            movementForwardSpeed = 30f;
            mosquitoAudio.pitch = 0.75f;
            anim.SetBool("BrakePressed", true);
            if (stamina < 2f)
            {
                stamina += 1.0f * Time.deltaTime;
            }
        }
        else
        {
            movementForwardSpeed = 50f;
            mosquitoAudio.pitch = 1f;
            anim.SetBool("BrakePressed", false);
        }

        if (diveHandler.DivePressed() && stamina > 0f)
        {
            movementForwardSpeed = 100f;
            mosquitoAudio.pitch = 1.25f;
            stamina -= 1.0f * Time.deltaTime;
            anim.SetBool("DivePressed", true);
        }

        if ((diveHandler.DivePressed() && stamina <= 0f) && !brakeHandler.BrakePressed())
        {
            stamina = 0f;
            movementForwardSpeed = 50f;
            anim.SetBool("DivePressed", false);
        }
        else if (!diveHandler.DivePressed() && stamina < 2f)
        {
            stamina += 0.2f * Time.deltaTime;
            anim.SetBool("DivePressed", false);
        }
        else if ((!diveHandler.DivePressed() && stamina >= 2f) && !brakeHandler.BrakePressed())
        {
            stamina = 2f;
            movementForwardSpeed = 50f;
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
        if (cooldown <= 0f)
        {
            anim.SetTrigger("dashleft");
            dashingLeft = true;
            cooldown = 0.8f;
        }
    }

    void DashRightClick()
    {
        if (cooldown <= 0f)
        {
            anim.SetTrigger("dashright");
            dashingRight = true;
            cooldown = 0.8f;
        }
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

    public void RevivePitch()
    {
        mosquitoAudio.pitch += 0.65f;
    }

    public void DeadSound()
    {
        float pitchDown = 0.5f;
        if (mosquitoAudio.pitch != 0f)
        {
            mosquitoAudio.pitch -= pitchDown * Time.deltaTime;
            if (mosquitoAudio.pitch <= 0f)
            {
                mosquitoAudio.enabled = false;
            }
        }
    }

    IEnumerator ResetCollider()
    {
        playerCol.enabled = false;
        yield return new WaitForSeconds(1.5f);
        playerCol.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Godzilla")
        {
            StartCoroutine(ResetCollider());
        }
    }

    
} 
