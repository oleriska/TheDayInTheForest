using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Raft : MonoBehaviour
{
    public float paddleTimer = 0.0f;

    [SerializeField]
    private Transform PlayerPos;
    [SerializeField]
    private Transform left;
    [SerializeField]
    private Transform right;
    [SerializeField]
    private float paddleInterval = 1.5f;

    [SerializeField]
    private Image LeftCooldown;
    [SerializeField]
    private Image RightCooldown;

    private Rigidbody rb;
    private bool _playerSeated;

    private GameObject _player;
    private PlayerInput _playerInput;

    private InputAction _raftLeft;
    private InputAction _raftRight;
    private bool _movingToSeat = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInput = _player.GetComponent<PlayerInput>();

        _raftLeft = _playerInput.actions.FindAction("RaftLeft");
        _raftRight = _playerInput.actions.FindAction("RaftRight");
    }

    void Update()
    {
        Paddle();
        if (_movingToSeat)
        {
            MovePlayerToSeat();
        }
        UpdateCooldownUI();
    }

    private void UpdateCooldownUI()
    {
        // Update LeftCooldown and RightCooldown based on paddleTimer
        float fillValue = paddleTimer / paddleInterval; // Normalize timer between 0 and 1
        LeftCooldown.fillAmount = Mathf.Clamp01(fillValue);
        RightCooldown.fillAmount = Mathf.Clamp01(fillValue);
    }

    private void Paddle()
    {
        paddleTimer += Time.deltaTime;

        if (paddleTimer < paddleInterval)
        {
            return;
        }

        Vector3 forceDirection;
        Vector3 forcePosition;

        if (_raftLeft.triggered)
        {
            forceDirection = left.forward + left.right * 2f;
            forcePosition = left.position;
            rb.AddForceAtPosition(forceDirection.normalized * 4f, forcePosition, ForceMode.Impulse);
            paddleTimer = 0.0f;
        }

        if (_raftRight.triggered)
        {
            forceDirection = right.forward - right.right * 2f;
            forcePosition = right.position;
            rb.AddForceAtPosition(forceDirection.normalized * 4f, forcePosition, ForceMode.Impulse);
            paddleTimer = 0.0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player && !_playerSeated)
        {
            _movingToSeat = true;
            _player.GetComponent<CharacterController>().enabled = false;
            _player.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    private void MovePlayerToSeat()
    {
        // Gradually move the player towards the target position
        _player.transform.position = Vector3.Lerp(
            _player.transform.position,
            PlayerPos.position,
            Time.deltaTime * 3f
        );

        // Gradually rotate the player to match the PlayerPos rotation
        _player.transform.rotation = Quaternion.Lerp(
            _player.transform.rotation,
            PlayerPos.rotation,
            Time.deltaTime * 5f // Adjust the rotation speed
        );

        // Check if the player is close enough to the target position and rotation
        if (Vector3.Distance(_player.transform.position, PlayerPos.position) < 0.5f &&
            Quaternion.Angle(_player.transform.rotation, PlayerPos.rotation) < 5f) // Small angle threshold
        {
            _player.transform.position = PlayerPos.position; // Snap to position to avoid jitter
            _player.transform.rotation = PlayerPos.rotation; // Snap to rotation to avoid jitter
            _player.transform.SetParent(PlayerPos); // Parent to the seat position
            _movingToSeat = false; // Stop moving
            _playerSeated = true; // Player is now seated
        }
    }
}
