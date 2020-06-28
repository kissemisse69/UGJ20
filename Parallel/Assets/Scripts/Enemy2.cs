using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy2 : MonoBehaviour {
    //Determines if close ranged in dim1 or two
    [SerializeField]
    bool closeDim1;
    bool dim1 = true;

    //Close attack related shit
    [SerializeField]
    int CloseAttackCooldown;
    [SerializeField]
    int dmg;
    [SerializeField]
    int knockBack;

    [SerializeField]
    float speed, cooldown;
    [SerializeField]
    int hp;

    bool mode1 = true;
    bool alive = true;

    [SerializeField]
    GameObject fireballPrefab;
    [SerializeField]
    float meleeDistance;

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
    bool canAttack = true;
    bool stuck = false;

    RaycastHit hit;

    void Start() {
        _agent = GetComponent<NavMeshAgent>(); if(_agent == null) Debug.Log("No navmeshagent found on " + gameObject.name);
        player = GameObject.FindGameObjectWithTag("Player"); if(player == null) Debug.Log("No player found on " + gameObject.name);
        _ani = GetComponent<Animator>(); if(_ani == null) Debug.Log("No animator found on " + gameObject.name);
        player.GetComponent<Player>().changeMode.AddListener(ChangeMode);
        hit = new RaycastHit();
    }

    // Update is called once per frame
    void Update() {
        if(player != null) transform.LookAt(player.transform);
        if(alive && !stuck) {
            if(dim1 && !closeDim1) {
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
                    if(canFire) RangedAttack();
                }
            } else {
                _agent.destination = player.transform.position;
                Ray ray = new Ray(transform.position, transform.forward);
                if(Physics.Raycast(ray, out hit, meleeDistance)) {
                    if(hit.transform == player.transform) {
                        StartCoroutine("CloseAttack");
                    }
                }
            }
        }
    }

    void ChangeMode() {
        dim1 = !dim1;
        _ani.SetBool("Mode1", !_ani.GetBool("Mode1"));
    }

    void RangedAttack() {
        _ani.SetTrigger("RangedAttack");
        GameObject lazer = Instantiate(fireballPrefab, transform.position + transform.forward * 2, transform.rotation, null);
        canFire = false;
        StartCoroutine("AttackCooldown");
    }

    void Die() {
        // TODO tell room director that youre dead
        _agent.isStopped = true;
        _ani.SetBool("Dead", true);
        alive = false;
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator CloseAttack() {
        _ani.SetTrigger("MeleeAttack");
        stuck = true;
        _agent.isStopped = true;
        yield return new WaitForSeconds(.5f);
        player.GetComponent<Player>().TakeDmg(dmg);
        Vector3 knockBackVector = new Vector3(transform.forward.x, 0, transform.forward.z) * knockBack;
        //Debug.Log(knockBackVector);
        player.GetComponent<CharacterController>().Move(knockBackVector);
        yield return new WaitForSeconds(CloseAttackCooldown);
        _agent.isStopped = false;
        stuck = false;
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(.2f);
        GameObject lazer1 = Instantiate(fireballPrefab, transform.position + transform.forward * 2, transform.rotation, null);
        yield return new WaitForSeconds(.2f);
        GameObject lazer2 = Instantiate(fireballPrefab, transform.position + transform.forward * 2, transform.rotation, null);
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }

    private void OnTriggerEnter(Collider other) {
        //if(other.name == "Player" && canAttack && !stuck && !(dim1 && !closeDim1)) StartCoroutine("CloseAttack");
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
