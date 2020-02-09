using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipVideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.Menu);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.Menu);
        }
    }
}
