using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
