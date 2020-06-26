using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    [SerializeField]
    Sprite dim1, dim2;

    [SerializeField]
    float speed;
    [SerializeField]
    int dmg, fireRate;

    bool mode1;

    // components
    SpriteRenderer _spriteRenderer;
    Animator _ani;
 
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); if(_spriteRenderer == null) Debug.Log("No Sprite Renderer Found");
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No Animator Found");
    }

    
    void Update() {
        
    }

    public void ChangeSprite(bool changeToDim2) {
        if(changeToDim2) { // change to dim 2
            _spriteRenderer.sprite = dim2;
        } else { // change to dim 1
            _spriteRenderer.sprite = dim1;
        }
    }
}
