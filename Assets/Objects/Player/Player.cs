using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    public float movementSpeed = 300.0f;
    public float jumpForce = 200.0f;
    public float shortJumpForce = 100.0f;
    public float maxSpeed = 300.0f;
    public bool isOnGround = true;
    private Transform startPosition;
    void Start() {
        body = GetComponent<Rigidbody2D>();
        startPosition = transform;
    }

    // Update is called once per frame
    void Update() {
    }

    public void FixedUpdate() {
        if(body.velocity.magnitude < maxSpeed) body.AddForce(new Vector2(Input.GetAxis("Horizontal")*body.mass,0.0f)*Time.fixedDeltaTime*movementSpeed);
    //    body.velocity = new Vector2(Input.GetAxis("Horizontal")*movementSpeed, body.velocity.y*body.gravityScale)*Time.fixedDeltaTime;

        if(Input.GetButtonDown("Jump") && isOnGround) {
            // Vector2 newJumpForce = new Vector2(0.0f, 1.0f*jumpForce)*Time.fixedDeltaTime;
            // if(newJumpForce.y > maxSpeed) newJumpForce = new Vector2(0.0f, maxSpeed);
            // body.AddForce(newJumpForce, ForceMode2D.Impulse);
            body.velocity = new Vector2(body.velocity.x, jumpForce)*Time.fixedDeltaTime;
            Debug.Log("JUMP"+body.velocity);
            isOnGround = false;
        } else if(Input.GetButtonUp("Jump") && Vector2.Dot(body.velocity, Vector2.up)>0 && Vector2.Dot(body.velocity, Vector2.up) <= jumpForce) {
            body.velocity = new Vector2(body.velocity.x, shortJumpForce)*Time.fixedDeltaTime;
            Debug.Log("SHORT JUMP"+body.velocity);
        }
        // if (Input.GetButtonUp("Jump") && Vector2.Dot(body.velocity, Vector2.up)>0 && !isOnGround) {
        //     body.AddForce(Vector2.down*(jumpForce/2)*body.mass, ForceMode2D.Impulse);
        //     isOnGround = false;
        // }

        Collider2D c = Physics2D.OverlapArea(
                new Vector2(transform.position.x - 0.5f, transform.position.y + 0.55f),
                new Vector2(transform.position.x + 0.5f, transform.position.y+0.6f));

        BonkHandler block;
        // if(c != null && colliding != c.gameObject && c.gameObject.TryGetComponent<BonkHandler>()) {
        if(c != null && c.gameObject.TryGetComponent<BonkHandler>(out block)) {
            block.HandleBonk(transform.position.x, transform.position.y+0.575f);

        }
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
