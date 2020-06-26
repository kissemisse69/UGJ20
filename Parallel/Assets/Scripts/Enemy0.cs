using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy0 : MonoBehaviour {

    [SerializeField]
    Sprite dim1, dim2;

    [SerializeField]
    float speed;
    [SerializeField]
    int dmg, fireRate;

    bool mode1 = true;

    // components
    SpriteRenderer _spriteRenderer;
    Animator _ani;
    NavMeshAgent _agent;

    // ai stuff
    [SerializeField]
    float detectionRange, shootRange;
    GameObject player;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); if(_spriteRenderer == null) Debug.Log("No Sprite Renderer Found " + gameObject.name);
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No Animator Found on " + gameObject.name);
        _agent = GetComponent<NavMeshAgent>(); if(_agent == null) Debug.Log("No navmeshagent found on " + gameObject.name);
        player = GameObject.FindGameObjectWithTag("Player"); if(player == null) Debug.Log("No player found on " + gameObject.name);
    }

    void Update() {
        transform.LookAt(player.transform); // always look at player

        if(mode1) { // mode 1
            if(Vector3.Distance(transform.position, player.transform.position) > shootRange) {
                float distanceToShootRange = Vector3.Distance(transform.position, player.transform.position) - shootRange;
                _agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + distanceToShootRange);
                Debug.Log("Shoot!");
            }
        } else { // mode 2

        }
    }

    public void ChangeMode() {
        mode1 = !mode1;
    }

    void Die() {
        // tell room director that your dead
    }

    // remove
    public void ChangeSprite(bool changeToDim2) {
        if(changeToDim2) { // change to dim 2
            _spriteRenderer.sprite = dim2;
        } else { // change to dim 1
            _spriteRenderer.sprite = dim1;
        }
    }
}
