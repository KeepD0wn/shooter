using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float mouseSence = 3f;
    float mouseAngleX;
    float mouseAngleY;    
    Quaternion origRotate;

    // Start is called before the first frame update
    void Start()
    {
        origRotate = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    public void CameraMove()
    {
        mouseAngleX += Input.GetAxis("Mouse X") * mouseSence;
        mouseAngleY += Input.GetAxis("Mouse Y") * mouseSence;

        mouseAngleY = Mathf.Clamp(mouseAngleY,-80,80); // ограничиваем взгляд потолок/пол

        Quaternion rotationX = Quaternion.AngleAxis(mouseAngleX, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(mouseAngleY,- Vector3.right);

        transform.rotation = origRotate * rotationX * rotationY;
    }
}
