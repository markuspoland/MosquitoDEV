using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageLight : MonoBehaviour
{
    float rageLevel = 0.23f;
    float rageDrop = 0.04f;
    bool isInLight;
    bool isInHitRange;
    Image rage;
    Image rage1;
    Image rage2;
    Image[] rages;
    Button bloodSuckBtn;
    public Animator boyAnim;
    public Animator rageIconAnim;
    public static int atkNum;
    Image currentRageImage = null;
    public GameObject rageHighlight;
    public static bool enraged;


    // Start is called before the first frame update
    void Start()
    {
        enraged = false;
        rage = GameObject.FindGameObjectWithTag("Rage").GetComponent<Image>();
        rage1 = GameObject.FindGameObjectWithTag("Rage1").GetComponent<Image>();
        rage2 = GameObject.FindGameObjectWithTag("Rage2").GetComponent<Image>();
        

        isInLight = false;
        isInHitRange = false;
        rages = new Image[3];
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rage.fillAmount >= 1f && rage1.fillAmount >= 1f && rage2.fillAmount >= 0.99f)
        {
            enraged = true;
        }
        Debug.Log("Rage1: " + rage.fillAmount);
        Debug.Log("Rage2: " + rage1.fillAmount);
        Debug.Log("Rage3: " + rage2.fillAmount);

        if (isInLight)
        {

            rages[0] = rage2;
            rages[1] = rage1;
            rages[2] = rage;

            
          
            if (currentRageImage == null)
            {
                for (int i = 0; i < rages.Length; i++)
                {
                    if (rages[i].fillAmount == 0f)
                    {
                        currentRageImage = rages[i];
                        
                    }
                }
            }
            else
            {
                if (!MosqitController.isInRagdoll)
                {
                    currentRageImage.fillAmount += rageLevel * Time.deltaTime;
                    rageIconAnim.SetBool("InLight", true);
                }
            }

        }

        if (currentRageImage != null && currentRageImage.fillAmount >= 1f)
        {
            rageIconAnim.SetBool("InLight", false);
            gameObject.GetComponent<RageLight>().enabled = false;
        }

        if (currentRageImage != null)
        {
            if (!isInLight && (currentRageImage.fillAmount > 0f && currentRageImage.fillAmount < 1f))
            {
                currentRageImage.fillAmount -= rageDrop * Time.deltaTime;
                rageIconAnim.SetBool("InLight", false);
            }
        }

    }

    private void OnTriggerEnter(Collider plr)
    {
        if (plr.tag == "Player")
        {
            isInLight = true;
        }

    }

    private void OnTriggerExit(Collider plr)
    {
        if (plr.gameObject.name.Contains("Mosquito"))
        {
            isInLight = false;
            isInHitRange = false;
            atkNum = 0;
        }
    }

}
