﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

    public MyButt background;
    public MyButt close;
    public MyButt replay;
    public MyButt next;
    public UILabel coins;
    public UILabel fruits;
    public UI2DSprite fun;

    // Use this for initialization
    void Start () {
        background.signalOnClick.AddListener(this.onClosePlay);
        close.signalOnClick.AddListener(this.onClosePlay);
        replay.signalOnClick.AddListener(this.onReplayPlay);
        next.signalOnClick.AddListener(this.onClosePlay);
        setsFilds();
    }

    private void setsFilds()
    {
        UI2DSprite[] crystals = fun.transform.GetComponentsInChildren<UI2DSprite>();
       
        SpriteRenderer[] crystalsFromGame = LevelController.current.getCrystals();
        for(int i = 0; i < crystalsFromGame.Length; ++i)
        {
            crystals[i+1].sprite2D = crystalsFromGame[i].sprite;
        }

        coins.text = "+" + LevelController.current.getCoinsOnThisLevel();
        fruits.text = LevelController.current.getFruitCount().ToString();
    }

    private void onReplayPlay()
    {
        SceneManager.LoadScene(LevelController.current.currentLevelName);
    }

    private void onClosePlay()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

}
