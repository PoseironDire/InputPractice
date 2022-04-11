using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public Camera cam;

    [HideInInspector] public Vector2 move; public void OnMove(InputAction.CallbackContext ctx) => move = ctx.ReadValue<Vector2>();
    [HideInInspector] public Vector2 lookGP; public void OnLookGP(InputAction.CallbackContext ctx) => lookGP = ctx.ReadValue<Vector2>();
    [HideInInspector] public Vector2 lookRC; public void OnLookRC(InputAction.CallbackContext ctx) => lookRC = ctx.ReadValue<Vector2>();
    [HideInInspector] public bool attack; public void OnAttack(InputAction.CallbackContext ctx) => attack = ctx.action.triggered;
    [HideInInspector] public bool boost; public void OnBoost(InputAction.CallbackContext ctx) => boost = ctx.action.triggered;
}
