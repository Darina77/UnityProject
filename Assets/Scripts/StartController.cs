using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartController : MonoBehaviour {

    public MyButt playButton;
    public GameObject settingsScreen;
    public MyButt settings;

    void Start()
    {
        playButton.signalOnClick.AddListener(this.onPlay);
        settings.signalOnClick.AddListener(this.onSettings);
    }
    void onPlay()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

    void onSettings()
    {
        Time.timeScale = 0;
        GameObject obj = GameObject.Find("UI Root").AddChild(this.settingsScreen);
  
        obj.transform.position = this.transform.position;
        obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        
        SettingsPopUp settings = obj.GetComponent<SettingsPopUp>();
    }
}
