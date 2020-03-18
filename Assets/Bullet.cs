using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletSpeed = 5.0f;
    public float lifeTime = 5.0f;

    public void OnFired(float direction) {
        GetComponent<SpriteRenderer>().flipX = direction < 0 ? true : false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(direction*bulletSpeed, 0.0f), ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Enemy") {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(damage);
        }
        Destroy(gameObject);
    }
}
