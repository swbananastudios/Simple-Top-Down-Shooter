using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;

    private IEnumerator attackCoroutine;
    private PlayerHealth playerHealth;

    private void Start()
    {
        // Reference and cache Attack() courotine so that we can stop it
        attackCoroutine = Attack();
    }

    private void OnEnable()
    {
        GameManager.GameOverAction += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.GameOverAction -= OnGameOver;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If enemy collides with player ...
        if (other.CompareTag("Player"))
        {
            // Cache the playerHealth component of the player
            playerHealth = other.GetComponent<PlayerHealth>();
            // Start the enemy attack
            StartCoroutine(attackCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If player exits collision with enemy ...
        if (other.CompareTag("Player"))
        {
            // Stop the enemy attack
            StopAttack();
        }
    }

    private IEnumerator Attack()
    {
        // Damage the cached player health component every (attackSpeed) seconds
        while (true)
        {
            playerHealth.Damage(damage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    public void OnGameOver()
    {
        StopAttack();
    }

    public void StopAttack()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }
}
