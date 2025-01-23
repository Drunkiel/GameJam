using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private PlayerController _controller;
    public float damage;

    private void Start()
    {
        _controller = PlayerController.instance;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (_controller._statistics.health - damage > 0)
                _controller.Jump(1.15f);
            _controller._statistics.TakeDamage(damage, _controller.transform, _controller._animation.sprites[3], true);
        }
    }
}
