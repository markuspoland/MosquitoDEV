using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static List<Image> lives;
    public List<Image> livesImage;
    static Animator livesAnim;
    // Start is called before the first frame update
    void Awake()
    {
        lives = livesImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public static void TakeLife(int liveId)
    {
        livesAnim = lives[liveId].GetComponent<Animator>();
        livesAnim.SetTrigger("Died");
    }
}
