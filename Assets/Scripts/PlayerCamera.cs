using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public float viewSize; //View Size
    Rigidbody2D playerRigidbody; //Player Rigidbody
    CinemachineVirtualCamera virtualCam; //Virtual Camera
    Camera cam; //Camera

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        playerRigidbody = GetComponentInChildren<Rigidbody2D>();
        virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void Start()
    {
        //Assign Layers To Players
        MovementController[] characterMovements = FindObjectsOfType<MovementController>();
        int layer = characterMovements.Length + 9;

        virtualCam.gameObject.layer = layer;

        int bitMask = (1 << layer)
            | (1 << 0)
            | (1 << 1)
            | (1 << 2)
            | (1 << 4)
            | (1 << 5)
            | (1 << 8);

        cam.cullingMask = bitMask;
        cam.gameObject.layer = layer;
    }

    void FixedUpdate()
    {
        float zoomOut = playerRigidbody.velocity.magnitude * 0.2f; virtualCam.m_Lens.OrthographicSize = Mathf.Lerp(virtualCam.m_Lens.OrthographicSize, viewSize + zoomOut, 0.1f); //Zooming
    }
}
