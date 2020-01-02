using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeHandler : MonoBehaviour
{

    public AudioClip success;
    public AudioClip failure;
    public GameObject successImage;
    public GameObject failureImage;
    public Animator successAnim;
    public Animator failureAnim;
    public AudioSource audioSource;

    int checker;
    // Start is called before the first frame update
     void Awake()
    {
        checker = 0;
    }

    void OnEnable()
    {

        EventManager.StartListening("SwipeFailure", SwipeFailure);
        EventManager.StartListening("SwipeSuccess", SwipeSuccess);
        Invoke("DisableMe", 1.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Left"))
        {
            print("I am " + gameObject.tag);
            
            if (SwipeInput.swipedLeft && checker == 0)
            {
                checker = 1;
                EventManager.TriggerEvent("SwipeSuccess");
                
            } else if (SwipeInput.swipedRight && checker == 0)
            {
                checker = 1;
                EventManager.TriggerEvent("SwipeFailure");
            }
        }

        if (gameObject.CompareTag("Right"))
        {
            print("I am " + gameObject.tag);
            if (SwipeInput.swipedRight && checker == 0)
            {
                checker = 1;
                EventManager.TriggerEvent("SwipeSuccess");
            }
            else if (SwipeInput.swipedLeft && checker == 0)
            {
                checker = 1;
                EventManager.TriggerEvent("SwipeFailure");
            }
        }
    }

    void OnDisable()
    {
                
        EventManager.StopListening("SwipeSuccess", SwipeSuccess);
        EventManager.StopListening("SwipeFailure", SwipeFailure);
        
    }

    void DisableMe()
    {
        if (checker == 0)
        {
            EventManager.TriggerEvent("SwipeFailure");
            
        }
        checker = 0;
        gameObject.SetActive(false);
    }

    void SwipeSuccess()
    {
        successAnim.SetTrigger("success");
        LevelSoundManager.audioSource.PlayOneShot(LevelSoundManager.swipeSuccess);
        
    }

    void SwipeFailure()
    {
        Debug.Log("I Failed");
        failureAnim.SetTrigger("fail");
        LevelSoundManager.audioSource.PlayOneShot(LevelSoundManager.swipeFailure);
        Bloodspot.swipeFailed = true;

    }

    //IEnumerator Failure()
    //{
    //    EventManager.TriggerEvent("SwipeFailure");
    //    yield return new WaitForSeconds(0.5f);
    //}
}
