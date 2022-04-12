using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        //Used On Game Object That Should Not Be Destroyed Across Scenes
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("DoNotDestroy");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

