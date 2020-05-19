using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour
{
    public Button restartButton;
    //public Button reviveButton;

    private void Start()
    {
        restartButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (MosqitController.playerDead)
        {
            restartButton.gameObject.SetActive(true);
            
        }   
    }
    // Start is called before the first frame update
    public void RestartGame()
    {
        GameManager.Instance.ChangeScene(GameManager.GameScene.ADScene2);
    }
}
