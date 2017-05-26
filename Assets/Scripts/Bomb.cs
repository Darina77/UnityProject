using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        this.CollectedHide();
        rabit.removeOneHealth();
    }
}
