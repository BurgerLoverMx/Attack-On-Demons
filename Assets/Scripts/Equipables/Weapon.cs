using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float damage = 1f;

    protected Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public abstract void Attack();
}
