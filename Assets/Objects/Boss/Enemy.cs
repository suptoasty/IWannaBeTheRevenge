using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public int damage = 1;
    public GameObject projectile;

    public void FixedUpdate() {
        int rand = Mathf.RoundToInt(Random.Range(0.0f, Mathf.Infinity));
        if(rand%3==1) {
            move(); 
        } else if(rand%3==0) {
            shoot();
        }
    }

    public void move() {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-180, 180), Random.Range(-180, 180)));
    }
    public void shoot() {
        Vector2 direction = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
        GameObject spawned = Instantiate(projectile);
        spawned.tag = "EnemyProjectile";
        spawned.GetComponent<Bullet>().OnFired(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
        spawned.transform.rotation = Random.rotation;
    }
    public void takeDamage(int value) {
        health -= value;
        if(health <= 0) die();
    }
    public void die() {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.name == "Player") {
            Player player = collider.gameObject.GetComponent<Player>();
            player.takeDamage(damage);
        }
    }
}
