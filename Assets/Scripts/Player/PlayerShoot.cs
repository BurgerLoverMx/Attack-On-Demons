using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBase))]
public class PlayerShoot : MonoBehaviour
{
    private PlayerBase player;

    private CameraFollow cameraFollow;

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private Transform[] weaponPositions;

    [SerializeField]
    private Transform weaponPivot;

    void Start()
    {
        player = PlayerBase.Instance;
        player.playerShoot = this;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        for (int i = 0; i <  weapons.Length; i++)
        {
            if (weaponPositions[i].childCount > 0)
            {
                weapons[i] = weaponPositions[i].GetChild(0).gameObject;
            }
        }
    }


    void Update()
    {
        Aim();
        Shoot();
        SwitchWeapons();
    }

    private void Aim()
    {
        Vector2 mousePosition = player.playerControls.Player.Aim.ReadValue<Vector2>();

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        if (mousePosition.x < screenPoint.x)
        {
            foreach (GameObject weapon in weapons)
            {
                if (weapon != null)
                {
                    weapon.transform.localRotation = Quaternion.Euler(180, 0, 0);

                    weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x,
                     Mathf.Abs(weapon.transform.localPosition.y),
                     weapon.transform.localPosition.z);
                }
            }

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            foreach (GameObject weapon in weapons)
            {
                if (weapon != null)
                {
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    
                    weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x,
                     -Mathf.Abs(weapon.transform.localPosition.y),
                     weapon.transform.localPosition.z);
                }
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        weaponPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        //Move camera towards mouse
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 mouseOffset = new Vector3(mousePosition.x, mousePosition.y, 0);
        cameraFollow.offsetMouse = mouseOffset;
    }

    private void Shoot()
    {
        if (player.playerControls.Player.ShootFirst.ReadValue<float>() > 0)
        {
            if (weapons[0] != null)
            {
                weapons[0].GetComponent<Weapon>().Attack();
            }
        }
        if (player.playerControls.Player.ShootSecond.ReadValue<float>() > 0)
        {
            if (weapons[1] != null)
            {
                weapons[1].GetComponent<Weapon>().Attack();
            }
        }
    }

    public void SwitchWeapons()
    {
        if (player.playerControls.Player.SwitchWeapons.triggered)
        {
            GameObject firstWeapon = weapons[0];
            weapons[0] = weapons[1];
            weapons[1] = firstWeapon;

            int nullWeaponIndex = -1;
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] == null)
                {
                    if (nullWeaponIndex == -1)
                    {
                        nullWeaponIndex = i;
                    }
                    else
                    {
                        //No weapons to switch
                        return;
                    }
                }
            }

            if (nullWeaponIndex != 0)
            {
                weapons[0].GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
                weapons[0].transform.SetParent(weaponPositions[0], false);
            }
            if (nullWeaponIndex != 1)
            {
                weapons[1].GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
                weapons[1].transform.SetParent(weaponPositions[1], false);
            }
        }
    }

    public bool PickupWeapon(GameObject newWeapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                GameObject weapon = Instantiate(newWeapon, weaponPositions[i]);
                weapons[i] = weapon;
                return true;
            }
        }

        return false;
    }

    public GameObject GetWeapon(int index)
    {
        return weapons[index];
    }
}
