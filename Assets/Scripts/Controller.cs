using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [HideInInspector] public Vector2 move; public void OnMove(InputAction.CallbackContext ctx) => move = ctx.ReadValue<Vector2>();
    [HideInInspector] public Vector2 look; public void OnLook(InputAction.CallbackContext ctx) => look = ctx.ReadValue<Vector2>();
    [HideInInspector] public bool use; public void OnUse(InputAction.CallbackContext ctx) => use = ctx.action.triggered;
    [HideInInspector] public bool boost; public void OnBoost(InputAction.CallbackContext ctx) => boost = ctx.action.triggered;
}
