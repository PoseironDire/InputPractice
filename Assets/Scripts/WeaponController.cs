using UnityEngine;

public class WeaponController : MonoBehaviour
{
    MovementController characterController;

    public bool ranged = false;
    GameObject projectilePrefab;
    Transform projectileSpawn;
    [Range(0, 300)] public float initialProjectileVelocity; //Projectile Speed
    [Range(0, 50)] public float initialProjectileSpread; //Projectile Spread

    [Range(0, 5)] public float useTime; //Time It Takes To Use
    [Range(1, 10)] public int burstAmount; //Amount of Times An Action Will Be Done In A Single Use
    [Range(0.1f, 1)] public float timeForBurst; //Time For All Those Actions To Finish

    void Awake()
    {
        projectilePrefab = (GameObject)Resources.Load("Prefabs/YellowProjectile", typeof(GameObject));
        projectileSpawn = transform.Find("ProjectileSpawn");
        characterController = GetComponent<MovementController>();
    }

    public void UseWeapon()
    {
        if (ranged) Shoot();
    }

    [HideInInspector] public bool isUsing = false; //True When Using
    [HideInInspector] public float applyMoveUsePenalty = 0f;

    float actionTimer = 0f; //Tracker For When An Action Can Be Triggered Again
    float useTimer = 0f; //Tracker For When Use Can Be Used Again
    int storedShots = 0;
    void Shoot()
    {
        if (!isUsing) //Trigger Use
        {
            storedShots = 0;
            useTimer = 0;
            isUsing = true;
        }

        if (isUsing && actionTimer > ((useTime * (timeForBurst * 10)) / (float)burstAmount) / 10 && storedShots < burstAmount)
        {
            applyMoveUsePenalty = characterController.attackMovePenalty;
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation); /**/ projectile.transform.SetParent(transform.parent); //Spawn & Make The Projectile A Child Of This Game Object 
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            actionTimer = 0; /**/ storedShots++;
        }
        else applyMoveUsePenalty = Mathf.Lerp(applyMoveUsePenalty, 0, 0.1f); //Return To Original Move Speed

        if (useTimer > useTime)
        {
            isUsing = false;
        }
    }

    void FixedUpdate()
    {
        actionTimer += Time.deltaTime;
        useTimer += Time.deltaTime;
    }
}
