using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 1f, realoadSpeed = 1.5f, shotSpread = 0f, shotDamage = 1f, shotSpeed = 1f;

    [SerializeField]
    private int numAmmo, numProjectiles;

    [SerializeField]
    private GameObject bulletPrefab;

    public void Shoot()
    {
        GameObject bulletClone = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bulletCloneScript = bulletClone.GetComponent<Bullet>();
        bulletCloneScript.shotSpeed = shotSpeed;
        bulletCloneScript.shotDamage = shotDamage;
    }
}
