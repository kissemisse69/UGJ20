using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float cooldown;
    bool canFire = true;

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            if(canFire) {
                GameObject bullet = Instantiate(projectile, transform.position + transform.forward, Camera.main.transform.rotation, null) as GameObject;
                bullet.tag = "Player Projectile";
                canFire = false;
                StartCoroutine("AttackCooldown");
            }
        }
    }

    IEnumerator AttackCooldown() {
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
}