using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float shotSpeed, shotDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * shotSpeed;
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().StartCoroutine(other.gameObject.GetComponent<Enemy>().Hit(shotDamage));
        }
        Destroy(gameObject);
    }

}
