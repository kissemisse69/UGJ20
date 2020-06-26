using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            if(GameObject.FindGameObjectWithTag("Enemy") != null) {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(GameObject enemy in enemies) {
                    enemy.GetComponent<SpriteChange>().ChangeSprite(!inDim1);
                    // will probably have to change above ^
                }
            }

            inDim1 = !inDim1;
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

            default:
                break;
        }
    }
}
