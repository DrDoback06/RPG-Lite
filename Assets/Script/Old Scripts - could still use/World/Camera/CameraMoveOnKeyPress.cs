using UnityEngine;

public class CameraMoveOnKeyPress : MonoBehaviour
{
    public KeyCode moveKey = KeyCode.I;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private bool isMoving;
    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(moveKey))
        {
            isMoving = !isMoving;

            if (isMoving)
            {
                targetPosition = originalPosition + Vector3.right * moveDistance;
            }
            else
            {
                targetPosition = originalPosition;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
            }
        }
    }
}
