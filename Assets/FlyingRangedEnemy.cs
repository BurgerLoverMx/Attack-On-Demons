using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingRangedEnemy : Enemy
{
    public GameObject bullet;
    public bool usesProjectiles = false;
    public float nextWaypointDistance = 3f;
    public Transform shootPoint;
    public float minXOffset, maxXOffset, minYOffset, maxYOffset;
    public float aggroDistance;
    public float shootDistance;
    public float reloadTime = 0.5f;

    Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private float reloadTimer;
    private Transform target;

    Seeker seeker;

    void Start()
    {
        currentHealth = maxHealth;
        reloadTimer = Time.time + reloadTime;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    override public void Move()
    {
        if (seeker.IsDone())
        {
            target = player.transform;
            if (Vector2.Distance(rb.position, target.position) < aggroDistance)
            {
                float newXOffset = Random.Range(minXOffset, maxXOffset);
                float newYOffset = Random.Range(minYOffset, maxYOffset);
                seeker.StartPath(rb.position, target.position + new Vector3(newXOffset, newYOffset, 0), OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;

            if (Vector2.Distance(rb.position, target.position) < aggroDistance && Time.time - reloadTimer > 0 && usesProjectiles)
            {
                StartCoroutine(Shoot());
            }
        }
    }


    public override void Attack()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        reloadTimer = Time.time + reloadTime;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackAnimTime);
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    void Update()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currentMovespeed;
        rb.AddForce(force);

        transform.right = target.position - transform.position;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (target.position.x < rb.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
