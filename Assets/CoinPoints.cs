using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPoints : MonoBehaviour
{
    float ySpeed = 20f;
    float disappearTimer = 0.5f;
    static TextMeshPro textMesh;
    Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, ySpeed) * Time.deltaTime;

        transform.rotation = Camera.main.transform.rotation;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            // test
        }

        textColor.a -= 1.5f * Time.deltaTime;
        textMesh.color = textColor;

        if (textMesh.color.a < 0)
        {
            Destroy(gameObject);
        }
    }

}
