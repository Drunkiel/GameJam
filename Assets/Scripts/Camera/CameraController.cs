using System.Collections;
using UnityEngine;

public enum Move
{
    Up,
    Down,
    Left,
    Right,
}

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public bool onCooldown;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!IsTargetVisible(PlayerController.instance.gameObject) && !GameController.isPaused)
            AdjustCameraToPlayer();
    }

    public void MoveCamera(Move moveDirection)
    {
        if (onCooldown)
            return;

        switch (moveDirection)
        {
            case Move.Up:
                transform.position = new Vector3(transform.position.x, transform.position.y + 10, -10);
                break;
            case Move.Down:
                transform.position = new Vector3(transform.position.x, transform.position.y - 10, -10);
                break;
            case Move.Left:
                transform.position = new Vector3(transform.position.x - 17.77778f, transform.position.y, -10);
                break;
            case Move.Right:
                transform.position = new Vector3(transform.position.x + 17.77778f, transform.position.y, -10);
                break;
        }

        StartCoroutine(ResetCooldown());
    }

    private IEnumerator ResetCooldown()
    {
        onCooldown = true;

        yield return new WaitForSeconds(0.5f);

        onCooldown = false;
    }

    private bool IsTargetVisible(GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    private void AdjustCameraToPlayer()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        Vector3 cameraPosition = transform.position;

        float deltaX = playerPosition.x - cameraPosition.x;
        float deltaY = playerPosition.y - cameraPosition.y;

        if (Mathf.Abs(deltaX) > 8.88889f)
        {
            if (deltaX > 0)
                MoveCamera(Move.Right);
            else
                MoveCamera(Move.Left);
        }

        if (Mathf.Abs(deltaY) > 5f)
        {
            if (deltaY > 0)
                MoveCamera(Move.Up);
            else
                MoveCamera(Move.Down);
        }
    }
}
