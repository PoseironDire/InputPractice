using UnityEngine;

public class HitEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.1f);
    }
}
