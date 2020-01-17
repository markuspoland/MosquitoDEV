using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDino : MonoBehaviour
{

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Destroy(rb, 5f);
            StartCoroutine(RemoveTag());
        }
    }

    IEnumerator RemoveTag()
    {
        yield return new WaitForSeconds(2f);
        gameObject.tag = "Untagged";
    }
}
