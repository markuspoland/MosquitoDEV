using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{

    public AudioClip success;
    public AudioClip failure;
    public GameObject successImage;
    public GameObject failureImage;
    public Animator successAnim;
    public Animator failureAnim;
    public AudioSource audioSource;
    // Start is called before the first frame update
     void Awake()
    {
     
    }

    void OnEnable()
    {
        Invoke("DisableMe", 1.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Left"))
        {
            print("I am " + gameObject.tag);
            if (SwipeManager.swipeDirection == SwipeManager.Swipe.Left)
            {
                audioSource.PlayOneShot(success);
                successAnim.SetTrigger("success");

            } else if (SwipeManager.swipeDirection == SwipeManager.Swipe.Right)
            {
                audioSource.PlayOneShot(failure);
                failureAnim.SetTrigger("fail");
            }
        }

        if (gameObject.CompareTag("Right"))
        {
            print("I am " + gameObject.tag);
            if (SwipeManager.swipeDirection == SwipeManager.Swipe.Right)
            {
                successAnim.SetTrigger("success");
                audioSource.PlayOneShot(success);
            }
            else if (SwipeManager.swipeDirection == SwipeManager.Swipe.Left)
            {
                audioSource.PlayOneShot(failure);
                failureAnim.SetTrigger("fail");
            }
        }
    }

    void OnDisable()
    {
        audioSource.PlayOneShot(failure);
        
        print("FAILURE!!!!!");
    }

    void DisableMe()
    {
        
        gameObject.SetActive(false);
    }
}
