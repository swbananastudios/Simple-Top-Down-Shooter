using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rigidBody;
    private float moveHorizontal;
    private float moveVertical;
    private bool allowMovement = true;
    private AudioSource shootSFX;

    private void Awake()
    {
        // Get reference to the Player's rigidbody component and cache it
        rigidBody = GetComponent<Rigidbody>();

        // Get reference to the shoot sfx audio source and cache it
        shootSFX = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Let OnGameOver method subscribe to GameOverAction
        GameManager.GameOverAction += OnGameOver;
    }

    private void OnDisable()
    {
        // Remove subsciption when player object is disabled
        GameManager.GameOverAction -= OnGameOver;
    }

    private void Update()
    {
        if (!allowMovement) return;

        // Get movement input
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        // Fire bullet
        if (Input.GetButtonUp("Fire1"))
        {
            shootSFX.Play();
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (!allowMovement) return;

        // Apply movement based on input
        rigidBody.velocity = new Vector3(moveHorizontal, 0f, moveVertical).normalized * moveSpeed;

        // Look at mouse using camera and plane
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        if (groundPlane.Raycast(cameraRay, out float intersectPoint))
        {
            Vector3 pointToLook = cameraRay.GetPoint(intersectPoint);
            transform.LookAt(pointToLook);
        }
    }

    public void OnGameOver() 
    {
        // Disallow movement when GameOver action is invoked
        allowMovement = false;
    }
}
