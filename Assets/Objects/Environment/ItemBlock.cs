using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ItemBlock : BonkHandler
{
    public GameObject itemToSpawn;
    public int dispensedItems = 0;
    public int maxDispensedItems = -1;
    public Sprite emptyBlock = null;
    public bool suprisePhysics = false;
    public float gravityScale = 0.5f;

    void OnCollisionEnter2D(Collision2D collision) {
        if(Regex.IsMatch(collision.gameObject.name, "ItemBlock*", RegexOptions.IgnoreCase)) return;
        if(itemToSpawn) {
            if(maxDispensedItems == -1) Dispense();
            else if(dispensedItems >= 0 && dispensedItems < maxDispensedItems) Dispense();
        } 
        if(suprisePhysics) { 
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Dynamic;
            body.gravityScale = gravityScale;
        }
    }

    public override void HandleBonk(float x, float y) {
        Dispense();
    }

    void Dispense() {
        if(dispensedItems < maxDispensedItems) {
            dispensedItems++;
            GameObject spawned = Instantiate(itemToSpawn);
            spawned.transform.position = new Vector2(transform.position.x, transform.position.y+1.0f);
            Debug.Log(spawned.name+": "+spawned.transform.position);
        }
        if(emptyBlock) GetComponent<SpriteRenderer>().sprite = emptyBlock;
    }

    // IEnumerator Hit() {
    //     yield return new WaitForSeconds(1.0f);
    // }
}
