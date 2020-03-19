using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("{{{{{{");
        if(collision.gameObject.name == "Player") {
            Debug.Log("Player Died");
            Player player = collision.gameObject.GetComponent<Player>() as Player;
            player.die();
        }
    }
}
