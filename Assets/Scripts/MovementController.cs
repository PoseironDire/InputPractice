using UnityEngine;

public class MovementController : Controller
{
    public Camera cam;
    Rigidbody2D rigidBody;
    Controller controller;
    WeaponController weaponController;
    Vector3 velocity = Vector3.zero;
    [Space]
    [Range(10, 500)] public float moveForce = 500; //Movement Force
    [Range(10, 500)] public float runMoveBonus = 250; //Additional Movement While Running
    [Range(0, 100)] public float turnForce = 10; //Turn Force
    [Range(0, 5)] public float timeToTurn = 0.3f; //Smooting Time For Turning

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
        weaponController = GetComponent<WeaponController>();

        GetComponentInChildren<TrailRenderer>().emitting = false; //Disable Trail
    }

    void FixedUpdate()
    {
        //Movement
        Move();
        //Rotation
        Look();
        //Boosting
        Boost();
        //Using
        Use();
    }

    void Move()
    {
        rigidBody.AddForce(cam.transform.rotation * move * (moveForce + weaponController.applyMoveUsePenalty + applyMoveRunBonus)); //Apply Movement Force
    }
    void Look()
    {
        //Rotate Player's Front Parallel To Right Joystick Based On Screen
        if (look != Vector2.zero) transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * look * (turnForce + weaponController.applyRotationUsePenalty), ref velocity, timeToTurn);
        else LookFree(); //Or Rotate Player's Front Parallel To Left Joystick Based On Screen
    }

    void LookFree()
    {
        //Rotate Player's Front Parallel To Left Joystick Based On Screen
        transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * move * (turnForce + weaponController.applyRotationUsePenalty), ref velocity, timeToTurn);
    }

    void Use()
    {
        if (use || weaponController.isUsing) weaponController.UseWeapon();
    }

    bool boosted = false; //Allows For Toggle Function
    bool isRunning = false; //True When Running
    public float applyMoveRunBonus = 0f;
    void Boost() //Running Method
    {
        if (boost && !boosted) //Toggle Function
        {
            boosted = true;
            isRunning = !isRunning;
        }
        else if (!boost) boosted = false;

        if (isRunning) //Start Running
        {
            applyMoveRunBonus = runMoveBonus;
            GetComponentInChildren<TrailRenderer>().emitting = true; //Enable Trail
        }
        float deadzone = 0.2f; //For Joysticks
        if (move.y <= deadzone && move.y >= -deadzone && move.x <= deadzone && move.x >= -deadzone || !isRunning) //Stop Running If Input Is Lost
        {
            isRunning = false;
            applyMoveRunBonus = 0;
            GetComponentInChildren<TrailRenderer>().emitting = false; //Disable Trail
        }
    }
}
