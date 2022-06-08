using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWithScroll : MonoBehaviour
{
    CameraMovement cameraMovement;
    [SerializeField]
    private Camera cam;
    public CameraControlActions zoom;
    public float mouseScrollY;
    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;
    private void Awake()
    {

        zoom = new CameraControlActions();
        zoom.Camera.MouseScrollY.performed += x => mouseScrollY = x.ReadValue<float>();
    }
    private void Update()
    {
        if (mouseScrollY > 0)
        {
            ZoomIn();
        }
        if (mouseScrollY < 0)
        {
            ZoomOut();
        }
    }
    public void ZoomIn() 
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize,minCamSize,maxCamSize);

    }
    public void ZoomOut() 
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize,minCamSize,maxCamSize);
        cameraMovement = GetComponent<CameraMovement>();
        cam.transform.position = cameraMovement.ClampCamera(cam.transform.position + cameraMovement.difference);
    }
    #region -Enable / Disable
    private void OnEnable()
    {
        zoom.Enable();
    }
    private void OnDisable()
    {
        zoom.Disable();
    }
    #endregion
}
