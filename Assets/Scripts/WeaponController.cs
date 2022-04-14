using UnityEngine;

public class WeaponController : MonoBehaviour //(Spaghetti code atm, WIP)
{
    public Transform handle;
    public Animator swingAnimator;
    public Animator gunAnimator;
    public Animator muzzleAnimator;
    public GameObject swingHitbox;
    public GameObject ranged;
    public GameObject melee;

    MovementController characterController;

    public Transform projectileSpawn;
    GameObject projectilePrefab;
    [Space]
    [HideInInspector] public int weaponType = 1;
    [Range(0, 5)] public float useTime; //Time It Takes To Use
    [Space]
    [Range(0, 2)] public float swingRange = 0.5f; //Swing Range
    [Range(500, 2000)] public float swingForce = 1000; //Swing Force
    [Space]
    [Range(0, 300)] public float initialProjectileVelocity; //Projectile Speed
    [Range(0, 50)] public float initialProjectileSpread; //Projectile Spread
    [Range(1, 10)] public int burstAmount; //Amount of Times An Action Will Be Done In A Single Use
    [Range(0.1f, 1)] public float timeForBurst; //Time For All Those Actions To Finish
    [Space]
    [Range(-500, -10)] public float attackMovePenalty = -450; //Penalty Movement While Attacking
    [Range(-10, -1)] public float attackRotationPenalty = -8; //Penalty Rotation While Attacking

    float actionTimer = 0f; //Tracker For When An Action Can Be Triggered Again
    float useTimer = 0f; //Tracker For When Use Can Be Used Again

    void Awake()
    {
        characterController = GetComponent<MovementController>();
        projectilePrefab = (GameObject)Resources.Load("Prefabs/YellowProjectile", typeof(GameObject));
    }

    public void UseWeapon()
    {
        if (weaponType == 1) Shoot();
        if (weaponType == 2) Swing();
    }

    [HideInInspector] public bool isUsing = false; //True When Using
    [HideInInspector] public float applyMoveUsePenalty = 0f;
    [HideInInspector] public float applyRotationUsePenalty = 0f;

    int burstsCounted = 0;
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
            gunAnimator.SetBool("IsUsing", true);
            muzzleAnimator.SetBool("IsUsing", true);
            applyMoveUsePenalty = attackMovePenalty; /**/ applyRotationUsePenalty = attackRotationPenalty; //Apply Penalties
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.transform.position, projectileSpawn.transform.rotation); /**/ projectile.transform.SetParent(transform.parent); //Spawn & Make The Projectile A Child Of This Game Object 
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            actionTimer = 0; /**/ burstsCounted++;
        }
        else
        {
            gunAnimator.SetBool("IsUsing", false);
            muzzleAnimator.SetBool("IsUsing", false);
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
            swingAnimator.SetBool("IsUsing", true);
            useTimer = 0;
            isUsing = true;
            applyMoveUsePenalty = attackMovePenalty; /**/ applyRotationUsePenalty = attackRotationPenalty; //Apply Penalties
        }
        else
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(swingHitbox.transform.position, swingRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject != this.gameObject)
                {
                    if (hit.GetComponent<EnergyManager>() && swingHitbox.activeSelf) hit.GetComponent<EnergyManager>().DealDamage();
                    if (hit.GetComponent<Rigidbody2D>() && swingHitbox.activeSelf) hit.GetComponent<Rigidbody2D>().AddForce(transform.right * swingForce);
                }
            }
        }
        if (useTimer > useTime)
        {
            swingAnimator.SetBool("IsUsing", false);
            isUsing = false;
        }
    }

    void Update()
    {
        if (weaponType == 1) ranged.SetActive(true); else ranged.SetActive(false);
        if (weaponType == 2) melee.SetActive(true); else melee.SetActive(false);
    }

    void FixedUpdate()
    {
        actionTimer += Time.deltaTime;
        useTimer += Time.deltaTime;

        applyMoveUsePenalty = Mathf.Lerp(applyMoveUsePenalty, 0, 0.1f); ; //Return To Original Move Speed
        applyRotationUsePenalty = Mathf.Lerp(applyRotationUsePenalty, 0, 0.1f); //Return To Original Rotation Speed
    }
}
