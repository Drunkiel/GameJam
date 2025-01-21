using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EatingController : MonoBehaviour
{
    public int upgradePoints;
    public int foodPoints;

    public Slider eatingSlider;
    public CorpsController _corps;
    public PlayerController _player;

    private void GivePoint(int points)
    {
        upgradePoints += Mathf.FloorToInt(_corps._statistics.pointsUsed * 0.8f);
        foodPoints += points;
        if (foodPoints >= 5)
        {
            int multiplication = foodPoints / 5;

            foodPoints -= 5 * multiplication;
            upgradePoints += multiplication;
            UpgradeController.instance._upgradeUI.UpdateTexts();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Corpses"))
            _corps = collider.GetComponent<CorpsController>();
    }

    public IEnumerator Eat()
    {
        if (_player.isStopped)
            yield break;

        _player.isStopped = true;
        eatingSlider.gameObject.SetActive(true);
        eatingSlider.value = 0;
        eatingSlider.maxValue = _corps._statistics.maxHealth;
        _player._animation.ChangeTexure(4);

        while (eatingSlider.value < eatingSlider.maxValue)
        {
            yield return new WaitForSeconds(1f);
            eatingSlider.value += _player._statistics.damage;
        }

        GivePoint(_corps._statistics.maxHealth);
        _player.isStopped = false;
        eatingSlider.gameObject.SetActive(false);
        Destroy(_corps.gameObject);
        _corps = null;
    }
}
