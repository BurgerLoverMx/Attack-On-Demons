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
    protected bool attacking = false, dying = false;
    protected Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentMovespeed = maxMovespeed;
        player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(StartDeath());
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

    public void ChangeHealth(float modifier)
    {
        if(modifier < 0)
        {
            //do hit anim or smth
            currentHealth -= modifier;
            if(currentHealth <= 0)
            {
                StartCoroutine(StartDeath());
            }
        }
    }

    public abstract void Move();

    private void CheckIfAttack()
    {
        Vector3 distance = player.transform.position - transform.position;
        float sqrLength = distance.sqrMagnitude;
        if (sqrLength < attackRange * attackRange)
        {
            //in range
            StartCoroutine(StartAttack());
        }
        else
        {
            //not in range
        }
    }

    private IEnumerator StartAttack()
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
