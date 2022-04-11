using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Controller controller;
    CharacterController2D characterController;

    public int playerNumber; //Player Number

    [HideInInspector] public Vector2 move; //Player Move Input Vector
    [HideInInspector] public Vector2 lookGP; //Player Look Input Vector
    [HideInInspector] public bool rightClickHold; //Player Right Click Hold
    [HideInInspector] public bool boost; //Player Right Click Hold
    [HideInInspector] public bool fire; //Player Right Click Hold

    void Awake()
    {
        controller = new Controller();

        controller.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>(); controller.Player.Move.canceled += ctx => move = Vector2.zero;
        controller.Player.LookGP.performed += ctx => lookGP = ctx.ReadValue<Vector2>(); controller.Player.LookGP.canceled += ctx => lookGP = Vector2.zero;
        controller.Player.RightClick.performed += ctx => rightClickHold = true; controller.Player.RightClick.canceled += ctx => rightClickHold = false;
        controller.Player.Boost.performed += ctx => boost = true; controller.Player.Boost.canceled += ctx => boost = false;
        controller.Player.Fire.performed += ctx => fire = true; controller.Player.Fire.canceled += ctx => fire = false;
    }

    void OnEnable()
    {
        controller.Player.Enable();
    }

    void OnDisable()
    {
        controller.Player.Disable();

    }
}
