using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyAttack : MonoBehaviour
{
    private bool isInLight;
    private bool isInHitRange;
    private int atkNum;
    public Animator boyAnim;

    // Start is called before the first frame update
    void Start()
    {
        isInLight = false;
        isInHitRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider plr)
    {
        if (plr.tag == "Player")
        {
            isInLight = true;
        }

        if (gameObject.tag == "DeskLight")
        {
            isInHitRange = true;
        }

    }

    private void OnTriggerStay(Collider plr)
    {

        if (plr.gameObject.name.Contains("Mosquito"))
        {


            if (isInLight && isInHitRange)
            {
                int attackTime = Random.Range(100, 2000);
                float attackStart = Random.Range(0, 100);


                atkNum = Random.Range(1, 3);

                InvokeRepeating("BoyLightAttack", attackStart, attackTime);
            }

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

    void BoyLightAttack()
    {

        boyAnim.SetInteger("AttackNumber", atkNum);

    }
}
