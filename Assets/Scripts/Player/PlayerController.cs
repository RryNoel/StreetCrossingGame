using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
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
    public LayerMask layerMask;
    private bool isMoving = false;
    private bool isInputActive = false;

    private Raft raftObj;
    private Vector3 raftOffSetPos = Vector3.zero;
    private Vector3 previousRaftPosition;
    private bool isOnRaft = false;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        targetPosition = transform.position;
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        EnvironmentManager.Instance.UpdateForwardMap((int)transform.position.z);
    }

    private void Update()
    {
        InputUpdate();
        if (isOnRaft)
        {
            UpdateRaftMovement();
        }
    }

    private bool IsCheckObject(DirectionType moveType)
    {
        Vector3 direction = Vector3.zero;

        switch (moveType)
        {
            case DirectionType.Forward:
                direction = Vector3.up;
                break;
            case DirectionType.Back:
                direction = Vector3.back;
                break;
            case DirectionType.Left:
                direction = Vector3.left;
                break;
            case DirectionType.Right:
                direction = Vector3.right;
                break;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, tileSize, layerMask))
        {
           return false;
        }

        return true;
    }

    private void Move(DirectionType moveType)
    {
        if (isMoving) return;
        if (!IsCheckObject(moveType)) return;

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
            isInputActive = true;

            if (input.y > 0)
                Move(DirectionType.Forward);
            else if (input.y < 0)
                Move(DirectionType.Back);
            else if (input.x < 0)
                Move(DirectionType.Left);
            else if (input.x > 0)
                Move(DirectionType.Right);

            CharacterManager.Instance.Player.anim.SetWalkAnim();
            CharacterManager.Instance.Player.anim.InvokeOutAnim();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isInputActive = false;
        }
    }

    private void InputUpdate()
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

    private void UpdateRaftMovement()
    {
        if(raftObj == null) return;

        Vector3 raftMovement = raftObj.transform.position - previousRaftPosition;
        previousRaftPosition = raftObj.transform.position;

        EnvironmentManager.Instance.UpdateForwardMap((int)transform.position.z);

        if (!isInputActive)
        {
            transform.position += raftMovement;
            targetPosition += raftMovement;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Raft raft = other.GetComponent<Raft>();

        if (other.gameObject.CompareTag("Raft") && raft != null)
        {
            raftObj = raft;
            previousRaftPosition = raft.transform.position;
            raftOffSetPos = transform.position - raft.transform.position;
            isOnRaft = true;
        }

        if (other.CompareTag("Car"))
        {
            // 애니메이션 재생후 게임매니저의 게임 종료 로직
            Debug.Log("사망");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (raftObj != null && raftObj.transform == other.transform)
        {
            StartCoroutine(SmoothExitRaft());
        }
    }

    private IEnumerator SmoothExitRaft()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = raftObj.transform.position + (transform.position - raftObj.transform.position);
        float duration = 0.5f; // 이동 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        targetPosition = transform.position;

        raftObj = null;
        isOnRaft = false;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Pause.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.OnPauseEvent(!GameManager.Instance.IsPaused);
        }
    }
}