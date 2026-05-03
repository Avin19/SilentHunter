using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTarget(Input.mousePosition);
        }

        Move();
    }

    void SetTarget(Vector3 screenPos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f; // VERY IMPORTANT for 2D

        targetPosition = worldPos;
        isMoving = true;
    }

    void Move()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            isMoving = false;
        }
    }
}