using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    //1 is forward, -1 backward, 0 idle
    private float _movingDirection;

    //1 is right, -1 left, 0 idle
    private float _strafingDirection;

    private float _mouseX;

    private float _mouseY;

    private bool _quickSave;

    private bool _manualBlink;

    private bool _isSprinting;

    private bool _isCrouching;

    private bool _inventoryInteract;

    private bool _consoleInteract;

    public float MovingDirection { get => _movingDirection; set => _movingDirection = value; }

    public float StrafingDirection { get => _strafingDirection; set => _strafingDirection = value; }

    public float MouseX { get => _mouseX; set => _mouseX = value; }

    public float MouseY { get => _mouseY; set => _mouseY = value; }

    public bool QuickSaving { get => _quickSave; set => _quickSave = value; }

    public bool ManualBlink { get => _manualBlink; set => _manualBlink = value; }

    public bool IsSprinting { get => _isSprinting; set => _isSprinting = value; }

    public bool IsCrouching { get => _isCrouching; set => _isCrouching = value; }

    public bool IsInventoryInteract { get => _inventoryInteract; set => _inventoryInteract = value; }

    public bool IsConsoleInteract { get => _consoleInteract; set => _consoleInteract = value; }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    /*
     * This is how my teacher taught us to do inputs.
     * ngl, I don't like how it looks but I've used this 
     * on other projects and it works, so it stays for now,
     * but I wish there were a better, less messier way to do this.
    */
    private void OnEnable()
    {
        inputActions.Player.Move.performed += StartMoving;
        inputActions.Player.Move.canceled += StopMoving;

        inputActions.Player.Strafe.performed += StartStrafing;
        inputActions.Player.Strafe.canceled += StopStrafing;

        inputActions.Player.QuickSave.performed += StartQuickSave;
        inputActions.Player.QuickSave.canceled += StopQuickSave;

        inputActions.Player.Blink.performed += StartBlink;
        inputActions.Player.Blink.canceled += StopBlink;

        inputActions.Player.Sprint.performed += StartSprint;
        inputActions.Player.Sprint.canceled += StopSprint;

        inputActions.Player.Crouch.performed += StartCrouch;
        inputActions.Player.Crouch.canceled += StopCrouch;

        inputActions.Player.Inventory.performed += StartInventoryInteract;
        inputActions.Player.Inventory.canceled += StopInventoryInteract;

        inputActions.Player.Console.performed += StartConsoleInteract;
        inputActions.Player.Console.canceled += StopConsoleInteract;

        inputActions.Player.MouseX.performed += StartMouseX;
        inputActions.Player.MouseX.canceled += StopMouseX;

        inputActions.Player.MouseY.performed += StartMouseY;
        inputActions.Player.MouseY.canceled += StopMouseY;

        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= StartMoving;
        inputActions.Player.Move.canceled -= StopMoving;

        inputActions.Player.Strafe.performed -= StartStrafing;
        inputActions.Player.Strafe.canceled -= StopStrafing;

        inputActions.Player.QuickSave.performed -= StartQuickSave;
        inputActions.Player.QuickSave.canceled -= StopQuickSave;

        inputActions.Player.Blink.performed -= StartBlink;
        inputActions.Player.Blink.canceled -= StopBlink;

        inputActions.Player.Sprint.performed -= StartSprint;
        inputActions.Player.Sprint.canceled -= StopSprint;

        inputActions.Player.Crouch.performed -= StartCrouch;
        inputActions.Player.Crouch.canceled -= StopCrouch;

        inputActions.Player.Inventory.performed -= StartInventoryInteract;
        inputActions.Player.Inventory.canceled -= StopInventoryInteract;

        inputActions.Player.Console.performed -= StartConsoleInteract;
        inputActions.Player.Console.canceled -= StopConsoleInteract;

        inputActions.Player.MouseX.performed += StartMouseX;
        inputActions.Player.MouseX.canceled += StopMouseX;

        inputActions.Player.MouseY.performed += StartMouseY;
        inputActions.Player.MouseY.canceled += StopMouseY;

        inputActions.Player.Disable();
    }

    private void StartMoving(InputAction.CallbackContext context)
    {
        _movingDirection = context.ReadValue<float>();
    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        _movingDirection = 0;
    }

    private void StartStrafing(InputAction.CallbackContext context)
    {
        _strafingDirection = context.ReadValue<float>();
    }

    private void StopStrafing(InputAction.CallbackContext context)
    {
        _strafingDirection = 0;
    }

    private void StartQuickSave(InputAction.CallbackContext context)
    {
        _quickSave = true;
    }

    private void StopQuickSave(InputAction.CallbackContext context)
    {
        _quickSave = false;
    }

    private void StartBlink(InputAction.CallbackContext context)
    {
        _manualBlink = true;
    }

    private void StopBlink(InputAction.CallbackContext context)
    {
        _manualBlink = false;
    }

    private void StartSprint(InputAction.CallbackContext context)
    {
        _isSprinting = true;
    }

    private void StopSprint(InputAction.CallbackContext context)
    {
        _isSprinting = false;
    }

    private void StartCrouch(InputAction.CallbackContext context)
    {
        _isCrouching = true;
    }

    private void StopCrouch(InputAction.CallbackContext context)
    {
        _isCrouching = false;
    }

    private void StartInventoryInteract(InputAction.CallbackContext context)
    {
        _inventoryInteract = true;
    }

    private void StopInventoryInteract(InputAction.CallbackContext context)
    {
        _inventoryInteract = false;
    }

    private void StartConsoleInteract(InputAction.CallbackContext context)
    {
        _consoleInteract = true;
    }

    private void StopConsoleInteract(InputAction.CallbackContext context)
    {
        _consoleInteract = false;
    }

    private void StartMouseX(InputAction.CallbackContext context) {
        _mouseX = inputActions.Player.MouseX.ReadValue<float>();
    }

    private void StartMouseY(InputAction.CallbackContext context) {
        _mouseY = inputActions.Player.MouseY.ReadValue<float>();
    }

    private void StopMouseX(InputAction.CallbackContext context) {
        _mouseX = 0;
    }

    private void StopMouseY(InputAction.CallbackContext context) {
        _mouseY = 0;
    }
}
