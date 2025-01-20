using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum State
{
    Standing,
    Patroling,
    Attacking,
}

[Serializable]
public class BehaviourState
{
    public State state;
    public UnityEvent acton;
}

public class EnemyController : MonoBehaviour
{
    public EntityStatistics _statistics;
    public State currentState;
    public List<BehaviourState> _behaviourStates = new();

    public float height;
    public LayerMask whatIsGround;
    public bool grounded;
    public Rigidbody2D rgBody;
    public EnemyMovement _movement;
    public EntityAnimation _animation;

    private void Update()
    {
        grounded = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0), Vector3.down, height, whatIsGround) || Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0), Vector3.down, height, whatIsGround);

        //Movement
        switch (currentState)
        {
            case State.Standing:
                _behaviourStates[0].acton.Invoke();
                break;

            case State.Patroling:
                _behaviourStates[1].acton.Invoke();
                break;

            case State.Attacking:
                _behaviourStates[2].acton.Invoke();
                break;
        }

        //Animation stuff
        if (grounded)
        {
            if (_movement.isMoving)
                _animation.ChangeTexure(1);
            else
                _animation.ChangeTexure(0);
        } else
            _animation.ChangeTexure(2);
    }

    private void FixedUpdate()
    {
        //Make movement depend on direction player is facing
        Vector3 move = (Vector3)_movement.movement.normalized;
        Vector3 rotatedMovement = transform.TransformDirection(move);

        //Move player
        rgBody.AddForce(rotatedMovement * _statistics.speed);
        rgBody.velocity = new(Mathf.Clamp(rgBody.velocity.x, -2, 2), rgBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _statistics.TakeDamage(PlayerController.instance._statistics.damage, transform, _animation.sprites[3]);
            PlayerController.instance.Jump(1.15f);
        }
    }
}
