using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGroundChase : Enemy
{
    [SerializeField]
    private Transform collisionChecker;
    [SerializeField]
    private float checkCollisionRadius, jumpForce;
    public LayerMask layersToCheck;
    private bool jumping = false;

    override public void Move()
    {
        if(!attacking || canMoveWhileAttack){
            float direction = player.transform.position.x - transform.position.x;
            if (direction < 0)
            {
                //left facing
                rb.velocity = new Vector2(-currentMovespeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //right facing
                rb.velocity = new Vector2(currentMovespeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (!jumping)
            {
                Collider2D collider = Physics2D.OverlapCircle(collisionChecker.position, checkCollisionRadius, layersToCheck);
                if (collider != null)
                {
                    StartCoroutine(Jump());
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private IEnumerator Jump()
    {
        jumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        jumping = false;
    }

    override public void Attack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && attacking)
        {
            StartCoroutine(collision.GetComponent<PlayerBase>().Hit(damage));
        }
    }

}
