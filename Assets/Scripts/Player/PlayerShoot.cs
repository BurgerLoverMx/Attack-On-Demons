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

    void Start()
    {
        player = PlayerBase.Instance;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }


    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        Vector2 mousePosition = player.playerControls.Player.Mouse.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Move camera towards mouse
        Vector3 mouseOffset = new Vector3(mousePosition.x, mousePosition.y, 0);
        cameraFollow.offsetMouse = mouseOffset;
    }
}
