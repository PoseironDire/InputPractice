using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Camera cam;
    public PlayerController controller;

    float fpsDeltaTime = 0;

    void Update()
    {
        if (controller.playerNumber == 1) { fpsDeltaTime += (Time.deltaTime - fpsDeltaTime) * 0.1f; /**/ float fps = 1.0f / fpsDeltaTime; /**/ GetComponentsInChildren<Text>()[0].text = Mathf.Ceil(fps).ToString(); } //Draw Fps
    }
}
