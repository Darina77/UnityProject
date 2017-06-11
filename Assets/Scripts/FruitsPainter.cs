using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsPainter : MonoBehaviour {

	// Use this for initialization

	void Start () {
        bool[] collected = LevelController.current.getStats().collectedFruits;
        Fruit[] all = this.GetComponentsInChildren<Fruit>();
        for(int i = 0; i < all.Length; ++i)
        {
            if (collected[all[i].getId()])
                all[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 128);
              
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
