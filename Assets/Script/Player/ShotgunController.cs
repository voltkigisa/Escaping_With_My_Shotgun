using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ShotgunController : MonoBehaviour
{
    private CharacterMovement movement;
    private bool wasGrounded;
    [Header("Shotgun")]
    [SerializeField] private Transform shotgunPivot;
    [SerializeField] private float recoilForce = 15f;
    [SerializeField] private float shootCooldown = 0.2f;

    [Header("Peluru")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 2;
    [SerializeField] private AmmoUI ammoUI;
    [SerializeField] private float reloadPerBullet = 2f;
    private bool isReloading = false;

    private int currentAmmo;

    private Rigidbody2D playerRb;
    private Vector2 aimDirection;
    private float lastShootTime;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
            playerRb = GetComponentInParent<Rigidbody2D>();

        //Debug.Log("Rigidbody found: " + playerRb);
        movement = GetComponent<CharacterMovement>();
    }
    void Start()
    {
        UpdateAmmoUI();
    }

    void Update()
    {
        Vector2 aimInput = Vector2.zero;

        if (Gamepad.current != null)
        {
            aimInput = Gamepad.current.rightStick.ReadValue();
        }

        if (aimInput.magnitude > 0.1f)
        {
            aimDirection = aimInput.normalized;

            float angle = Mathf.Atan2(
                aimDirection.y,
                aimDirection.x
            ) * Mathf.Rad2Deg;

            // Kalau karakter menghadap kiri, balik angle
            CharacterMovement charMovement = GetComponent<CharacterMovement>();
            if (charMovement != null && !charMovement.IsFacingRight())
                angle = 180 + angle;

            shotgunPivot.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (JoystickReader.ShootPressed)
        {
            JoystickReader.ShootPressed = false;
            Shoot();
        }
        bool grounded = movement.IsGrounded();

        if (grounded)
        {
            StartReload();
        }
        else
        {
            StopAllCoroutines();
            isReloading = false;
        }

        wasGrounded = grounded;
    }
    private void StartReload()
    {
        if (!isReloading && currentAmmo < maxAmmo) // tambah cek ammo
        {
            StartCoroutine(ReloadRoutine());
        }
    }
    private void UpdateAmmoUI()
    {
        ammoUI.UpdateAmmo(currentAmmo);
    }
    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        while (currentAmmo < maxAmmo)
        {
            yield return new WaitForSeconds(reloadPerBullet);

            currentAmmo++;

            UpdateAmmoUI();

            //Debug.Log("Reload: " + currentAmmo);
        }

        isReloading = false;
        SoundManager.Instance.PlayReload();
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        if (Time.time < lastShootTime + shootCooldown)
            return;

        currentAmmo--;

        lastShootTime = Time.time;

        Vector2 recoilDirection = -aimDirection;
        playerRb.AddForce(
            recoilDirection * recoilForce,
            ForceMode2D.Impulse
        );

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );

            bullet.GetComponent<Rigidbody2D>()?.AddForce(
                aimDirection * 20f,
                ForceMode2D.Impulse
            );

            Destroy(bullet, 3f);
        }

        UpdateAmmoUI();
        SoundManager.Instance.PlayShotgun();
    }
}