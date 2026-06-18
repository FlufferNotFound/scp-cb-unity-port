
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    private bool _isMovingForward;

    private bool _isStrafingLeft;

    private bool _isMovingBackward;

    private bool _isStrafingRight;

    private bool _isQuickSave;

    private bool _isManualBlink;

    private bool _isSprinting;

    private bool _isCrouching;

    private bool _isInventoryOpen;

    private bool _isConsoleOpen;

    public bool IsMovingForward { get => _isMovingForward; set => _isMovingForward = value; }
    public bool IsStrafingLeft { get => _isStrafingLeft; set => _isStrafingLeft = value; }
    public bool IsMovingBackward { get => _isMovingBackward; set => _isMovingBackward = value; }
    public bool IsStrafingRight { get => _isStrafingRight; set => _isStrafingRight = value; }

    public bool IsQuickSave { get => _isQuickSave; set => _isQuickSave = value; }
    public bool IsManualBlink { get => _isManualBlink; set => _isManualBlink = value; }
    public bool IsSprinting { get => _isSprinting; set => _isSprinting = value; }
    public bool IsCrouching { get => _isCrouching; set => _isCrouching = value; }
    public bool IsInventoryOpen { get => _isInventoryOpen; set => _isInventoryOpen = value; }
    public bool IsConsoleOpen { get => _isConsoleOpen; set => _isConsoleOpen = value; }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    /*
     * This is how my teacher taught us to do inputs.
     * ngl, I don't like how it looks but I've used this 
     * on other projects and it works, so it stays for now,
     * but I wish there were a better way to do this.
    */

    private void OnEnable()
    {
        inputActions.Player.MoveForward.performed += StartMove;
        inputActions.Player.MoveForward.canceled += StopMove;

        inputActions.Player.StrafeLeft.performed += StartMove;
        inputActions.Player.StrafeLeft.canceled += StopMove;

        inputActions.Player.MoveBackward.performed += StartMove;
        inputActions.Player.MoveBackward.canceled += StopMove;

        inputActions.Player.StrafeRight.performed += StartMove;
        inputActions.Player.StrafeRight.canceled += StopMove;

        inputActions.Player.QuickSave.performed += StartMove;
        inputActions.Player.QuickSave.canceled += StopMove;

        inputActions.Player.ManualBlink.performed += StartMove;
        inputActions.Player.ManualBlink.canceled += StopMove;

        inputActions.Player.Sprint.performed += StartMove;
        inputActions.Player.Sprint.canceled += StopMove;

        inputActions.Player.Crouch.performed += StartMove;
        inputActions.Player.Crouch.canceled += StopMove;

        inputActions.Player.Inventory.performed += StartMove;
        inputActions.Player.Inventory.canceled += StopMove;

        inputActions.Player.Console.performed += StartMove;
        inputActions.Player.Console.canceled += StopMove;

        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.MoveForward.performed -= StartMove;
        inputActions.Player.MoveForward.canceled -= StopMove;

        inputActions.Player.StrafeLeft.performed -= StartMove;
        inputActions.Player.StrafeLeft.canceled -= StopMove;

        inputActions.Player.MoveBackward.performed -= StartMove;
        inputActions.Player.MoveBackward.canceled -= StopMove;

        inputActions.Player.StrafeRight.performed -= StartMove;
        inputActions.Player.StrafeRight.canceled -= StopMove;

        inputActions.Player.QuickSave.performed -= StartMove;
        inputActions.Player.QuickSave.canceled -= StopMove;

        inputActions.Player.ManualBlink.performed -= StartMove;
        inputActions.Player.ManualBlink.canceled -= StopMove;

        inputActions.Player.Sprint.performed -= StartMove;
        inputActions.Player.Sprint.canceled -= StopMove;

        inputActions.Player.Crouch.performed -= StartMove;
        inputActions.Player.Crouch.canceled -= StopMove;

        inputActions.Player.Inventory.performed -= StartMove;
        inputActions.Player.Inventory.canceled -= StopMove;

        inputActions.Player.Console.performed -= StartMove;
        inputActions.Player.Console.canceled -= StopMove;

        inputActions.Player.Disable();
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        _axisX = context.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _axisX = 0;
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        _isJumping = true;
    }

    private void StopJump(InputAction.CallbackContext context)
    {
        _isJumping = false;
    }
}
