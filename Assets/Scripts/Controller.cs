using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput; public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();
    [HideInInspector] public Vector2 lookInput; public void OnLook(InputAction.CallbackContext ctx) => lookInput = ctx.ReadValue<Vector2>();
    [HideInInspector] public bool useInput; public void OnUse(InputAction.CallbackContext ctx) => useInput = ctx.action.triggered;
    [HideInInspector] public bool boostInput; public void OnBoost(InputAction.CallbackContext ctx) => boostInput = ctx.action.triggered;
    [HideInInspector] public Vector2 formInput; public void OnForm(InputAction.CallbackContext ctx) => formInput = ctx.ReadValue<Vector2>();

    public Camera cam;
    public WeaponController weaponController;
    public PlayerInterface playerInterface;

    bool canSelect = true; //Used To Get Input Only From The Frame it Was Pressed
    public void FormSelector()
    {
        //These Vectors Stand For The 4 Inputs
        if (formInput == new Vector2(-1, 0) && canSelect) //Left
        {
            playerInterface.ToggleForm(1);
            canSelect = false;
        }
        if (formInput == new Vector2(0, -1) && canSelect) //Up
        {
            playerInterface.ToggleForm(2);
            canSelect = false;
        }
        if (formInput == new Vector2(1, 0) && canSelect) //Right
        {
            playerInterface.ToggleForm(3);
            canSelect = false;
        }
        if (formInput == new Vector2(0, 1) && canSelect) //Down
        {
            playerInterface.ToggleForm(4);
            canSelect = false;
        }
        if (formInput == Vector2.zero) canSelect = true; //Enable Form Select Again If No Button Is Currently Pressed

        playerInterface.DecideForm();

        weaponController.weaponType = playerInterface.currentForm;
    }
}