using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField][Range(0, 30)] float viewSize = 10; //View Size
    [SerializeField][Range(0, 1)] float cameraDamp = 0.5f; //Camera Damp
    [SerializeField] Camera cam; //Camera
    [SerializeField] Transform target; //Camera Target
    [SerializeField] Rigidbody2D playerRigidbody; //Player Rigidbody
    Vector3 velocity = Vector3.zero; //Vector Zero

    void FixedUpdate()
    {
        float zoomOut = playerRigidbody.velocity.magnitude * 0.1f; cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, viewSize + zoomOut, 0.1f); //View Size Zooming

        Vector3 point = cam.WorldToViewportPoint(target.position); //Follow Camera Target
        Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = new Vector3(target.position.x, target.position.y, -10) + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, cameraDamp);
    }
}
