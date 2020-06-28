using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllwaysFacePlayer : MonoBehaviour {
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
