using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour {

    public string levelName;
    void OnTriggerEnter2D(Collider2D collider)
    {
         HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null)
        {
            SceneManager.LoadScene(levelName);
        }
        
        
    }
}
