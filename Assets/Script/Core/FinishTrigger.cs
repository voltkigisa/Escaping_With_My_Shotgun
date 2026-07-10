using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [Header("Referensi")]
    public GameObject canvasFinish;

    private void Start()
    {
        canvasFinish.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasFinish.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}