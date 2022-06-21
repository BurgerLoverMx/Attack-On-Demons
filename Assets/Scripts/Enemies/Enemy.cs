using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth, maxMovespeed, damage, attackRange, attackAnimTime;
    protected float currentHealth, currentMovespeed, attackTimer;
    public Animator animator;
    protected GameObject player;
    public bool canMoveWhileAttack;
    protected bool attacking = false, dying = false, alive = true;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentMovespeed = maxMovespeed;
        player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(SetPlayerVar());
        //StartCoroutine(StartDeath());
    }


    private IEnumerator SetPlayerVar()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            Move();
            if (!attacking)
            {
                CheckIfAttack();
            }
        }
    }

    public IEnumerator Hit(float dmg)
    {
        if (alive)
        {
            currentHealth -= dmg;
            Debug.Log("enemy health: " + currentHealth);
            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0, .65f);
            yield return new WaitForSeconds(0.08f);
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }
    }

    private IEnumerator Die()
    {
        dying = true;
        animator.SetTrigger("Die");
        this.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public abstract void Move();

    public abstract void CheckIfAttack();

    protected IEnumerator StartAttack()
    {
        attacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackAnimTime);
        Attack();
        attacking = false;
    }

    public abstract void Attack();

    private IEnumerator StartDeath()
    {
        //delete
        yield return new WaitForSeconds(2);

        animator.SetTrigger("Die");
        dying = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
