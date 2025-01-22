using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public GameObject parent;

    public TMP_Text titleText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text speedText;
    public TMP_Text jumpText;

    public void UpdateTexts()
    {
        EntityStatistics _statistics = PlayerController.instance._statistics;
        UpgradeController _controller = UpgradeController.instance;

        titleText.text = $"Upgrade points: {PlayerController.instance._controller.upgradePoints}";
        healthText.text = $"<color=red>{_statistics.maxHealth}</color> => <color=green>{_statistics.maxHealth + _controller.healthMultiplier}</color>";
        damageText.text = $"<color=red>{_statistics.damage}</color> => <color=green>{_statistics.damage + _controller.damageMultiplier}</color>";
        speedText.text = $"<color=red> {_statistics.speed} </color> => <color=green> {_statistics.speed + _controller.speedMultiplier} </color>";
        jumpText.text = $"<color=red> {_statistics.jump} </color> => <color=green> {_statistics.jump + _controller.jumpMultiplier} </color>";
        GameController.isPaused = true;
        parent.SetActive(true);
    }
}
