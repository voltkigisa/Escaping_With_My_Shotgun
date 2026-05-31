using UnityEngine;

public class JoystickReader : MonoBehaviour
{
    public static Vector2 RightStickValue;

    public void OnStickMove(Vector2 input)
    {
        RightStickValue = input;
    }

    public void OnStickRelease()
    {
        RightStickValue = Vector2.zero;
    }
}