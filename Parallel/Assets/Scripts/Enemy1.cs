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
    [SerializeField]
    float avoidRange, shootRange;
    GameObject player;
    bool atLocation = true;
    bool canFire = true;
    bool isUp;

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
                    StartCoroutine("GoUp");
                } else {
                    if(canFire) Attack();
                }

            } else { // is down
                GoUp();
            }

        } else {
            if(Vector3.Distance(transform.position, player.transform.position) < 3) {
                atLocation = true;
            }
        }
    }

    IEnumerator GoUp() {
        // TODO set animation
        yield return new WaitForSeconds(GetComponent<Animation>().GetClip("GoingUp").length);
        isUp = true;
    }

    IEnumerator GoDown() {
        // TODO set animation
        atLocation = false;
        isUp = false;
        yield return new WaitForSeconds(GetComponent<Animation>().GetClip("GoingDown").length);
        _agent.destination = transform.TransformPoint(player.transform.position.x + Random.Range(-shootRange, shootRange), player.transform.position.y, player.transform.position.y + Random.Range(-shootRange, shootRange));
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
