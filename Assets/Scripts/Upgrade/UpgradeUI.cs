using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public GameObject parent;

    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text speedText;
    public TMP_Text jumpText;

    public void UpdateTexts()
    {
        EntityStatistics _statistics = PlayerController.instance._statistics;
        UpgradeController _controller = UpgradeController.instance;

        healthText.text = $"{_statistics.maxHealth} => {_statistics.maxHealth + _controller.healthMultiplier}";
        damageText.text = $"{_statistics.damage} => {_statistics.damage + _controller.damageMultiplier}";
        speedText.text = $"{_statistics.speed} => {_statistics.speed + _controller.speedMultiplier}";
        jumpText.text = $"{_statistics.jump} => {_statistics.jump + _controller.jumpMultiplier}";
        parent.SetActive(true);
    }
}
