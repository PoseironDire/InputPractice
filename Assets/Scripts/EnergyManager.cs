using UnityEngine;
using UnityEngine.Lumin;
using System.Collections.Generic;

public class EnergyManager : MonoBehaviour
{
    public bool colorize;
    GameObject damageText;
    Dictionary<SpriteRenderer, Color> originalColor = new Dictionary<SpriteRenderer, Color>();

    void Start()
    {
        damageText = (GameObject)Resources.Load("Prefabs/DamageText", typeof(GameObject));

        if (colorize) GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1);

        SpriteRenderer[] children = transform.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in children)
        {
            originalColor.Add(renderer, renderer.color);
        }
    }

    void FixedUpdate()
    {
        LerpColor();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DamageSource")
        {
            foreach (SpriteRenderer renderer in originalColor.Keys)
            {
                if (renderer.color != Color.red)
                    renderer.color = Color.red;
                else
                    renderer.color = Color.white;
            }
            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>(); //Spawn Damage Indicator
            indicator.transform.SetParent(transform.parent); //Make The Indicator A Child Of This Game Object
            indicator.SetDamageText(Random.Range(1, 100));
        }
    }

    public void LerpColor()
    {
        foreach (SpriteRenderer renderer in originalColor.Keys)
        {
            renderer.color = Color.Lerp(renderer.color, originalColor[renderer], 0.1f);
        }
    }
}
