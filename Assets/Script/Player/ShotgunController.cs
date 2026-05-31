using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunController : MonoBehaviour
{
    [Header("Shotgun")]
    [SerializeField] private Transform shotgunPivot;
    [SerializeField] private float recoilForce = 15f;
    [SerializeField] private float shootCooldown = 0.8f;

    [Header("Peluru")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private Rigidbody2D playerRb;
    private Vector2 aimDirection;
    private float lastShootTime;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
            playerRb = GetComponentInParent<Rigidbody2D>();

        //Debug.Log("Rigidbody found: " + playerRb);
    }

    void Update()
    {
        // Baca langsung dari Input System
        Vector2 aimInput = Vector2.zero;

        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            aimInput = gamepad.rightStick.ReadValue();
            //Debug.Log("Gamepad detected, right stick: " + aimInput);
        }

        if (aimInput.magnitude > 0.1f)
        {
            aimDirection = aimInput.normalized;

            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            shotgunPivot.rotation = Quaternion.Euler(0, 0, angle);

            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time < lastShootTime + shootCooldown) return;
        lastShootTime = Time.time;

        //Debug.Log("Shoot! aimDirection: " + aimDirection + " recoil: " + (-aimDirection * recoilForce));
        
        playerRb.velocity = Vector2.zero;

        Vector2 recoilDirection = -aimDirection;
        playerRb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>()?.AddForce(aimDirection * 20f, ForceMode2D.Impulse);
            Destroy(bullet, 3f);
        }
    }
}