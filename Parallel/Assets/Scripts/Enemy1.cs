using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    int cooldown, hp;

    bool mode1 = true;
    bool alive = true;

    [SerializeField]
    GameObject projectilePrefab;

    // components
    SpriteRenderer _spriteRenderer;
    Animator _ani;
    NavMeshAgent _agent;

    // ai stuff

    void Start() {
        
    }
 
    void Update() {
        
    }
}
