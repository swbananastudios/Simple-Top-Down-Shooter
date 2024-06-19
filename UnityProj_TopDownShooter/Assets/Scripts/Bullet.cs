using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet variables
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBeforeDestroy;

    private void Start()
    {
        // Start self destruct timer when spawning the bullet
        StartCoroutine(SelfDestruct());
    }

    private void Update()
    {
        // Bullet moves forward with regard to the moveSpeed variable
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the bullet collides with an Enemy
        if (other.CompareTag("Enemy"))
        {
            // Damage enemy by getting the EnemyHealth component and calling the Damae method and passing the bullet's damage as parameter
            other.GetComponent<EnemyHealth>().Damage(damage);
            // Destroy bullet so that it doesn't go through the enemy
            Destroy(gameObject);
        }
        // If the bullet collides with a wall
        else if (other.CompareTag("Wall"))
        {
            // Destroy bullet so that it doesn't go through the wall
            Destroy(gameObject);
        }
    }

    private IEnumerator SelfDestruct()
    {
        // Start a timer to destroy bullet, so it doesn't infinitely travel in space and time
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
