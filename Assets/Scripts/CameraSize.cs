using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private Camera mainCamera;
    public Transform planetCenter;
    public Transform objectToFollow;
    public float zoomSpeed = 0.1f;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
    }

    //public void UpdateCameraZoom(float pushForce)
    //{
    //    float zoomAmount = pushForce * 0.1f; // Можно настроить коэффициент масштабирования
    //    float newZoom = mainCamera.orthographicSize + zoomAmount;
    //    mainCamera.orthographicSize = Mathf.Clamp(newZoom, 0.1f, 10.0f); // Ограничиваем масштаб камеры
    //}
   

    
   

    private void Update()
    {
        if (planetCenter == null || objectToFollow == null)
        {
            return;
        }

        Vector3 direction = objectToFollow.position - planetCenter.position;
        float distanceToPlanetEdge = direction.magnitude - planetCenter.localScale.x / 2; // Предполагается, что планета - это окружность

        float t = Mathf.InverseLerp(minDistance, maxDistance, distanceToPlanetEdge);
        float newZoom = Mathf.Lerp(minZoom, maxZoom, t);

        mainCamera.orthographicSize = newZoom;
    }
}
