using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Pengaturan Kamera")]
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Batas Kamera")]
    public float minY = 0f;  // Batas bawah kamera (set sesuai level)

    private float cameraHalfHeight;
    private float cameraHalfWidth;

    void Start()
    {
        Camera cam = Camera.main;
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cam.orthographicSize * cam.aspect;

        // Langsung snap kamera ke posisi karakter saat mulai
        if (target != null)
        {
            Vector3 startPosition = target.position + offset;
            startPosition.y = Mathf.Max(startPosition.y, minY);
            transform.position = startPosition;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // X kamera selalu 0, Y mengikuti karakter tapi hanya ke atas
        float targetY = Mathf.Max(transform.position.y, target.position.y + offset.y);

        Vector3 targetPosition = new Vector3(0f, targetY, offset.z);

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Clamp karakter agar tidak keluar kamera kiri/kanan
        Vector3 playerPos = target.position;
        playerPos.x = Mathf.Clamp(playerPos.x,
            -cameraHalfWidth,
            cameraHalfWidth);
        target.position = playerPos;
    }
}