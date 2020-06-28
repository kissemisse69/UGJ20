using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour {
    List<GameObject> enemySpawners = new List<GameObject>();
    [SerializeField]
    float enemy0Prob;
    [SerializeField]
    float enemy1Prob;
    [SerializeField]
    float enemy2Prob;
    [SerializeField]
    int amountOfEnemies;
    [SerializeField]
    GameObject enemy0;
    [SerializeField]
    GameObject enemy1;
    [SerializeField]
    GameObject enemy2;

    int deadEnemies = 0;
    void Start() {
        if(amountOfEnemies == 0) transform.parent.GetComponent<RoomDirector>().RoomCleared();
        float temp = enemy0Prob;
        enemy0Prob = 100 * enemy0Prob / (enemy0Prob + enemy1Prob + enemy2Prob);
        float temp1 = enemy1Prob;
        enemy1Prob = 100 * enemy1Prob / (temp + enemy1Prob + enemy2Prob);
        enemy2Prob = 100 * enemy2Prob / (temp + temp1 + enemy2Prob);
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).tag == "enemyspawners") enemySpawners.Add(transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < amountOfEnemies; i++) {
            if(enemySpawners.Count == 0) break;
            int rand = (int)Random.Range(0, enemySpawners.Count);
            GameObject spawner = enemySpawners[rand];
            enemySpawners.RemoveAt(rand);
            int enemyInt = Random.Range(0, 100);
            if(enemyInt < enemy0Prob) Instantiate(enemy0, spawner.transform.position, new Quaternion(1, 1, 1, 1), transform);
            else if(enemyInt < enemy1Prob) Instantiate(enemy1, spawner.transform.position, new Quaternion(1, 1, 1, 1), transform);
            else if(enemyInt < enemy2Prob) Instantiate(enemy2, spawner.transform.position, new Quaternion(1, 1, 1, 1), transform);

        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void EnemyDied() {
        deadEnemies++;
        if(deadEnemies >= amountOfEnemies) transform.parent.GetComponent<RoomDirector>().RoomCleared();

    }
}
