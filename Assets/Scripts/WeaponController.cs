using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform handle;
    Animator[] animators;
    MovementController characterController;
    GameObject projectilePrefab; GameObject projectileSpawn;

    public bool isRanged = false; GameObject ranged;
    public bool isMelee = false; GameObject melee;

    [Range(0, 300)] public float initialProjectileVelocity; //Projectile Speed
    [Range(0, 50)] public float initialProjectileSpread; //Projectile Spread

    [Range(0, 5)] public float useTime; //Time It Takes To Use
    [Range(1, 10)] public int burstAmount; //Amount of Times An Action Will Be Done In A Single Use
    [Range(0.1f, 1)] public float timeForBurst; //Time For All Those Actions To Finish

    int burstsCounted = 0;
    float actionTimer = 0f; //Tracker For When An Action Can Be Triggered Again
    float useTimer = 0f; //Tracker For When Use Can Be Used Again

    void Awake()
    {
        ranged = handle.Find("Ranged").gameObject;
        melee = handle.Find("Melee").gameObject;
        projectilePrefab = (GameObject)Resources.Load("Prefabs/YellowProjectile", typeof(GameObject));
        projectileSpawn = GameObject.Find("ProjectileSpawn");
        characterController = GetComponent<MovementController>();
        animators = handle.GetComponentsInChildren<Animator>();
    }

    public void UseWeapon()
    {
        if (isRanged) Shoot();
        if (isMelee) Swing();
    }

    [HideInInspector] public bool isUsing = false; //True When Using
    [HideInInspector] public float applyMoveUsePenalty = 0f;
    [HideInInspector] public float applyRotationUsePenalty = 0f;

    void Shoot()
    {
        if (!isUsing) //Trigger Use
        {
            burstsCounted = 0;
            useTimer = 0;
            isUsing = true;
        }

        if (isUsing && actionTimer > ((useTime * (timeForBurst * 10)) / (float)burstAmount) / 10 && burstsCounted < burstAmount)
        {
            applyMoveUsePenalty = characterController.attackMovePenalty; /**/ applyRotationUsePenalty = characterController.attackRotationPenalty; //Apply Penalties
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.transform.position, projectileSpawn.transform.rotation); /**/ projectile.transform.SetParent(transform.parent); //Spawn & Make The Projectile A Child Of This Game Object 
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            actionTimer = 0; /**/ burstsCounted++;
        }
        else
        {
            applyMoveUsePenalty = 0; //Return To Original Move Speed
            applyRotationUsePenalty = 0; //Return To Original Rotation Speed
        }

        if (useTimer > useTime)
        {
            isUsing = false;
        }
    }

    void Swing()
    {
        if (!isUsing) //Trigger Use
        {
            animators[0].SetBool("IsUsing", true);
            useTimer = 0;
            isUsing = true;
            applyMoveUsePenalty = characterController.attackMovePenalty; /**/ applyRotationUsePenalty = characterController.attackRotationPenalty; //Apply Penalties
        }
        if (useTimer > useTime)
        {
            animators[0].SetBool("IsUsing", false);
            isUsing = false;
            applyMoveUsePenalty = 0; //Return To Original Move Speed
            applyRotationUsePenalty = 0; //Return To Original Rotation Speed
        }
    }

    void Update()
    {
        if (isRanged) ranged.SetActive(true); else ranged.SetActive(false);
        if (isMelee) melee.SetActive(true); else melee.SetActive(false);
    }

    void FixedUpdate()
    {
        actionTimer += Time.deltaTime;
        useTimer += Time.deltaTime;
    }
}
