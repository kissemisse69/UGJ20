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
    int amountOfEnemies;
    void Start() {
        float temp = armourProb;
        armourProb = 100 * armourProb / (armourProb + healthProb);
        healthProb = 100 * healthProb / (temp + healthProb);
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).tag == "itemSpawner") itemSpawners.Add(transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < amountOfPickUps; i++) {

            int rand = (int)Random.Range(0, itemSpawners.Count + 1);
            GameObject spawner = itemSpawners[rand];
            itemSpawners.RemoveAt(rand);
            int pickUpInt = Random.Range(0, 100);
            if(pickUpInt < armourProb) Instantiate(Resources.Load("Armour") as GameObject, spawner.transform.position, new Quaternion(1, 1, 1, 1));
            else Instantiate(Resources.Load("Health") as GameObject, spawner.transform.position, new Quaternion(1, 1, 1, 1));

        }




    }

    // Update is called once per frame
    void Update() {

    }
}
