using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    bool hide = false;
        protected virtual void OnRabitHit(HeroRabit rabit)
        {
        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!this.hide)
            {
                HeroRabit rabit = collider.GetComponent<HeroRabit>();
                if (rabit != null)
                {
                    this.OnRabitHit(rabit);
                }
            }
        }
        public void CollectedHide()
        {
            Destroy(this.gameObject);
            hide = true;
        }
    
}
