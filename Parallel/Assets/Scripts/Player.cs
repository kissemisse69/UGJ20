using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    bool inDim1 = true;

    [SerializeField]
    int maxHealth;

    [SerializeField]
    int maxArmour;

    int health;
    int armour;

    void Start() {
        health = maxHealth;
        armour = maxArmour;
    }

    void Update() {
        

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            ChangeDimension();
        }
    }

    void ChangeDimension() {
        if(inDim1) { // change to dim2
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Static Objects")) {
                // static objects like ground and walls
                obj.GetComponent<MaterialChange>().ChangeMaterial(true);
            }

            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Changable Objects")) {
                // object that change depending on dimensions
                obj.GetComponent<MaterialChange>().ChangeMaterial(true);
            }

            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                // enemies
                //enemy.GetComponent<>().(true);
            }

            inDim1 = false;

        } else { // change to dim1

        }
    }

    private void OnCollisionEnter(Collision collision) {
        tag = collision.gameObject.transform.tag;
        switch(tag) {
            case "Armour":
                if(armour < maxArmour) {
                    armour += collision.gameObject.GetComponent<PickUp>().Value;
                    if(armour > maxArmour) armour = maxArmour;
                    Destroy(collision.gameObject);
                }
                break;
            case "Health":
                if(health < maxHealth) {
                    health += collision.gameObject.GetComponent<PickUp>().Value;
                    if(health > maxHealth) health = maxHealth;
                    Destroy(collision.gameObject);
                }
                break;

            default:
                break;
        }
    }
}
