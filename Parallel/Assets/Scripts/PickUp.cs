using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    int value;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public int Value {
        get { return value; }
    }
}
