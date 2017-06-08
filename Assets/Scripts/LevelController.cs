using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    public UILabel coinsLabel;
    public UILabel fruitsLabel;
    Vector3 startingPosition;
    int coins = 0;
    int fruits = 0;
    int lifes = 3;
    public UI2DSprite heartSprites;
    public UI2DSprite crystalSprites;


    void Awake()
    {
        current = this;
    }

    

    public void setStartPosition(Vector3 pos)
    {
        
        this.startingPosition = pos;
    }
    public void onRabitDeath(HeroRabit rabit)
    {
        if (lifes > 0)
        {
            --lifes;
            SpriteRenderer sr = heartSprites.gameObject.GetComponentsInChildren<SpriteRenderer>()[lifes];
            sr.sprite = Resources.Load<Sprite>("life-used");
            //При смерті кролика повертаємо на початкову позицію
            rabit.transform.position = this.startingPosition;
        }
        if (lifes == 0) SceneManager.LoadScene("ChooseLevel");
    }

    public void addCoins(int coin)
    {
        this.coins += coin;
        string coinsNum = coins.ToString();
        string forPast = "";
        for (int i = 0; i < 4 - coinsNum.Length; i++) {
            forPast += "0";
        }
        forPast += coinsNum;
        coinsLabel.text = forPast;
    }

    public void addFruit(int fruit)
    {

        this.fruits += fruit;
        fruitsLabel.text = fruits.ToString();
    }

    public void addDiamant(Diamant diam)
    {
        
        SpriteRenderer sr = crystalSprites.gameObject.GetComponentsInChildren<SpriteRenderer>()[diam.getId()];
        sr.gameObject.transform.localScale = new Vector3(100, 100, 0);
        sr.sprite = diam.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
