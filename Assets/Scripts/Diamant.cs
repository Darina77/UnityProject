using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamant : Collectable
{
    public static int  count = 0;
    public int id;

    void Start()
    {
        id = count;
        ++count;
        
    }

    public static void setCountZero()
    {
        count = 0;
    }


    public int getId() { return id; }
    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addDiamant(this);
        this.CollectedHide();
    }
}
