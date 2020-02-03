using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDino : MonoBehaviour
{

    Rigidbody rb;
    Collider col;
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
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        col.enabled = true;
        gameObject.tag = "Untagged";
    }
}
