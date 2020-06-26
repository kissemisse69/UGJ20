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
    float avoidRange, shootRange;
    GameObject player;
    bool atLocation = true;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); if(_spriteRenderer == null) Debug.Log("No Sprite Renderer Found " + gameObject.name);
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No Animator Found on " + gameObject.name);
        _agent = GetComponent<NavMeshAgent>(); if(_agent == null) Debug.Log("No navmeshagent found on " + gameObject.name);
        player = GameObject.FindGameObjectWithTag("Player"); if(player == null) Debug.Log("No player found on " + gameObject.name);
    }

    void Update() {
        transform.LookAt(player.transform); // always look at player

        if(mode1) { // mode 1
            float distanceToShootRange = Vector3.Distance(transform.position, player.transform.position) - shootRange;
            
            if(Vector3.Distance(transform.position, player.transform.position) > shootRange && atLocation) {
                //_agent.destination = transform.TransformPoint(transform.position) + transform.forward * distanceToShootRange;
                _agent.destination = transform.TransformPoint(player.transform.position.x + Random.Range(-shootRange, shootRange), player.transform.position.y, player.transform.position.y + Random.Range(-shootRange, shootRange));
                atLocation = false;

            } else if(Vector3.Distance(transform.position, player.transform.position) < avoidRange && atLocation) {
                _agent.destination = transform.position + transform.forward * Random.Range(-3, -6);
                atLocation = false;
            }
            

            if(Vector3.Distance(transform.position, player.transform.position) < shootRange && Vector3.Distance(transform.position, player.transform.position) > avoidRange) {
                Debug.Log("Shoot!");
            }
            

            if(Vector3.Distance(_agent.destination, transform.position) < 3) atLocation = true;

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
