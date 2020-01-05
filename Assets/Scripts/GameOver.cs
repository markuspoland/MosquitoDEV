using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
     
   
    
    public static GameObject joystickVert;
    public static Image gameOverImage;
    Button restartButton;
    public Image[] gameUI;
    public static Image[] imageUI;

    // Start is called before the first frame update
    void Start()
    {
        
        gameOverImage = GameObject.FindGameObjectWithTag("Gameover").GetComponent<Image>();
        //restartButton = GameObject.FindGameObjectWithTag("RestartButton").GetComponent<Button>();
        //restartButton.gameObject.SetActive(true);
        
        joystickVert = GameObject.FindGameObjectWithTag("JoystickVert");
        imageUI = gameUI;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    public static void GameIsOver()
    {
        if (gameOverImage.color.a < 1f)
        {
            foreach(Image img in imageUI)
            {
                img.gameObject.SetActive(false);
            }
            joystickVert.SetActive(false);
            var tempColor = gameOverImage.color;
            tempColor.a += 0.2f * Time.deltaTime;
            gameOverImage.color = tempColor;
        }
    }

}
