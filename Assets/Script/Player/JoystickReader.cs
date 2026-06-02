using UnityEngine;

public class JoystickReader : MonoBehaviour
{
    public static bool ShootPressed;

    public void OnStickRelease()
    {
        ShootPressed = true;
    }
}