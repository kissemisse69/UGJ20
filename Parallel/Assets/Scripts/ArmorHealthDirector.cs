using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorHealthDirector : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    List<GameObject> itemSpawners;
    [SerializeField]
    float healthProb;
    [SerializeField]
    float armourProb;
    [SerializeField]
    int amountOfPickUps;

    [SerializeField]
    GameObject armourPrefab, healthPrefab;

    void Start() {

        itemSpawners = new List<GameObject>();

        float temp = armourProb;
        armourProb = 100 * armourProb / (armourProb + healthProb);
        healthProb = 100 * healthProb / (temp + healthProb);
        if(transform.childCount > 0) {
            for(int i = 0; i < transform.childCount; i++) {
                if(transform.GetChild(i).tag == "itemSpawner") itemSpawners.Add(transform.GetChild(i).gameObject);
            }
        }


        for(int i = 0; i < amountOfPickUps; i++) {

            int rand = (int)Random.Range(0, itemSpawners.Count);
            GameObject spawner = itemSpawners[rand];
            itemSpawners.RemoveAt(rand);
            int pickUpInt = Random.Range(0, 100);
            if(pickUpInt < armourProb) Instantiate(armourPrefab, spawner.transform.position, new Quaternion(1, 1, 1, 1));
            else Instantiate(healthPrefab, spawner.transform.position, new Quaternion(1, 1, 1, 1));

        }
    }
}
