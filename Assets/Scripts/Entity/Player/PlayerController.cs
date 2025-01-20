using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public bool isStopped;

    public EntityStatistics _statistics;
    public float height;
    public LayerMask whatIsGround;
    [SerializeField] private bool grounded;
    private bool isFlipped;
    private Vector2 movement;
    public List<bool> additionalJumps = new();

    public Rigidbody2D rgBody;
    public EntityAnimation _animation;
    public EatingController _controller;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        grounded = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0), Vector3.down, height, whatIsGround) || Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0), Vector3.down, height, whatIsGround);

        if (isStopped)
            return;

        if (grounded)
        {
            if (movement.magnitude > 0.01f)
                _animation.ChangeTexure(1);
            else
                _animation.ChangeTexure(0);
        } else
            _animation.ChangeTexure(2);
    }

    private void FixedUpdate()
    {
        //Make movement depend on direction player is facing
        Vector3 move = isStopped ? Vector3.zero : (Vector3)movement.normalized;
        Vector3 rotatedMovement = transform.TransformDirection(move);

        //Move player
        rgBody.AddForce(rotatedMovement * _statistics.speed);
        rgBody.velocity = new(Mathf.Clamp(rgBody.velocity.x, -2, 2), rgBody.velocity.y);
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();

        //Flipping player to direction they are going
        if (inputValue.x < 0 && !isFlipped)
        {
            transform.GetChild(0).localScale = new(-1, 1, 1);
            isFlipped = true;
        }
        else if (inputValue.x > 0 && isFlipped)
        {
            transform.GetChild(0).localScale = new(1, 1, 1);
            isFlipped = false;
        }

        movement = new Vector2(inputValue.x, 0);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (grounded)
            {
                Jump();
                ResetJumps();
            }
            else if (CheckIfCanJump())
                Jump();
        }
    }

    public void EatingInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (grounded && GetComponent<TriggerController>().isTriggered)
                StartCoroutine(_controller.Eat());
        }
    }

    public void Jump(float multiplier = 1f)
    {
        if (isStopped)
            return;

        rgBody.velocity = new Vector3(rgBody.velocity.x, 0f);
        rgBody.AddForce(transform.up * (_statistics.jump * multiplier));
    }

    private bool CheckIfCanJump()
    {
        if (additionalJumps.Count == 0)
            return false;

        for (int i = 0; i < additionalJumps.Count; i++)
        {
            if (!additionalJumps[i])
            {
                additionalJumps[i] = true;
                return true;
            }
        }

        return false;
    }

    private void ResetJumps()
    {
        if (additionalJumps.Count == 0)
            return;

        for (int i = 0; i < additionalJumps.Count; i++)
            additionalJumps[i] = false;
    }
}
