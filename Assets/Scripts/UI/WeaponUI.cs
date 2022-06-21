using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSlot1, weaponSlot2;
    void Start()
    {
    }

    void Update()
    {
        if (PlayerBase.Instance.playerShoot != null)
        {
            GameObject weapon1 = PlayerBase.Instance.playerShoot.GetWeapon(0);
            GameObject weapon2 = PlayerBase.Instance.playerShoot.GetWeapon(1);

            if (weapon1 != null)
            {
                weaponSlot1.GetComponent<Image>().sprite = weapon1.GetComponent<Weapon>().spriteRenderer.sprite;
                weaponSlot1.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            }
            else
            {
                weaponSlot1.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

            if (weapon2 != null)
            {
                weaponSlot2.GetComponent<Image>().sprite = weapon2.GetComponent<Weapon>().spriteRenderer.sprite;
                weaponSlot2.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            }
            else
            {
                weaponSlot2.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
        }
    }
}
