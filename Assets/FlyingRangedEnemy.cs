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
    public float reloadTime = 0.5f;
    public float shotSpeed = 10;

    Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private float reloadTimer;

    Seeker seeker;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentMovespeed = maxMovespeed;
        player = GameObject.FindGameObjectWithTag("Player");
        reloadTimer = Time.time + reloadTime;
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (Vector2.Distance(rb.position, player.transform.position) < aggroDistance)
            {
                float newXOffset = Random.Range(minXOffset, maxXOffset);
                float newYOffset = Random.Range(minYOffset, maxYOffset);
                seeker.StartPath(rb.position, player.transform.position + new Vector3(newXOffset, newYOffset, 0), OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public override void Move()
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

        transform.right = player.transform.position - transform.position;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (player.transform.position.x < rb.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    public override void CheckIfAttack()
    {
        if (Vector2.Distance(rb.position, player.transform.position) < attackRange && Time.time - reloadTimer > 0 && usesProjectiles)
        {
            StartCoroutine(StartAttack());
        }
    }
    public override void Attack()
    {
        float angle = Vector3.Angle(Vector3.right, (player.transform.position - new Vector3(rb.position.x, rb.position.y, 0)).normalized);
        //This will always be positive, so lets flip the sign if it should be negative
        if (player.transform.position.y < rb.position.y) angle *= -1;

        //Create a rotation that will point towards the target
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        reloadTimer = Time.time + reloadTime;
        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, bulletRotation);
        bulletInstance.GetComponent<Bullet>().shotDamage = damage;
        bulletInstance.GetComponent<Bullet>().shotSpeed = shotSpeed;
        bulletInstance.layer = LayerMask.NameToLayer("EnemyProjectiles");
    }
}
