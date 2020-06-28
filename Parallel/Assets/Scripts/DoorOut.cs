using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOut : MonoBehaviour {
    // Start is called before the first frame update
    bool doorOpening = false;
    [SerializeField]
    float speed;
    [SerializeField]
    float openingDist;
    void Start() {
        //Fick inte eventen att funka
        //GameObject.Find("Room Director").GetComponent<RoomDirector>().roomCleared.AddListener(RoomCleared);
        //GameObject.Find("Room Director").GetComponent<RoomDirector>().roomCleared.GetPersistentEventCount();

    }

    // Update is called once per frame
    void Update() {
        if(doorOpening) {

            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
    }

    public void RoomCleared() {
        doorOpening = true;
        StartCoroutine("DoorOpening");
    }

    IEnumerator DoorOpening() {
        yield return new WaitForSeconds(openingDist / speed);
        doorOpening = false;
    }
}
