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

    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrow1;
    [SerializeField] GameObject arrow2;
    [SerializeField] GameObject arrow4;


    // Start is called before the first frame update
    void Start()
    {
        enraged = false;
        rage = GameObject.FindGameObjectWithTag("Rage").GetComponent<Image>();
        rage1 = GameObject.FindGameObjectWithTag("Rage1").GetComponent<Image>();
        rage2 = GameObject.FindGameObjectWithTag("Rage2").GetComponent<Image>();
        arrow4.SetActive(false);

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
            arrow4.SetActive(true);
        }
       
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

            if (gameObject.tag == "CeilingRage")
            {
                arrow.SetActive(false);
            }

            if (gameObject.tag == "CandleRage")
            {
                arrow1.SetActive(false);
            }

            if (gameObject.tag == "DeskLight")
            {
                arrow2.SetActive(false);
            }

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
