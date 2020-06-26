using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    bool inDim1 = true;

    [SerializeField]
    Material dim1mat, dim2mat;

    void Start() {
        
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
            }

            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                // enemies

            }

            inDim1 = false;

        } else { // change to dim1

        }
    }
}
