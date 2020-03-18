using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public int damage = 1;

    public void takeDamage(int value) {
        health -= value;
        if(health <= 0) die();
    }
    public void die() {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}
