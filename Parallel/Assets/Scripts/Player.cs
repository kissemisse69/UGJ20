using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    bool inDim1 = true;

    [SerializeField]
    int maxHealth;

    [SerializeField]
    int maxArmour;

    int health;
    int armour;

    public UnityEvent changeMode = new UnityEvent();

    void Start() {
        if(changeMode == null) changeMode = new UnityEvent();
        health = maxHealth;
        armour = maxArmour;
    }

    void Update() {
        

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            ChangeDimension();
        }

        if(health <= 0) Death(); 
    }

    void ChangeDimension() {

        if(GameObject.FindGameObjectWithTag("Static Objects") != null) {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Static Objects");
            foreach(GameObject obj in objs) {
                obj.GetComponent<MaterialChange>().ChangeMaterial(!inDim1);
            }
        }

        if(GameObject.FindGameObjectWithTag("Changable Objects") != null) {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Changable Objects");
            foreach(GameObject obj in objs) {
                obj.GetComponent<MaterialChange>().ChangeMaterial(!inDim1);
            }
        }

        foreach(GameObject projectile in GameObject.FindGameObjectsWithTag("Enemy Projectile")) {
            if(projectile.name != "Player")
                Destroy(projectile);
        }

        if(changeMode == null) changeMode = new UnityEvent();
        changeMode.Invoke();

        inDim1 = !inDim1;
    }

    void Death() {
        // Game over n shit
        // TODO game director game over
        Debug.Log("DEAD");
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().GameOver();
    }

    public void TakeDmg(int dmg) {
        armour -= dmg;
        if(armour < 0) {
            health += armour;
            armour = 0;
            if(health <= 0) Death();
        }
    }

    private void OnTriggerEnter(Collider collider) {
        tag = collider.gameObject.transform.tag;
        switch(tag) {
            case "Armour":
                if(armour < maxArmour) {
                    armour += collider.gameObject.GetComponent<PickUp>().Value;
                    if(armour > maxArmour) armour = maxArmour;
                    Destroy(collider.gameObject);
                }
                break;

            case "Health":
                if(health < maxHealth) {
                    health += collider.gameObject.GetComponent<PickUp>().Value;
                    if(health > maxHealth) health = maxHealth;
                    Destroy(collider.gameObject);
                }
                break;

            case "Enemy Projectile":
                TakeDmg(collider.GetComponent<Projectile>().dmg);
                break;

            default:
                break;
        }
    }
}
