using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class Sharing : MonoBehaviour
{
    string shareMessage;
    public string shareURL;
    public eShareOptions[] excludedOptions;

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject continuePanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FinishedSharing(eShareResult result)
    {
        Debug.Log("Finished Sharing: " + result);

        if (menuPanel.activeSelf)
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.ADScene1);
        }

        if (continuePanel.activeSelf)
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.End);
        }
    }
    public void ShareURLUsingShareSheet()
    {
        shareMessage = "I have just completed the level in Suck 'em All with " + GameManager.Instance.highscore.ToString() + " points! Want to try and beat me? :)";
        ShareSheet shareSheet = new ShareSheet();
        shareSheet.Text = shareMessage;
        shareSheet.URL = shareURL;

        shareSheet.ExcludedShareOptions = excludedOptions;

        NPBinding.UI.SetPopoverPointAtLastTouchPosition();
        NPBinding.Sharing.ShowView(shareSheet, FinishedSharing);
    }
}
