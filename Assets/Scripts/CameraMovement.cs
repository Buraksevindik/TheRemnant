using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-1)]
public class CameraMovement : MonoBehaviour
{
    private CameraControlActions cameraControls;
    [SerializeField]
    Camera cam;
    Vector3 dragOrigin;
    public Vector3 difference;
    //LimitCamera
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    [SerializeField]
    private SpriteRenderer maprenderer;


    void Update()
    {
#if UNITY_EDITOR
        DragCam();
    }
#if UNITY_ANDROID
    private void Awake()
    {
        cameraControls = new CameraControlActions();
        //LimitCamera
        mapMinX = maprenderer.transform.position.x - maprenderer.bounds.size.x / 2f;
        mapMaxX = maprenderer.transform.position.x + maprenderer.bounds.size.x / 2f;

        mapMinY = maprenderer.transform.position.y - maprenderer.bounds.size.y / 2f;
        mapMaxY = maprenderer.transform.position.y + maprenderer.bounds.size.y / 2f;
    }
    private void OnEnable()
    {
        cameraControls.Enable();

    }
    private void OnDisable()
    {
        cameraControls.Disable();
    }
#endif
    //LimitCamera

    public Vector3 ClampCamera(Vector3 targetPosition) 
    {
        float camHeight = cam.orthographicSize;
        float camWidht = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidht;
        float maxX = mapMaxX - camWidht;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x,minX,maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX,newY,targetPosition.z);
    }
    private void DragCam()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            difference = dragOrigin - cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            print(cam.transform.position += difference);
            cam.transform.position = ClampCamera(cam.transform.position+difference);
        }
        dragOrigin = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
#endif


}

