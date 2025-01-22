using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private float wanderingDistance;
    [SerializeField] private Vector2 positionToGo;

    public Vector2 movement;
    public bool isMoving;

    private bool isFlipped;
    private bool isNewPositionFound;

    [HideInInspector] public Vector3 move;
    private EnemyController _controller;

    private void Start()
    {
        _controller = GetComponent<EnemyController>();

        //Setting new first position to go
        startPosition = transform.position;
        SetNewPosition(wanderingDistance);
    }

    private void Update()
    {
        isMoving = _controller.rgBody.velocity.magnitude > 0.01f;

        if (GameController.isPaused)
            return;

        if (!isMoving && move.magnitude >= 1f && _controller.grounded)
            Jump();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10f)
        {
            print($"Enemy fall off the map {transform.name} on position: {transform.position}");
            Destroy(gameObject);
        };

        if (GameController.isPaused)
            move = Vector3.zero;
        else
            move = new Vector3(movement.x, 0, movement.y).normalized;
    }

    public void Patrolling()
    {
        //Checking distance to new position
        if (Vector3.Distance(transform.position, new(positionToGo.x, transform.position.y)) < 0.2f)
        {
            if (!isNewPositionFound)
            {
                StartCoroutine(PauseBeforeNewPosition());
                isNewPositionFound = true;
            }
            else
                movement = Vector2.zero;
        }
        else
            GoTo(positionToGo, positionToGo);
    }

    //Make it to be depended by skill
    private const float AllowedDistance = 1.0f;
    private const float TooCloseDistance = 0.5f;

    public void ApproachPlayer()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer > AllowedDistance)
            positionToGo = playerPosition;
        else
            positionToGo = transform.position;

        GoTo(positionToGo, playerPosition);
    }

    public void FleeFromPlayer()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer < TooCloseDistance)
        {
            Vector3 directionAwayFromPlayer = (transform.position - playerPosition).normalized;

            Vector3 newPosition = transform.position + directionAwayFromPlayer * (TooCloseDistance - distanceToPlayer);
            positionToGo = newPosition;
        }
        else
            positionToGo = transform.position;

        GoTo(positionToGo, positionToGo);
    }

    public void RandomRun()
    {
        //Checking distance to new position
        if (Vector3.Distance(transform.position, positionToGo) < 1f)
            SetNewPosition(wanderingDistance * 2);
        else
            GoTo(positionToGo, positionToGo);
    }

    private void SetNewPosition(float distance)
    {
        positionToGo = GetRandomPosition(distance);
    }

    private IEnumerator PauseBeforeNewPosition()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        isNewPositionFound = false;
        SetNewPosition(wanderingDistance);
    }

    public void GoTo(Vector3 position, Vector3 faceDirection)
    {
        Vector3 direction = position - transform.position;
        float newFaceDirection = (faceDirection - transform.position).x;
        movement = new Vector2(
            direction.x * _controller._statistics.speed,
            direction.z * _controller._statistics.speed
        );

        //Flipping Enemy to direction they are going
        if (newFaceDirection < 0 && !isFlipped)
        {
            transform.GetChild(0).localScale = new(-1, 1, 1);
            _controller._animation.light.transform.localScale = new(-1, 1, 1);
            isFlipped = true;
        }
        else if (newFaceDirection > 0 && isFlipped)
        {
            transform.GetChild(0).localScale = new(1, 1, 1);
            _controller._animation.light.transform.localScale = new(1, 1, 1);
            isFlipped = false;
        }
    }

    private Vector3 GetRandomPosition(float distance)
    {
        //Getting random position on X and Z axis
        float deltaX = Random.Range(-distance, distance);
        float deltaZ = Random.Range(-distance, distance);

        //New position
        Vector3 newPosition = new(startPosition.x + deltaX, transform.position.y, startPosition.z + deltaZ);
        return newPosition;
    }

    private void Jump()
    {
        _controller.rgBody.velocity = new Vector3(_controller.rgBody.velocity.x, 0f);
        _controller.rgBody.AddForce(_controller._statistics.jump * 10 * transform.up);
    }
}