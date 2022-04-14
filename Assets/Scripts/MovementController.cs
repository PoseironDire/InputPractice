using UnityEngine;

public class MovementController : Controller
{
    [Range(10, 500)] public float moveForce = 500; //Movement Force
    [Range(10, 500)] public float runMoveBonus = 250; //Additional Movement While Running
    [Range(0, 100)] public float turnForce = 10; //Turn Force
    [Range(0, 5)] public float timeToTurn = 0.3f; //Smooting Time For Turning

    void FixedUpdate()
    {
        GetComponentInChildren<TrailRenderer>().emitting = false; //Disable Trail

        //Movement
        Move();
        //Rotation
        Look();
        //Boosting
        Boost();
        //Using
        Use();
        //Forms
        Forms();
    }

    void Move()
    {
        GetComponent<Rigidbody2D>().AddForce(cam.transform.rotation * moveInput * (moveForce + weaponController.applyMoveUsePenalty + applyMoveRunBonus)); //Apply Movement Force
    }

    Vector3 velocity = Vector3.zero;
    void Look()
    {
        //Rotate Player's Front Parallel To Right Joystick Based On Screen
        if (lookInput != Vector2.zero) transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * lookInput * (turnForce + weaponController.applyRotationUsePenalty), ref velocity, timeToTurn);
        else LookFree(); //Or Rotate Player's Front Parallel To Left Joystick Based On Screen
    }
    void LookFree()
    {
        //Rotate Player's Front Parallel To Left Joystick Based On Screen
        transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * moveInput * (turnForce + weaponController.applyRotationUsePenalty), ref velocity, timeToTurn);
    }

    void Use()
    {
        if (useInput || weaponController.isUsing) weaponController.UseWeapon();
    }

    void Forms()
    {
        FormSelector();
    }

    bool boosted = false; //Allows For Toggle Function
    bool isRunning = false; //True When Running
    float applyMoveRunBonus = 0f;
    void Boost() //Running Method
    {
        if (boostInput && !boosted) //Toggle Function
        {
            boosted = true;
            isRunning = !isRunning;
        }
        else if (!boostInput) boosted = false;

        if (isRunning) //Start Running
        {
            applyMoveRunBonus = runMoveBonus;
            GetComponentInChildren<TrailRenderer>().emitting = true; //Enable Trail
        }
        float deadzone = 0.2f; //For Joysticks
        if (moveInput.y <= deadzone && moveInput.y >= -deadzone && moveInput.x <= deadzone && moveInput.x >= -deadzone || !isRunning) //Stop Running If Input Is Lost
        {
            isRunning = false;
            applyMoveRunBonus = 0;
        }
    }
}
