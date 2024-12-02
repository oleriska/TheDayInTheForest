using UnityEngine;
using UnityEngine.InputSystem;

public class CompassManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput InputAsset;
    [SerializeField]
    private Transform _compassArrow;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _dial;
    [SerializeField]
    private float _rotationSpeed = 50f;

    private InputAction _rotateRight;
    private InputAction _rotateLeft;

    void Start()
    {
        _rotateLeft = InputAsset.actions.FindAction("DialRotateLeft");
        _rotateRight = InputAsset.actions.FindAction("DialRotateRight");
    }

    private void Update()
    {
        float playerYRotation = _player.eulerAngles.y;
        float compassZRotation = -playerYRotation;
        _compassArrow.eulerAngles = new Vector3(_compassArrow.eulerAngles.x, _compassArrow.eulerAngles.y, compassZRotation);
        float currentYRotation = _dial.localEulerAngles.y;

        if (_rotateLeft.IsPressed())
        {
            currentYRotation -= _rotationSpeed * Time.deltaTime;
        }

        if (_rotateRight.IsPressed())
        {
            currentYRotation += _rotationSpeed * Time.deltaTime;
        }

        _dial.localEulerAngles = new Vector3(_dial.localEulerAngles.x, currentYRotation, _dial.localEulerAngles.z);
    }
}
