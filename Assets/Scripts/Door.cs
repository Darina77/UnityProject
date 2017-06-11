using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour {

    public string levelName;
    public Door nextDoor;
    public bool open;
    LevelStats stats;

    void Awake()
    {
        string str = PlayerPrefs.GetString(levelName, null);
        stats = JsonUtility.FromJson<LevelStats>(str);
        if (stats == null)
        {
            stats = new LevelStats();
        }
       
        if (stats.levelPassed)
        {
            this.transform.Find("mark").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            if (nextDoor != null) nextDoor.open = true;
        }
       
    }

    void Start()
    {
        if (open)
            this.transform.Find("lock").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);

        if (stats.hasCrystals)
        {
            Transform crystal = this.transform.Find("crystal");
            crystal.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("crystal-1");
            crystal.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        }
        if (stats.hasAllFruits)
        {
            Transform fruit = this.transform.Find("fruit");
            fruit.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("fruit-icon");
            fruit.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        }
    }

   

    void OnTriggerEnter2D(Collider2D collider)
    {
         HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null && open)
        {
            SceneManager.LoadScene(levelName);
          
        }      
    }
}
