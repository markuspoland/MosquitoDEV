using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaFinish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OK()
    {
        GameManager.Instance.ChangeScene(GameManager.GameScene.LastADScene);
    }
}
