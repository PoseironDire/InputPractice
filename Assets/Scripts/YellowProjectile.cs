using UnityEngine;

public class YellowProjectile : MonoBehaviour
{
    Rigidbody2D rb2D;
    WeaponController weaponController;
    GameObject hitEffectPrefab;
    Transform visual;
    Transform hitEffectSpawn;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        weaponController = FindObjectOfType<WeaponController>();
        hitEffectPrefab = (GameObject)Resources.Load("Prefabs/OrangeExplosion", typeof(GameObject));
        visual = transform.Find("Visual");
        hitEffectSpawn = transform.Find("HitEffectSpawn");

        rb2D.velocity = transform.up * weaponController.initialProjectileVelocity + transform.right * UnityEngine.Random.Range(weaponController.initialProjectileSpread, -weaponController.initialProjectileSpread); //Apply Velocity
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(GetComponent<AudioSource>().pitch, GetComponent<AudioSource>().pitch + 2); //Random Bitch
        Destroy(this.gameObject, 1); //Life Time
    }

    void FixedUpdate()
    {
        if (rb2D.bodyType != RigidbodyType2D.Static) rb2D.MoveRotation(Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg - 90); //Rotate Twoards Velocity If Projectile Isn't Static
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false; /**/ visual.gameObject.SetActive(false); //Disable Hitbox & Visuals
        GameObject projectile = Instantiate(hitEffectPrefab, hitEffectSpawn.position, transform.rotation); /**/ projectile.transform.SetParent(transform.parent); //Spawn & Make Hit Effect A Child Of This Game Object
    }
}
