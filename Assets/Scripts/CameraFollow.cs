using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject triger;
    public Vector3 offset;
    public float cameraSpeed = 0.125f;
    // Update is called once per frame
    void Update()
    {   
        Vector3 cameraMove = Vector3.Lerp(transform.position, triger.transform.position+offset, cameraSpeed);
        transform.position = cameraMove;
    }
}
