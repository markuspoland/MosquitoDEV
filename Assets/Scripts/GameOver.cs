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
    public GameObject[] gameUI;
    [SerializeField] GameObject backToMenu;
    [SerializeField] GameObject settingsButton;
    public static GameObject[] imageUI;
    public static GameObject menuButton;
    public static GameObject settingsBtn;

    // Start is called before the first frame update
    void Start()
    {
        
        gameOverImage = GameObject.FindGameObjectWithTag("Gameover").GetComponent<Image>();
        //restartButton = GameObject.FindGameObjectWithTag("RestartButton").GetComponent<Button>();
        //restartButton.gameObject.SetActive(true);
        
        joystickVert = GameObject.FindGameObjectWithTag("JoystickVert");
        imageUI = gameUI;
        menuButton = backToMenu;
        settingsBtn = settingsButton;
    }

    
    void Update()
    {
        
    }
        
    public static void GameIsOver()
    {
        settingsBtn.SetActive(false);

        if (gameOverImage.color.a < 1f)
        {
            foreach(GameObject img in imageUI)
            {
                img.SetActive(false);
            }
            joystickVert.SetActive(false);
            var tempColor = gameOverImage.color;
            tempColor.a += 0.2f * Time.deltaTime;
            gameOverImage.color = tempColor;
            menuButton.SetActive(true);
            MosqitController mosqitController = GameObject.FindGameObjectWithTag("Player").GetComponent<MosqitController>();
            mosqitController.DeadSound();
            
        }
    }

}
