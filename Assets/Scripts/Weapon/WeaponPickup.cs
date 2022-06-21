using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponToGet;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = weaponToGet.GetComponent<Weapon>().spriteRenderer.sprite;

        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PlayerEntered");
            bool success = PlayerBase.Instance.playerShoot.PickupWeapon(weaponToGet);
            if (success)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
