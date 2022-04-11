using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    float fpsDeltaTime = 0;

    void Update()
    {
        { fpsDeltaTime += (Time.deltaTime - fpsDeltaTime) * 0.1f; /**/ float fps = 1.0f / fpsDeltaTime; /**/ GetComponentsInChildren<Text>()[0].text = Mathf.Ceil(fps).ToString(); } //Draw Fps
    }
}
