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

    private void GivePoint()
    {
        foodPoints += 1;
        if (foodPoints >= 5)
        {
            foodPoints -= 5;
            upgradePoints += 1;
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

        for (int i = 0; i < eatingSlider.maxValue; i++)
        {
            yield return new WaitForSeconds(1f);
            eatingSlider.value += 1;
            GivePoint();
        }

        _player.isStopped = false;
        eatingSlider.gameObject.SetActive(false);
        Destroy(_corps.gameObject);
        _corps = null;
    }
}
