using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour {

    public string levelName;
    public string currentLevelName;
    
    public GameObject winScreen;

   
    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null)
        {
            
            LevelStats stats = LevelController.current.getStats();
          
            stats.levelPassed = true;
            string str = JsonUtility.ToJson(stats);
            Debug.Log(str);
            PlayerPrefs.SetString(currentLevelName, str);
            PlayerPrefs.SetInt("coins", LevelController.current.getCoins());
            PlayerPrefs.Save();
            GameObject obj = GameObject.Find("UI Root").AddChild(this.winScreen);

            obj.transform.position = this.transform.position;
            obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);

            WinScreen winScreen = obj.GetComponent<WinScreen>();
        }
    }
}
