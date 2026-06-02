using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Image ammo1;
    public Image ammo2;

    public void UpdateAmmo(int ammo)
    {
        SetAlpha(ammo1, ammo >= 1 ? 1f : 0.3f);
        SetAlpha(ammo2, ammo >= 2 ? 1f : 0.3f);
    }

    private void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}