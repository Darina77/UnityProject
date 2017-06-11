using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour {

    public MyButt background;
    public MyButt close;
    public MyButt sound;
    public MyButt volume;
	// Use this for initialization
	void Start ()
    {
        setImages();
        background.signalOnClick.AddListener(this.onClosePlay);
        close.signalOnClick.AddListener(this.onClosePlay);
        sound.signalOnClick.AddListener(this.onSoundPlay);
        volume.signalOnClick.AddListener(this.onVolumePlay);
    }
	
    void Update()
    {
        setImages();
    }

    private void setImages()
    {
        if (SoundManager.Instance.isSoundOn())
        {
          

            sound.gameObject.GetComponent<UI2DSprite>().sprite2D = 
                Resources.Load<Sprite>("sound-on");
        }
        else
        {
            sound.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("sound-off");
        }
        if (SoundManager.Instance.isVolumeOn())
        {
            volume.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("music-on");
        }
        else
        {
            volume.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("music-off");
        }
    }

    void onClosePlay()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

    void onSoundPlay()
    {
        if (SoundManager.Instance.isSoundOn()) {
            SoundManager.Instance.setSoundOn(false);
           
        }
        else
        {
            SoundManager.Instance.setSoundOn(true);
            
        }
       
    }

    void onVolumePlay()
    {
        if (SoundManager.Instance.isVolumeOn())
        {
            SoundManager.Instance.setVolumeOn(false);
            
        }
        else
        {
            SoundManager.Instance.setVolumeOn(true);
        }
    }
}
