using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeObjective : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            if (levelManager)
            {
                levelManager.CompleteLevel1Objective("Naughty Boy!");
                Destroy(gameObject, 2f);
            }
        }
    }
}
