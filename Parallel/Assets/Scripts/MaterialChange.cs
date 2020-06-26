using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour {

    [SerializeField]
    Material dim1, dim2;

    public void ChangeMaterial(bool changeToDim2) {
        if(changeToDim2) { // change to dim 2
            GetComponent<MeshRenderer>().material = dim2;
        } else { // change to dim 1
            GetComponent<MeshRenderer>().material = dim1;
        }
    }
}
