using UnityEngine;
using System.Collections.Generic;

public class EnergyManager : MonoBehaviour
{
    GameObject damageText;

    public bool colorize;
    Dictionary<SpriteRenderer, Color> originalColor = new Dictionary<SpriteRenderer, Color>(); //Dictionary For Colors

    void Start()
    {
        damageText = (GameObject)Resources.Load("Prefabs/DamageText", typeof(GameObject));

        if (colorize) GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1); //Gain Random Color

        SpriteRenderer[] children = transform.GetComponentsInChildren<SpriteRenderer>(); //Save Colors To Dictionary
        foreach (SpriteRenderer renderer in children)
        {
            originalColor.Add(renderer, renderer.color);
        }
    }

    void FixedUpdate()
    {
        foreach (SpriteRenderer renderer in originalColor.Keys) //Lerp Color
        {
            renderer.color = Color.Lerp(renderer.color, originalColor[renderer], 0.1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DamageSource")
        {
            foreach (SpriteRenderer renderer in originalColor.Keys) //Red Tint If Hit By Damage Source (White Color Of The Object Already Is Red)
            {
                if (renderer.color != Color.red)
                    renderer.color = Color.red;
                else
                    renderer.color = Color.white;
            }

            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>(); /**/ indicator.transform.SetParent(transform.parent); //Spawn & Make The Indicator A Child Of This Game Object
            indicator.SetDamageText(Random.Range(1, 100));
        }
    }
}
