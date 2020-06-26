using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    Sprite dim1, dim2;

    void Start() {
        
    }

    
    void Update() {
        
    }

    public void ChangeSprite(bool changeToDim2) {
        if(changeToDim2) { // change to dim 2
            GetComponent<SpriteRenderer>().sprite = dim2;
        } else { // change to dim 1
            GetComponent<SpriteRenderer>().sprite = dim1;
        }
    }
}
