using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : Controller
{
    Rigidbody2D rigidBody; //Rigidbody
    Controller controller; //Player Input
    WeaponController weaponController; //Player Input
    Vector3 velocity = Vector3.zero; //Vector Zero

    [Space]

    [Range(10, 500)] public float moveForce = 500; //Movement Force
    [Range(10, 500)] public float runMoveBonus = 250; //Additional Movement While Running
    [Range(-500, -10)] public float attackMovePenalty = -450; //Penalty Movement While Attacking
    [Range(0, 100)] public float turnForce = 10; //Turn Force
    [Range(0, 5)] public float timeToTurn = 0.3f; //Smooting Time For Turning

    [HideInInspector] public bool rushing = false; //Rushing Bool
    [HideInInspector] public float applyrunBonus = 0f; //Time Since Last Burst
    [HideInInspector] public bool attacking = false; //Firing Bool
    [HideInInspector] public float applyMovePenalty = 0f; //Time Since Last Burst

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
        weaponController = GetComponent<WeaponController>();
    }

    void FixedUpdate()
    {
        GetComponentInChildren<TrailRenderer>().emitting = false; //Disable Trail

        //Movement
        Move();
        //Rotation
        LookGP();
        //Boosting
        if (boost || rushing) Boost();
        //Firing
        if (attack || attacking) Attack();
    }
    void Move()
    {
        rigidBody.AddForce(cam.transform.rotation * move * (moveForce + applyMovePenalty + applyrunBonus)); //Apply X Movement
    }
    void LookGP()
    {
        Vector3 difference = difference = cam.ScreenToWorldPoint(lookRC) - transform.position;
        //Rotate Player's Front Parallel To Joystick Based On Screen
        if (lookGP != Vector2.zero) transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * new Vector3(lookGP.x * turnForce, lookGP.y * turnForce), ref velocity, timeToTurn);
        //Rotate Player's Front Twoards Mouse Based On Screen
        else if (Mouse.current.rightButton.isPressed) transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * new Vector3(difference.x * turnForce, difference.y * turnForce), ref velocity, timeToTurn * 2);
        else LookFree();
    }

    void LookFree()
    {
        //Rotate Player's Front Parallel To Joystick Based On Screen
        transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * new Vector3(move.x * turnForce, move.y * turnForce), ref velocity, timeToTurn);
    }

    void Attack()
    {
        weaponController.UseWeapon();
    }

    void Boost() //Rushing Method
    {
        applyrunBonus = runMoveBonus;
        GetComponentInChildren<TrailRenderer>().emitting = true; //Enable Trail

        float deadzone = 0.2f; //For Joysticks
        if (move.y <= deadzone && move.y >= -deadzone && move.x <= deadzone && move.x >= -deadzone)
        {
            rushing = false; //Stop Rushing If Input Is Lost OR Rush Is Pressed Again
            applyrunBonus = 0;
        }
        else rushing = true; //Keep Rushing
    }
}
