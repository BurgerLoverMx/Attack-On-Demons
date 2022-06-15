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

    public int currentAmmo;

    private float fireRateTime = 0;

    void Start()
    {
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

        for (int i = 0; i < numProjectiles; i++)
        {
            if (currentAmmo == 0)
            {
                break;
            }

            GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation * Quaternion.Euler(0, 0, i * shotSpread));
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
        //Play animation
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = numAmmo;
    }
}
