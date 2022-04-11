using UnityEngine;

public class WeaponController : MonoBehaviour
{
    MovementController characterController;

    public bool ranged = false; //Is Ranged
    GameObject projectilePrefab; //Projectile
    Transform projectileSpawn; //Projectile Spawn Point
    [Range(0, 300)] public float initialProjectileVelocity; //Projectile Speed
    [Range(0, 50)] public float initialProjectileSpread; //Projectile Spread

    [Range(0, 5)] public float timeBetweenBursts; //Fires Per Second
    [Range(1, 10)] public int projectileBurst; //Fires Per Second
    [Range(0.1f, 1)] public float burstTime; //Time for Burst To Finish

    float timeSinceLastFire = 0f; //Time Since Last Fire
    float timeSinceLastBurst = 0f; //Time Since Last Burst
    int storedShots = 0;

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

    void Shoot()
    {
        if (!characterController.attacking) //Trigger Burst
        {
            storedShots = 0;
            timeSinceLastBurst = 0;
            characterController.attacking = true;
        }

        if (characterController.attacking && timeSinceLastFire > ((timeBetweenBursts * (burstTime * 10)) / (float)projectileBurst) / 10 && storedShots < projectileBurst)
        {
            characterController.applyMovePenalty = characterController.attackMovePenalty;
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation); //Spawn Projectile
            projectile.transform.SetParent(transform.parent); //Make The Projectile A Child Of This Game Object 
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            timeSinceLastFire = 0;
            storedShots++;
        }
        else characterController.applyMovePenalty = Mathf.Lerp(characterController.applyMovePenalty, 0, 0.1f); //Return To Original Move Speed

        if (timeSinceLastBurst > timeBetweenBursts)
        {
            characterController.attacking = false;
        }
    }

    void FixedUpdate()
    {
        timeSinceLastFire += Time.deltaTime;
        timeSinceLastBurst += Time.deltaTime;
    }
}
