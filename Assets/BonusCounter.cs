using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusCounter : MonoBehaviour
{
    int bonusPoints = 0;
    Animator anim;
    
    TextMeshProUGUI bonusText;
    // Start is called before the first frame update
    void Start()
    {
        bonusText = GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBonusScore(int bonusScore)
    {
        bonusPoints += bonusScore;
        bonusText.text = "BONUS: " + bonusPoints;
        
        anim.Play("Bonus");

    }
}
