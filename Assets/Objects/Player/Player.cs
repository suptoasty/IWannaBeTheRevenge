using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void FixedUpdate() {
        if(body.velocity.magnitude < maxSpeed) body.AddForce(new Vector2(Input.GetAxis("Horizontal")*body.mass,0.0f)*Time.fixedDeltaTime*movementSpeed);
    //    body.velocity = new Vector2(Input.GetAxis("Horizontal")*movementSpeed, body.velocity.y*body.gravityScale)*Time.fixedDeltaTime;
        
        //calc projectile offset
        if(body.velocity.x<0) projectileOffset = Vector2.left*0.32f;
        else if(body.velocity.x>0) projectileOffset = Vector2.right*0.32f;

        Debug.Log("Projectile Offset: "+projectileOffset);

        if(Input.GetButtonDown("Jump") && isOnGround) {
            body.velocity = new Vector2(body.velocity.x, jumpForce)*Time.fixedDeltaTime;
            Debug.Log("JUMP"+body.velocity);
            isOnGround = false;
        } else if(Input.GetButtonUp("Jump") && Vector2.Dot(body.velocity, Vector2.up)>0 && Vector2.Dot(body.velocity, Vector2.up) <= jumpForce) {
            body.velocity = new Vector2(body.velocity.x, shortJumpForce)*Time.fixedDeltaTime;
            Debug.Log("SHORT JUMP"+body.velocity);
        }

        if(Input.GetButtonDown("Shoot")) {
            shoot();
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
        spawned.transform.position = new Vector2(transform.position.x+projectileOffset.x, transform.position.y);
        spawned.GetComponent<Bullet>().OnFired(Mathf.Sign(projectileOffset.x));
    }

    public void takeDamage(int value) {
        health -= value;
        if(health <= 0) die();
    }

    public void die() {
        Debug.Log("Player Died");
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
