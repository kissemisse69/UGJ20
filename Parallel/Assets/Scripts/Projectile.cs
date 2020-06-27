using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    int lifetime;

    public int dmg;

    private void Start() {
        StartCoroutine("DestroyTimer");
    }

    void FixedUpdate() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Update() {
        transform.GetChild(0).transform.LookAt(Camera.main.transform);
        
    }

    IEnumerator DestroyTimer() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
