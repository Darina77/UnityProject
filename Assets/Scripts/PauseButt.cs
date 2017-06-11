using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButt : MonoBehaviour {

    public MyButt playButton;
    public GameObject settings;
    void Start()
    {
        playButton.signalOnClick.AddListener(this.onPlay);
    }
    void onPlay()
    {
        Time.timeScale = 0;
        GameObject obj = this.gameObject.AddChild(this.settings);
        //Розміщуємо в просторі
        obj.transform.position = this.transform.position;
        obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        //Запускаємо в рух
        SettingsPopUp settings = obj.GetComponent<SettingsPopUp>();
    }
}
