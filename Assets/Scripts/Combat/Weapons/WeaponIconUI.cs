using UnityEngine;
using UnityEngine.UI;

public class WeaponIconUI : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();

        if (weaponManager != null)
        {
            // Подписываемся на новое событие (с иконкой)
            weaponManager.OnWeaponChanged += UpdateIcon;

            // В начале иконка скрыта
            weaponIcon.enabled = false;
        }
    }

    private void UpdateIcon(int weaponIndex, Sprite icon)
    {
        if (icon != null)
        {
            weaponIcon.sprite = icon;
            weaponIcon.enabled = true;
        }
        else
        {
            // Иконка пустая — скрываем
            weaponIcon.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (weaponManager != null)
        {
            weaponManager.OnWeaponChanged -= UpdateIcon;
        }
    }
}