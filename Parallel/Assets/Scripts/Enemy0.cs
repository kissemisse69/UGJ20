using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy0 : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    int cooldown, hp;

    bool mode1 = true;
    bool alive = true;

    [SerializeField]
    GameObject fireballPrefab;

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

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); if(_spriteRenderer == null) Debug.Log("No Sprite Renderer Found " + gameObject.name);
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No Animator Found on " + gameObject.name);
        _agent = GetComponent<NavMeshAgent>(); if(_agent == null) Debug.Log("No navmeshagent found on " + gameObject.name);
        player = GameObject.FindGameObjectWithTag("Player"); if(player == null) Debug.Log("No player found on " + gameObject.name);

        player.GetComponent<Player>().changeMode.AddListener(ChangeMode);
    }

    void Update() {
        if(player != null) transform.LookAt(player.transform); // always look at player (camera)
        if(alive) {
            if(mode1) { // mode 1
                _ani.SetTrigger("Mode1");
                float distanceToShootRange = Vector3.Distance(transform.position, player.transform.position) - shootRange;

                if(Vector3.Distance(transform.position, player.transform.position) > shootRange) {
                    if(atLocation) {
                        _agent.destination = transform.TransformPoint(player.transform.position.x + Random.Range(-shootRange, shootRange), player.transform.position.y, player.transform.position.y + Random.Range(-shootRange, shootRange));
                        atLocation = false;
                    } 

                } else if(Vector3.Distance(transform.position, player.transform.position) < avoidRange && atLocation) {
                    _agent.destination = transform.position + transform.forward * Random.Range(-3, -6);
                    atLocation = false;
                } 

                if(Vector3.Distance(_agent.destination, transform.position) < 3) atLocation = true;

                if(Vector3.Distance(transform.position, player.transform.position) > avoidRange && Vector3.Distance(transform.position, player.transform.position) < shootRange) {
                    if(canFire) Attack();
                }

            } else { // mode 2
                _ani.SetTrigger("Mode2");
                if(atLocation) {
                    _agent.destination = player.transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
                    atLocation = false;
                } else if(Vector3.Distance(_agent.destination, transform.position) < 3) atLocation = true;
            }
        }
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }

    void Attack() {
        GameObject fireball = Instantiate(fireballPrefab, transform.position + transform.forward * 2, transform.rotation, null);
        canFire = false;
        StartCoroutine("AttackCooldown");
        _ani.SetTrigger("Attack");
    }

    public void ChangeMode() {
        mode1 = !mode1;
    }

    void Die() {
        // TODO tell room director that your dead
        _agent.destination = transform.position;
        _agent.isStopped = true;
        _ani.SetBool("Dead", true);
        alive = false;
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Player Projectile":
                if(mode1) {
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
