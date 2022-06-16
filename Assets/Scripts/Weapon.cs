using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 1f, reloadSpeed = 1.5f, shotSpread = 0f, shotDamage = 1f, shotSpeed = 1f;

    [SerializeField]
    private int numAmmo, numProjectiles;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPoint;

    private Animator animator;

    public int currentAmmo;

    private float fireRateTime = 0;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentAmmo = numAmmo;
    }

    void Update()
    {
        if (fireRateTime >= 0)
        {
            fireRateTime -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (fireRateTime > 0 || currentAmmo <= 0)
        {
            return;
        }

        animator.speed = 1 / fireRate;
        animator.SetTrigger("Shoot");

        //Even number of projectiles
        bool evenProjectiles = false;
        if (numProjectiles % 2 == 0)
        {
            evenProjectiles = true;
        }

        for (int i = 0; i < numProjectiles; i++)
        {
            if (currentAmmo == 0)
            {
                break;
            }
            
            int index = i;
            if (evenProjectiles)
            {
                index++;
            }
            if (index % 2 == 0 && index != 0)
            {
                index = -(index - 1);
            }
            Quaternion bulletRotation = Quaternion.Euler(0, 0, index * shotSpread);
            GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation * bulletRotation);
            Bullet bulletCloneScript = bulletClone.GetComponent<Bullet>();
            bulletCloneScript.shotSpeed = shotSpeed;
            bulletCloneScript.shotDamage = shotDamage;
            currentAmmo -= 1;
        }

        fireRateTime = fireRate;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForEndOfFrame();
        animator.speed = 1 / reloadSpeed;
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = numAmmo;
    }
}
