using UnityEngine;

public enum UpgradeType
{
    MaxHealth,
    Damage,
    Speed,
    Jump,
}

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController instance;

    public int healthMultiplier = 1;
    public float damageMultiplier = 0.5f;
    public float speedMultiplier = 0.1f;
    public float jumpMultiplier = 0.2f;

    public UpgradeUI _upgradeUI;

    private void Awake()
    {
        instance = this;
    }

    public void Upgrade(int index)
    {
        EntityStatistics _statistics = PlayerController.instance._statistics;
        EatingController _eatingController = PlayerController.instance._controller;

        switch (index)
        {
            case 0:
                _statistics.maxHealth = _statistics.maxHealth += healthMultiplier;
                break;

            case 1:
                _statistics.damage = Mathf.Round((_statistics.damage + damageMultiplier) * 10) / 10;
                break;

            case 2:
                _statistics.speed = Mathf.Round((_statistics.speed + speedMultiplier) * 10) / 10;
                break;

            case 3:
                _statistics.jump = Mathf.Round((_statistics.jump + jumpMultiplier) * 10) / 10;
                break;
        }

        _statistics.pointsUsed += 1;
        _eatingController.upgradePoints -= 1;
        if (_eatingController.upgradePoints <= 0)
        {
            _upgradeUI.parent.SetActive(false);
            GameController.isPaused = false;
        }
        else
            _upgradeUI.UpdateTexts();
    }
}
