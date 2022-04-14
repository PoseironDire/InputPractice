using UnityEngine;
using UnityEngine.UI;

public class GameHost : MonoBehaviour
{
    [HideInInspector] public Controller[] players; //Global Player Array
    Camera cam;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    float fpsDeltaTime = 0;

    void Update()
    {
        players = FindObjectsOfType<Controller>(); /**/ if (players.Length != 0 && cam.gameObject.activeSelf) cam.gameObject.SetActive(false); //Disable Main Camera

        fpsDeltaTime += (Time.deltaTime - fpsDeltaTime) * 0.1f; /**/ float fps = 1.0f / fpsDeltaTime; /**/ GetComponentsInChildren<Text>()[0].text = Mathf.Ceil(fps).ToString(); //Draw Fps
    }
}
