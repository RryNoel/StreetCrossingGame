using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public enum DirectionType
{
    Forward = 0,
    Back,
    Left,
    Right
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float tileSize = 1.0f;
    protected DirectionType type = DirectionType.Forward;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void Move(DirectionType moveType)
    {
        if (isMoving) return;

        switch (moveType)
        {
            case DirectionType.Forward:
                targetPosition += Vector3.forward * tileSize;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case DirectionType.Back:
                targetPosition += Vector3.back * tileSize;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case DirectionType.Left:
                targetPosition += Vector3.left * tileSize;
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case DirectionType.Right:
                targetPosition += Vector3.right * tileSize;
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }

        isMoving = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if (input.y > 0)
                Move(DirectionType.Forward);
            else if (input.y < 0)
                Move(DirectionType.Back);
            else if (input.x < 0)
                Move(DirectionType.Left);
            else if (input.x > 0)
                Move(DirectionType.Right);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            // 애니메이션 재생후 게임매니저의 게임 종료 로직
            Debug.Log("사망");
        }
    }
}
