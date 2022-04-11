using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text boldText;
    public float lifetime = 0.6f;
    public float minDist = 1f;
    public float maxDist = 5f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    float damaged;
    Vector3 value;

    void Start()
    {
        float direction = Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float dist = Random.Range(minDist * damaged / 100, maxDist * damaged / 100);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
        value = new Vector3(damaged / 100, damaged / 100, damaged / 100);
    }

    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        Destroy(gameObject, lifetime);

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one + value, Mathf.Sin(timer / lifetime));
    }

    void FixedUpdate()
    {
        boldText.color = Color.Lerp(boldText.color, Color.yellow, 0.1f);
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(1, 3);
    }

    public void SetDamageText(int damage)
    {
        boldText.text = damage.ToString();
        damaged = damage;
    }
}