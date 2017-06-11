using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHeart : Collectable {


    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addLife(this);
        this.CollectedHide();
    }
}
