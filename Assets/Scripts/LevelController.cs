using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    public UILabel coinsLabel;
    public UILabel fruitsLabel;
    public GameObject looseScreen;
    
    Vector3 startingPosition;
    LevelStats stat;
    public const int allFruits = 10;
    int allDiamands = 3;
    int coins = 0;
    int fruits = 0;
    int lifes = 3;
    int coinsOnThisLevel = 0;
    public UI2DSprite heartSprites;
    public UI2DSprite crystalSprites;
    public string currentLevelName;

    void Awake()
    {
        current = this;
        string str = PlayerPrefs.GetString(currentLevelName, null);
        Debug.Log(str);
        this.coins = PlayerPrefs.GetInt("coins", 0);
        this.stat = JsonUtility.FromJson<LevelStats>(str);
        Fruit.setCountZero();
        Diamant.setCountZero();
        if (stat == null)
        {
            this.stat = new LevelStats();
        }
    }

    void Start()
    {
        addCoins(0);
    }

  
    public LevelStats getStats()
    {
        return stat;
    }

    public int getCoins()
    {
        return coins;
    }
    public void setStartPosition(Vector3 pos)
    {
        
        this.startingPosition = pos;
    }
    public void onRabitDeath(HeroRabit rabit)
    {
        if (heartSprites == null)
        {
            rabit.transform.position = this.startingPosition;
            return;
        }
        if (lifes > 0)
        {
            --lifes;
            SpriteRenderer sr = heartSprites.gameObject.GetComponentsInChildren<SpriteRenderer>()[lifes];
            sr.sprite = Resources.Load<Sprite>("life-used");
            //При смерті кролика повертаємо на початкову позицію
            rabit.transform.position = this.startingPosition;
        }
        if (lifes == 0)
        {
          
            GameObject obj = GameObject.Find("UI Root").AddChild(this.looseScreen);
          
            obj.transform.position = this.transform.position;
            obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
          
            LooseScreen looseScreen = obj.GetComponent<LooseScreen>();
        }
    }

    public void addCoins(int coin)
    {
        this.coins += coin;
        this.coinsOnThisLevel += coin;
        string coinsNum = coins.ToString();
        string forPast = "";
        for (int i = 0; i < 4 - coinsNum.Length; i++) {
            forPast += "0";
        }
        forPast += coinsNum;
        coinsLabel.text = forPast;
    }

    public int getCoinsOnThisLevel()
    {
        return coinsOnThisLevel;
    }

    public int getFruitCount()
    {
        return fruits;
    }

    public void addFruit(Fruit fruit)
    {
        this.fruits ++;
        fruitsLabel.text = fruits.ToString();
     
        stat.collectedFruits[fruit.getId()] = true;
        if (allFruits == fruits) stat.hasAllFruits = true;
    }

    public void addDiamant(Diamant diam)
    {
        
        SpriteRenderer sr = crystalSprites.gameObject.GetComponentsInChildren<SpriteRenderer>()[diam.getId()];
        sr.gameObject.transform.localScale = new Vector3(100, 100, 0);
        sr.sprite = diam.gameObject.GetComponent<SpriteRenderer>().sprite;
        if (allDiamands > 0) allDiamands--;
        if(allDiamands <= 0) stat.hasCrystals = true;
    }

    public SpriteRenderer[] getCrystals()
    {
        return crystalSprites.gameObject.GetComponentsInChildren<SpriteRenderer>();
    }
}
