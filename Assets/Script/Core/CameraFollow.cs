using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Pengaturan Kamera")]
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private float startY;
    private float cameraHalfWidth;



    void Start()
    {
        Camera cam = Camera.main;
        cameraHalfWidth = cam.orthographicSize * cam.aspect;

        startY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetY = target.position.y + offset.y;

        // Kamera tidak boleh lebih rendah dari posisi awal
        targetY = Mathf.Max(targetY, startY);

        Vector3 targetPosition = new Vector3(
            0f,
            targetY,
            offset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // Batasi player agar tidak keluar layar kiri/kanan
        Vector3 playerPos = target.position;
        playerPos.x = Mathf.Clamp(
            playerPos.x,
            -cameraHalfWidth,
            cameraHalfWidth
        );
        target.position = playerPos;
    }
}