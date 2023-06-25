using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float currMovementSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;
    public float fireRate = 2.0f;
    private float nextFireTime = 0.0f;
    public float reloadRate = 1.0f;
    private float nextReloadTime = 0.0f;

    Vector2 moveDirection;
    Vector2 mousePosition;

    private void OnEnable()
    {
        PlayerBehaviour.onPlayerDeath += DisablePlayerMovement;
    }
    private void OnDisable()
    {
        PlayerBehaviour.onPlayerDeath -= DisablePlayerMovement;
    }

    private void Start()
    {
        EnablePlayerMovement();
    }

    public void DisablePlayerMovement()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void EnablePlayerMovement()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        PlayerBehaviour playerBehaviour = GetComponent<PlayerBehaviour>();

        if ((Input.GetMouseButton(0) || Input.GetKey("space")) 
            && playerBehaviour.CheckHealth() > 0 
            && Time.time > nextFireTime 
            && StatsManager.Instance.ammo != 0)
        {
            weapon.FireWeapon();
            nextFireTime = Time.time + 1.0f / fireRate;
        }

        if (Input.GetKey(KeyCode.R) && Time.time > nextReloadTime && StatsManager.Instance.ammo < StatsManager.Instance.maxAmmo)
        {
            if (currMovementSpeed == movementSpeed)
            {
                currMovementSpeed -= 3f;
            }
            StatsManager.Instance.IncreaseAmmoCount();
            nextReloadTime = Time.time + 1.0f / reloadRate;
        }
        else if(Time.time > nextReloadTime)
        {
            currMovementSpeed = movementSpeed;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * currMovementSpeed, moveDirection.y * currMovementSpeed);
        
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
