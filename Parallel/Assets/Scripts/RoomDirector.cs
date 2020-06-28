using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDirector : MonoBehaviour {
    
    [SerializeField]
    bool stolpeGodDim1;
    [SerializeField]
    bool stolpeGod2Dim1;
    [SerializeField]
    bool stolpeOndDim1;
    GameObject stolpeGod;
    GameObject stolpeOnd;
    GameObject stolpeGod2;

    void Start() {
        
        stolpeGod = GameObject.Find("Stolpe God 1");
        stolpeGod2 = GameObject.Find("God del 2");
        stolpeOnd = GameObject.Find("Stolpe God 2");
        if(!stolpeGodDim1) stolpeGod.SetActive(false);
        if(!stolpeGod2Dim1) stolpeGod2.SetActive(false);
        if(!stolpeOndDim1) stolpeOnd.SetActive(false);
        GameObject.Find("Player").GetComponent<Player>().changeMode.AddListener(ChangeDim);
    }

    void Update() {

    }

    void ChangeDim() {
        if(stolpeGod.activeSelf) stolpeGod.SetActive(false);
        else stolpeGod.SetActive(true);
        if(stolpeGod2.activeSelf) stolpeGod2.SetActive(false);
        else stolpeGod2.SetActive(true);
        if(stolpeOnd.activeSelf) stolpeOnd.SetActive(false);
        else stolpeOnd.SetActive(true);
    }
}