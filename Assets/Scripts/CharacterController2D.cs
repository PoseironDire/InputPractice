using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] Camera cam; //Camera
    [SerializeField] Transform target; //Camera Target
    [SerializeField] Rigidbody2D rigidBody; //Rigidbody
    [SerializeField] PlayerController controller; //Player Input
    Vector3 velocity = Vector3.zero; //Vector Zero

    [Space]

    [Range(10, 500)] public float speedForce = 100; //Speed Force
    [Range(100, 1000)] public float rushSpeedBonus = 50; //Speed While Rushing
    [Range(-1000, -100)] public float fireSpeedPenalty = -50; //Speed While Firing
    [Range(0, 10)] public float turnForce = 100; //Turn Force
    [Range(0, 5)] public float timeToTurn = 1; //Smooting Time For Turning

    [Space]

    public bool ranged = false; //Is Ranged
    public GameObject projectilePrefab; //Projectile
    public Transform projectileSpawnPoint; //Projectile Spawn Point
    [Range(0, 300)] public float initialProjectileVelocity; //Projectile Speed
    [Range(0, 50)] public float initialProjectileSpread; //Projectile Spread
    [Range(0, 1)] public float projectileLifeTime; //Projectile Life Time
    [Range(0, 5)] public float timeBetweenBursts; //Fires Per Second
    [Range(1, 10)] public int projectileBurst; //Fires Per Second
    [Range(0.1f, 1)] public float burstTime; //Time for Burst To Finish
    [Range(0, 1)] public float projectileVolume = 0.5f; //Volume
    [Range(0, 5)] public float minProjectilePitch = 1; //Minimum Pitch
    [Range(0, 5)] public float maxProjectilePitch = 2.5f; //Maximum Pitch

    [HideInInspector] public bool rushing = false; //Rushing Bool
    [HideInInspector] public float applyBoostSpeed = 0f; //Time Since Last Burst
    [HideInInspector] public bool attacking = false; //Firing Bool
    [HideInInspector] public float applyFireSpeed = 0f; //Time Since Last Burst
    float scopeDistance = 0.1f; //Distance Twoards Player's Direction
    float timeSinceLastFire = 0f; //Time Since Last Fire
    float timeSinceLastBurst = 0f; //Time Since Last Burst
    int storedShots = 0;

    void FixedUpdate()
    {
        timeSinceLastFire += Time.deltaTime;
        timeSinceLastBurst += Time.deltaTime;

        GetComponentInChildren<TrailRenderer>().emitting = false; //Disable Trail

        Move(); //Movement

        //Rotation
        if (controller.lookGP != Vector2.zero) GamePadRotation();
        else if (controller.rightClickHold) MouseRotation();
        else FreeRotation();

        if (controller.fire || attacking) Fire();
        if (controller.boost || rushing) Boost();
    }

    void Move()
    {
        rigidBody.AddForce(cam.transform.right * controller.move.x * (speedForce + applyFireSpeed + applyBoostSpeed)); //Apply X Movement
        rigidBody.AddForce(cam.transform.up * controller.move.y * (speedForce + applyFireSpeed + applyBoostSpeed));  //Apply Y Movement
    }

    void GamePadRotation()
    {   //Move Camera's Target Based On Joystick Input Times Max Scope Distance
        target.position = new Vector3(transform.position.x + controller.lookGP.x * scopeDistance, transform.position.y + controller.lookGP.y * scopeDistance);
        //Rotate Player's Front Parallel To Joystick Based On Screen
        transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * new Vector3(controller.lookGP.x * turnForce, controller.lookGP.y * turnForce), ref velocity, timeToTurn);
    }
    public void MouseRotation()
    {   //Get Position Difference Between Mouse & Player 
        Vector3 difference = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        //Move Camera's Target To Mouse OR Max Scope Distance
        target.position = new Vector3(Mathf.MoveTowards(transform.position.x, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, scopeDistance), Mathf.MoveTowards(transform.position.y, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y, scopeDistance));
        //Rotate Player's Front Twoards Mouse Based On Screen
        transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * new Vector3(difference.x * turnForce, difference.y * turnForce), ref velocity, timeToTurn);
    }

    void FreeRotation()
    {   //Move Camera's Target Based On Joystick Input Times Max Scope Distance
        target.position = new Vector3(transform.position.x + controller.move.x * scopeDistance, transform.position.y + controller.move.y * scopeDistance);
        //Rotate Player's Front Parallel To Joystick Based On Screen
        transform.up = Vector3.SmoothDamp(transform.up, cam.transform.rotation * new Vector3(controller.move.x * turnForce, controller.move.y * turnForce), ref velocity, timeToTurn);
    }

    public void Fire() //Firing Method
    {
        if (ranged)
        {

            if (!attacking) //Trigger Burst
            {
                storedShots = 0;
                timeSinceLastBurst = 0;
                attacking = true;
            }

            if (attacking && timeSinceLastFire > ((timeBetweenBursts * (burstTime * 10)) / (float)projectileBurst) / 10 && storedShots < projectileBurst)
            {
                applyFireSpeed = fireSpeedPenalty;
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation); //Spawn Projectile
                projectile.transform.SetParent(transform.parent); //Make The Projectile A Child Of This Game Object 
                Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                timeSinceLastFire = 0;
                storedShots++;
            }
        }

        if (timeSinceLastBurst > timeBetweenBursts)
        {
            applyFireSpeed = 0; //Return To Original Move Speed
            attacking = false;
        }
    }

    public void Boost() //Rushing Method
    {
        applyBoostSpeed = rushSpeedBonus;
        GetComponentInChildren<TrailRenderer>().emitting = true; //Enable Trail

        float deadzone = 0.2f; //For Joysticks
        if (controller.move.y <= deadzone && controller.move.y >= -deadzone && controller.move.x <= deadzone && controller.move.x >= -deadzone)
        {
            rushing = false; //Stop Rushing If Input Is Lost OR Rush Is Pressed Again
            applyBoostSpeed = 0;
        }
        else rushing = true; //Keep Rushing
    }
}
