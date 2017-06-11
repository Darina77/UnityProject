using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseScreen : MonoBehaviour {

    public MyButt background;
    public MyButt close;
    public MyButt replay;
    public MyButt menu;
  
    public UI2DSprite fun;

    // Use this for initialization
    void Start()
    {
        background.signalOnClick.AddListener(this.onClosePlay);
        close.signalOnClick.AddListener(this.onClosePlay);
        replay.signalOnClick.AddListener(this.onReplayPlay);
        menu.signalOnClick.AddListener(this.onClosePlay);
        setsFilds();
    }

    private void setsFilds()
    {
        UI2DSprite[] crystals = fun.transform.GetComponentsInChildren<UI2DSprite>();
   
        SpriteRenderer[] crystalsFromGame = LevelController.current.getCrystals();
        for (int i = 0; i < crystalsFromGame.Length; ++i)
        {
            crystals[i + 1].sprite2D = crystalsFromGame[i].sprite;
        }

       
    }

    private void onReplayPlay()
    {
        SceneManager.LoadScene(LevelController.current.currentLevelName);
    }

    private void onClosePlay()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
