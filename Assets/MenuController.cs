using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;

    private PlayerInput _inputAsset;
    private InputAction _openMenu;
    private StarterAssetsInputs playerInputs;
    private InventoryManager inventoryManager;

    void Start()
    {
        _inputAsset = GetComponent<PlayerInput>();
        playerInputs = GetComponent<StarterAssetsInputs>();
        inventoryManager = GetComponent<InventoryManager>();

        _openMenu = _inputAsset.actions.FindAction("ToggleMenu");
        _openMenu.performed += ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        if (Menu.activeInHierarchy == true)
        {
            Menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerInputs.cursorInputForLook = true;
            return;
        }
        Menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        playerInputs.cursorInputForLook = false;
    }

    public void Resume()
    {
        ToggleMenu(new InputAction.CallbackContext());
    }
    public void Exit()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
