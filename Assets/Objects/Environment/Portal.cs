using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextScene = "SampleScene";
    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Player") {
            //DontDestroyOnLoad(collision.gameObject);
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
