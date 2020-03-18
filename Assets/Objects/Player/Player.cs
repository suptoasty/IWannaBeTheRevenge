using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    public float movementSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float maxSpeed = 50.0f;
    public bool isOnGround = true;
    private Transform startPosition;
    void Start() {
        body = GetComponent<Rigidbody2D>();
        startPosition = transform;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("YEET");
            transform.SetPositionAndRotation(startPosition.position, Quaternion.identity);
        }

    }

    public void FixedUpdate() {
        if(body.velocity.magnitude < maxSpeed) body.AddForce(new Vector2(Input.GetAxis("Horizontal")*body.mass,0.0f)*Time.fixedDeltaTime*movementSpeed);

        if(Input.GetButtonDown("Jump") && isOnGround) {
            body.AddForce(new Vector2(0.0f, 1.0f*jumpForce*body.mass)*Time.fixedDeltaTime, ForceMode2D.Impulse);
            isOnGround = false;
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
