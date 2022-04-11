using UnityEngine;

public class YellowProjectile : MonoBehaviour
{
    Rigidbody2D rb2D; //Rigidbody
    CharacterController2D characterController; //Player

    [SerializeField] GameObject hitEffectPrefab; //Hit Effect
    [SerializeField] Transform hitEffectSpawn; //Hit Effect Spawn Point
    [SerializeField] GameObject visual; //Visual

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>(); //Get Rigidbody
        characterController = FindObjectOfType<CharacterController2D>(); //Get Controller

        //Get Random Spread
        rb2D.velocity = transform.up * characterController.initialProjectileVelocity + transform.right * UnityEngine.Random.Range(characterController.initialProjectileSpread, -characterController.initialProjectileSpread); ;

        //Config Sound
        GetComponent<AudioSource>().volume = characterController.projectileVolume;
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(characterController.minProjectilePitch, characterController.maxProjectilePitch);

        Destroy(this.gameObject, characterController.projectileLifeTime); //Destroy
    }

    void FixedUpdate()
    {
        //Rotate Twoards Velocity
        var dir = rb2D.velocity;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (rb2D.bodyType != RigidbodyType2D.Static) rb2D.MoveRotation(angle - 90);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false; //Disable Hitbox
        visual.gameObject.SetActive(false); //Disable Visuals
        GameObject projectile = Instantiate(hitEffectPrefab, hitEffectSpawn.position, hitEffectSpawn.rotation); //Spawn Hit Effect
        projectile.transform.SetParent(transform.parent); //Make The Hit Effect A Child Of This Game Object 
    }
}
