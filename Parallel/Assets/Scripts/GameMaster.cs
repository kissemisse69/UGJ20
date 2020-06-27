using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    
    GameObject[] showOnGameOver;
    GameObject[] showOnPause;
    void Start() {
        showOnGameOver = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        showOnPause = GameObject.FindGameObjectsWithTag("ShowOnPause");
        foreach(GameObject ob in showOnGameOver) {
            ob.SetActive(false);
        }
        foreach(GameObject ob in showOnPause) {
            ob.SetActive(false);
        }
        Cursor.visible = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }
    public void GameOver() {
        Time.timeScale = 0;
        foreach(GameObject ob in showOnGameOver) ob.SetActive(true);
    }

    public void Pause() {
        if(Time.timeScale == 1) {
            Time.timeScale = 0;
            foreach(GameObject ob in showOnPause) ob.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Time.timeScale = 1;
            foreach(GameObject ob in showOnPause) ob.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
    }
    public void Quit() {
        Application.Quit();
    }
}
