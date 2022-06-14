using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2((rb.velocity.x + 1) * 55, Mathf.Abs(rb.velocity.y) * 2), ForceMode2D.Impulse);
        }
    }
}
