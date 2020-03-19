using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletSpeed = 5.0f;
    public float lifeTime = 5.0f;

    public void OnFired(Vector2 direction) {
        GetComponent<SpriteRenderer>().flipX = direction.x < 0 ? true : false;
        GetComponent<Rigidbody2D>().AddForce(direction*bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if(gameObject.tag == "PlayerProjectile" && collider.gameObject.tag == "Enemy") {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(damage);
            Destroy(gameObject);
        } else if (gameObject.tag == "EnemyProjectile" && collider.gameObject.tag == "Player") {
            Player player = collider.gameObject.GetComponent<Player>();
            player.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
