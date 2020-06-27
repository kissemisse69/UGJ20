using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {

    [SerializeField]
    float speed, downSpeed;
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
    Collider collider;

    // ai stuff
    [SerializeField]
    float avoidRange, shootRange;
    GameObject player;
    bool atLocation = true;
    bool canFire = true;
    bool isUp = true;
    bool ongoingRoutine;
    bool onCooldown;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); if(_spriteRenderer == null) Debug.Log("No sprite renderer found on " + name);
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No animator found on " + name);
        _agent = GetComponent<NavMeshAgent>(); if(_agent == null) Debug.Log("No navmeshagent found on " + name);
        player = GameObject.FindGameObjectWithTag("Player"); if(player == null) Debug.Log("No player found on " + name);
        collider = GetComponent<Collider>(); if(collider == null) Debug.Log("No collider found on " + name);

        player.GetComponent<Player>().changeMode.AddListener(ChangeMode);
    }

    void Update() {
        transform.LookAt(player.transform);

        if(alive) {
            if(atLocation) {
                if(isUp && !ongoingRoutine) { // is up
                    if(Vector3.Distance(transform.position, player.transform.position) > shootRange) {
                        StartCoroutine("GoDown");
                    } else if(Vector3.Distance(transform.position, player.transform.position) < avoidRange) {
                        StartCoroutine("GoDown");
                    } else {
                        if(canFire && !onCooldown) Attack();
                    }

                } else if(!ongoingRoutine) { // is down
                    StartCoroutine("GoUp");
                }

            } else {
                if(Vector3.Distance(transform.position, _agent.destination) < 3) {
                    atLocation = true;
                }
            }
        }
    }

    IEnumerator GoUp() {
        collider.enabled = true;
        ongoingRoutine = true;
        _ani.SetBool("Down", false);
        _agent.isStopped = true;
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + (float)0.2);
        isUp = true;
        canFire = true;
        ongoingRoutine = false;
    }

    IEnumerator GoDown() {
        ongoingRoutine = true;
        _ani.SetBool("Down", true);
        atLocation = false;
        isUp = false;
        canFire = false;
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + (float)0.2);
        _agent.isStopped = false;
        _agent.destination = transform.TransformPoint(player.transform.position.x + Random.Range(-shootRange, shootRange), 0, player.transform.position.z + Random.Range(-shootRange, shootRange));
        ongoingRoutine = false;
        collider.enabled = false;
    }

    public void ChangeMode() {
        mode1 = !mode1;
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }

    void Attack() {
        GameObject fireball = Instantiate(projectilePrefab, transform.position + transform.forward * 2, transform.rotation, null);
        canFire = false;
        StartCoroutine("AttackCooldown");
        _ani.SetTrigger("Attack");
    }

    void Die() {
        // TODO tell room director that your dead
        _agent.isStopped = true;
        _agent.destination = transform.position;
        _ani.SetBool("Dead", true);
        alive = false;
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Player Projectile":
                if(isUp) {
                    hp -= other.GetComponent<Projectile>().dmg;
                    if(hp <= 0) Die();
                    Debug.Log(hp);
                    Destroy(other.gameObject);
                }
                
                break;

            default:
                break;
        }
    }
}
