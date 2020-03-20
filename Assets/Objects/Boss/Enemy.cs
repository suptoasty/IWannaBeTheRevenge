using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public int damage = 1;
    public GameObject projectile;
    public float shootCooldown = 0.58f;

    public void Update() {
        shootCooldown -= Time.deltaTime;
    }

    public void FixedUpdate() {
        int rand = Mathf.RoundToInt(Random.Range(0.0f, 100.0f));
        if(rand%2==0) {
            move(); 
        } 
        if(shootCooldown<=0.0f) {
            shoot();
            shootCooldown = 0.58f;
        }

    }

    public void move() {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-180, 180), Random.Range(-180, 180)));
    }
    public void shoot() {
        Vector2 direction = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
        GameObject spawned = Instantiate(projectile);
        spawned.tag = "EnemyProjectile";
        spawned.transform.position = transform.position;
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

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            Player player = collider.gameObject.GetComponent<Player>();
            player.takeDamage(damage);
        }
    }
}
