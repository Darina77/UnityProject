using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    bool is_sound_on = true;
    bool is_volume_on = true;
    public bool isSoundOn()
    {
        return this.is_sound_on;
    }

    public bool isVolumeOn()
    {
        return this.is_volume_on;
    }
    public void setSoundOn(bool val)
    {
        this.is_sound_on = val;
        PlayerPrefs.SetInt("sound", this.is_sound_on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void setVolumeOn(bool val)
    {
        this.is_volume_on = val;
        PlayerPrefs.SetInt("volume", this.is_volume_on ? 1 : 0);
        PlayerPrefs.Save();
    }
    SoundManager()
    {
        is_sound_on = PlayerPrefs.GetInt("sound", 1) == 1;
        is_volume_on = PlayerPrefs.GetInt("volume", 1) == 1;
    }
    public static SoundManager Instance = new SoundManager();
}