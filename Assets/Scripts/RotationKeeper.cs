using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationKeeper : MonoBehaviour
{
    public float PlayerRotationY { get; set; }
    
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
