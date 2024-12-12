using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepController : MonoBehaviour
{
    private AudioSource _audio;
    private PlayerInput _playerInput;
    private InputAction _move;
    private InputAction _running;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _playerInput = GetComponent<PlayerInput>();
        _move = _playerInput.actions.FindAction("Move");
        _running = _playerInput.actions.FindAction("Sprint");
    }

    // Update is called once per frame
    void Update()
    {
        if (_move.IsPressed())
        {
            _audio.enabled = true;

            if (_running.IsPressed())
            {
                _audio.pitch = 0.9f + Random.Range(-0.15f, 0.15f);
            }
            else
            {
                _audio.pitch = 0.75f + Random.Range(-0.15f, 0.15f);
            }
            return;
        }

        _audio.enabled = false;
    }
}
