using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance;
    public PlayerControls playerControls;
    public float maxHealth = 5, invulnTime = .12f;
    private float currentHealth;
    public Animator animator;
    private bool gettingHit = false, alive = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Hit(float dmg)
    {
        if (!gettingHit && alive)
        {
            currentHealth -= dmg;
            Debug.Log("health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            gettingHit = true;
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0, .65f);
            yield return new WaitForSeconds(invulnTime);
            gettingHit = false;
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }
    }

    private void Die()
    {
        alive = false;
        //zoom in on player plsss
        animator.SetTrigger("Die");
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerShoot>().enabled = false;
        this.enabled = false;
    }
}
