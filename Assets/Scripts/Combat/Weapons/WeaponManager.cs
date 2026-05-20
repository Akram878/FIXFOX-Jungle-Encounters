using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Оружие на игроке")]
    [SerializeField] private ClawsCombat clawsCombat;

    [Header("Иконки оружия")]
    [SerializeField] private Sprite clawsIcon;
    [SerializeField] private Sprite grenadeIcon;
    [SerializeField] private Sprite gunIcon;
    [SerializeField] private Sprite magicIcon;

    private int currentWeaponIndex = -1;

    public event System.Action<int, Sprite> OnWeaponChanged;

    private void Awake()
    {
        // Автоматически находит ClawsCombat, если поле пустое
        if (clawsCombat == null)
        {
            clawsCombat = GetComponent<ClawsCombat>();
        }

        if (clawsCombat != null)
        {
            clawsCombat.OnClawsToggled += OnClawsToggled;
        }
    }

    private void Update()
    {
        // Клавиши для будущего оружия
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1, grenadeIcon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2, gunIcon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeapon(3, magicIcon);
        }
    }

    private void OnClawsToggled(bool enabled)
    {
        if (enabled)
        {
            // Всегда показываем иконку, даже если уже был индекс 0
            currentWeaponIndex = 0;
            OnWeaponChanged?.Invoke(0, clawsIcon);
        }
        else
        {
            // Когти выключены — убираем иконку
            currentWeaponIndex = -1;
            OnWeaponChanged?.Invoke(-1, null);
        }
    }

    private void SelectWeapon(int index, Sprite icon)
    {
        // Если выбираем то же оружие — ничего не делаем
        if (index == currentWeaponIndex) return;

        // Выключаем когти, если они были включены
        if (clawsCombat != null)
        {
            clawsCombat.ForceDisableClaws();
        }

        currentWeaponIndex = index;
        OnWeaponChanged?.Invoke(index, icon);
    }

    private void OnDestroy()
    {
        if (clawsCombat != null)
        {
            clawsCombat.OnClawsToggled -= OnClawsToggled;
        }
    }
}