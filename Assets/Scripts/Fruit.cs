using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
    
    private static int count = 0;
    public int id;

    
    void Start()
    { 
        id = count;
        count++;
    }

    public static void setCountZero()
    {
        count = 0;
    }
    
    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addFruit(this);
        this.CollectedHide();
    }

    public int getId()
    {
        return id;
    }
}
