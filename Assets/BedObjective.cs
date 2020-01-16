using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedObjective : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            if (levelManager)
            {
                levelManager.CompleteLevel1Objective("Under The Bed");
                Destroy(gameObject, 2f);
            }
        }
    }


}
