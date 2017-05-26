using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamant : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addDiamant(1);
        this.CollectedHide();
    }
}
