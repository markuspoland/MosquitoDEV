using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    // Start is called before the first frame update
    public void OK()
    {
        GameManager.Instance.ChangeScene(GameManager.GameScene.Intro);
    }
}
