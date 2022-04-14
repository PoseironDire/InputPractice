using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public Controller[] players; //Individual Player Array
    GameHost playerManager;
    Rigidbody2D playerRigidbody;
    CinemachineVirtualCamera virtualCam; Camera cam;

    public float viewSize; float splitViewSize;

    void Awake()
    {
        players = FindObjectsOfType<Controller>();
        playerManager = FindObjectOfType<GameHost>();
        cam = GetComponentInChildren<Camera>();
        playerRigidbody = GetComponentInChildren<Rigidbody2D>();
        virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void Start()
    {
        //Assign Layers To Players (No Idea What This Really Does It Just Works)
        int layer = players.Length + 9;
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

    void Update()
    {
        if (playerManager.players.Length == 2) //If Total Players Are 2
        {
            //Camera Sizing
            if (cam.gameObject.layer == 10)
            {
                cam.rect = new Rect(cam.rect.x, 0.5f, 1, 0.5f);
            }
            if (cam.gameObject.layer == 11)
            {
                cam.rect = new Rect(cam.rect.x, cam.rect.y, 1, 0.5f);
            }
            splitViewSize = -viewSize / 4;
        }
        else if (playerManager.players.Length == 3) //If Total Players Are 3
        {
            //Camera Sizing
            if (cam.gameObject.layer == 10) cam.rect = new Rect(cam.rect.x, 0.5f, 1, 0.5f);
            if (cam.gameObject.layer == 11) cam.rect = new Rect(cam.rect.x, cam.rect.y, 0.5f, 0.5f);
            if (cam.gameObject.layer == 12) cam.rect = new Rect(0.5f, cam.rect.y, 0.5f, 0.5f);
            splitViewSize = viewSize / 4;
        }
        else if (playerManager.players.Length == 4) //If Total Players Are 4
        {
            //Camera Sizing
            if (cam.gameObject.layer == 10) cam.rect = new Rect(cam.rect.x, 0.5f, 0.5f, 0.5f);
            if (cam.gameObject.layer == 11) cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            if (cam.gameObject.layer == 12) cam.rect = new Rect(cam.rect.x, cam.rect.y, 0.5f, 0.5f);
            if (cam.gameObject.layer == 13) cam.rect = new Rect(0.5f, cam.rect.y, 0.5f, 0.5f);
            splitViewSize = viewSize / 4;
        }
        else //If Total Players Are 1
        {
            cam.rect = new Rect(cam.rect.x, 0, 1, 1); //Camera Sizing
            splitViewSize = 0;
        }
    }

    void FixedUpdate()
    {
        float zoomOut = playerRigidbody.velocity.magnitude * 0.2f; virtualCam.m_Lens.OrthographicSize = Mathf.Lerp(virtualCam.m_Lens.OrthographicSize, viewSize + splitViewSize + zoomOut, 0.05f); //Zooming
    }
}
