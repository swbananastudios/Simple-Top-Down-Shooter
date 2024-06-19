using UnityEngine;
using DG.Tweening;
using System;

public class EnemyHealth : MonoBehaviour
{
    // See PlayerHealth.cs

    [SerializeField] private int health;

    public static Action EnemyDeathAction;

    public void Damage(int damage)
    {
        health -= damage;
        transform.DOPunchScale(transform.localScale * 0.4f, 0.1f);

        if (health == 0)
            Death();
    }

    public void Death()
    {
        EnemyDeathAction?.Invoke();
        transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => Destroy(gameObject));
    }
}
