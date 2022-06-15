using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBase))]
public class PlayerShoot : MonoBehaviour
{
    private PlayerBase player;

    private CameraFollow cameraFollow;

    [SerializeField]
    private GameObject weapon;

    private bool shooting = false;

    void Start()
    {
        player = PlayerBase.Instance;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }


    void Update()
    {
        Aim();
        Shoot();
    }

    private void Aim()
    {
        Vector2 mousePosition = player.playerControls.Player.Aim.ReadValue<Vector2>();

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        if (mousePosition.x < screenPoint.x)
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, 
            -Mathf.Abs(weapon.transform.localScale.y), 
            weapon.transform.localScale.z);
        }
        else
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x,
             Mathf.Abs(weapon.transform.localScale.y), 
             weapon.transform.localScale.z);
        }
        weapon.transform.parent.rotation = Quaternion.Euler(0f, 0f, angle);

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        //Move camera towards mouse
        Vector3 mouseOffset = new Vector3(mousePosition.x, mousePosition.y, 0);
        cameraFollow.offsetMouse = mouseOffset;
    }

    private void Shoot()
    {
        if (player.playerControls.Player.Shoot.ReadValue<float>() > 0)
        {
            if (weapon != null)
            {
                weapon.GetComponent<Weapon>().Shoot();
            }
        }
    }
}
