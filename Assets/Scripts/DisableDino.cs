using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDino : MonoBehaviour
{

    Rigidbody rb;
    Collider col;
    [SerializeField] GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            GameObject explosion = Instantiate(boom, new Vector3(177.14f, -62.04f, 128.93f), Quaternion.identity) as GameObject;
            Destroy(gameObject);

            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            if (levelManager)
            {
                levelManager.CompleteLevel1Objective("Dino down");
            }
            //StartCoroutine(RemoveTag());
        }
    }

    //IEnumerator RemoveTag()
    //{
    //    col.enabled = false;
    //    yield return new WaitForSeconds(2f);
    //    col.enabled = true;
    //    gameObject.tag = "Untagged";
    //}
}
