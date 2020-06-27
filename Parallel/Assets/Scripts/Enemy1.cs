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

    // ai stuff
    [SerializeField]
    float avoidRange, shootRange;
    GameObject player;
    bool atLocation = true;
    bool canFire = true;
    bool isUp = true;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); if(_spriteRenderer == null) Debug.Log("No sprite renderer found on " + name);
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No animator found on " + name);
        _agent = GetComponent<NavMeshAgent>(); if(_agent == null) Debug.Log("No navmeshagent found on " + name);
        player = GameObject.FindGameObjectWithTag("Player"); if(player == null) Debug.Log("No player found on " + name);

        player.GetComponent<Player>().changeMode.AddListener(ChangeMode);
    }
 
    void Update() {
        transform.LookAt(player.transform);

        if(atLocation) {
            if(isUp) { // is up
                if(Vector3.Distance(transform.position, player.transform.position) > shootRange) {
                    StartCoroutine("GoDown");
                } else if(Vector3.Distance(transform.position, player.transform.position) < avoidRange) {
                    StartCoroutine("GoDown");
                } else {
                    if(canFire) Attack();
                    Debug.Log("vibing");
                }

            } else { // is down
                StartCoroutine("GoUp");
            }

        } else {
            if(Vector3.Distance(transform.position, _agent.destination) < 3) {
                atLocation = true;
            }
        }
    }

    IEnumerator GoUp() {
        Debug.Log("Go up");
        _ani.SetBool("Down", false);
        _agent.isStopped = true;
        yield return new WaitForSeconds(GetComponent<Animator>().GetAnimatorTransitionInfo(0).duration);
        isUp = true;
        canFire = true;
    }

    IEnumerator GoDown() {
        Debug.Log("Go down");
        _ani.SetBool("Down", true);
        atLocation = false;
        isUp = false;
        canFire = false;
        yield return new WaitForSeconds(GetComponent<Animator>().GetAnimatorTransitionInfo(0).duration);
        _agent.isStopped = false;
        _agent.destination = transform.TransformPoint(player.transform.position.x + Random.Range(-shootRange, shootRange), 0, player.transform.position.z + Random.Range(-shootRange, shootRange));
    }

    public void ChangeMode() {
        mode1 = !mode1;
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }

    void Attack() {
        GameObject fireball = Instantiate(projectilePrefab, transform.position, transform.rotation, null);
        canFire = false;
        StartCoroutine("AttackCooldown");
        _ani.SetTrigger("Attack");
    }

    void Die() {
        // TODO tell room director that your dead
        _agent.isStopped = true;
        _ani.SetBool("Dead", true);
        alive = false;
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Player Projectile":
                if(isUp) {
                    hp -= other.GetComponent<Projectile>().dmg;
                    if(hp <= 0) Die();
                }
                Destroy(other.gameObject);
                break;

            default:
                break;
        }
    }
}
