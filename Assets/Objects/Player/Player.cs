﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    public int health = 3;
    public float movementSpeed = 300.0f;
    public float jumpForce = 200.0f;
    public float shortJumpForce = 100.0f;
    public float maxSpeed = 300.0f;
    public bool isOnGround = true;
    public GameObject projectile;
    private Vector2 projectileOffset;
    private float shootCoolDown = 0.58f;
    private bool jump = false;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //for some reason this solves timing issues with jump (Is it Due to the Faster Update Call?)
        if(Input.GetButtonDown("Jump") && isOnGround) {
            jump = true;
        }
         shootCoolDown -= Time.deltaTime;
    }

    public void FixedUpdate() {
        if(body.velocity.magnitude < maxSpeed) body.AddForce(new Vector2(Input.GetAxis("Horizontal")*body.mass,0.0f)*Time.fixedDeltaTime*movementSpeed);
    //    body.velocity = new Vector2(Input.GetAxis("Horizontal")*movementSpeed, body.velocity.y*body.gravityScale)*Time.fixedDeltaTime;
        
        //calc projectile offset
        if(body.velocity.x<0) projectileOffset = Vector2.left*0.32f;
        else if(body.velocity.x>0) projectileOffset = Vector2.right*0.32f;
        
        if(jump) {
            body.velocity = new Vector2(body.velocity.x, jumpForce)*Time.fixedDeltaTime;
            isOnGround = false;
            jump = false;
        } else if(Input.GetButtonUp("Jump") && Vector2.Dot(body.velocity, Vector2.up)>0) {
            body.velocity = new Vector2(body.velocity.x, shortJumpForce)*Time.fixedDeltaTime;
        }

        if(Input.GetButton("Shoot") && shootCoolDown <= 0.0f) {
            shoot();
            shootCoolDown = 0.58f;
        }

        Collider2D c = Physics2D.OverlapArea(
                new Vector2(transform.position.x - 0.5f, transform.position.y + 0.55f),
                new Vector2(transform.position.x + 0.5f, transform.position.y+0.6f));

        BonkHandler block;
        // if(c != null && colliding != c.gameObject && c.gameObject.TryGetComponent<BonkHandler>()) {
        if(c != null && c.gameObject.TryGetComponent<BonkHandler>(out block)) {
            block.HandleBonk(transform.position.x, transform.position.y+0.575f);

        }
    }

    public void shoot() {
        GameObject spawned = Instantiate(projectile);
        spawned.tag = "PlayerProjectile";
        spawned.transform.position = new Vector2(transform.position.x+projectileOffset.x, transform.position.y);
        spawned.GetComponent<Bullet>().OnFired(new Vector2(Mathf.Sign(projectileOffset.x), 0.0f));
    }

    public void takeDamage(int value) {
        health -= value;
        if(health <= 0) die();
    }

    public void die() {
        FindObjectOfType<GameState>().deaths++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if(isCollidingWithGround(collision)) isOnGround = true;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if(isCollidingWithGround(collision)) isOnGround = true;
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if(!isCollidingWithGround(collision)) isOnGround = false;
    }

    private bool isCollidingWithGround(Collision2D collision) {
        bool on_ground = false;
        foreach(ContactPoint2D contact in collision.contacts) {
            Vector2 collision_direction_vector = contact.point - body.position;
            if(collision_direction_vector.y < 0) on_ground = true;
        }

        return on_ground;
    }
}
