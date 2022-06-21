using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    protected float damage = 1f;
    [SerializeField]
    protected Animator animator;

    public abstract void Attack();
}
