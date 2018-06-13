using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform map;
    private float cameraSpeed = 3f;
    private float bottomTrigger, topTrigger, leftTrigger, rightTrigger;
    private bool isActive;

    void Awake()
    {
        topTrigger = Screen.height * 0.9f;
        bottomTrigger = Screen.height * 0.1f;
        leftTrigger = Screen.width * 0.1f;
        rightTrigger = Screen.width * 0.9f;
    }

    public void ActiveCamera(bool active)
    {
        isActive = active;
    }

    void Update()
    {
        if (isActive)
        {
            Vector2 mousePosition = Input.mousePosition;
            float moveSpeedY = 0;
            float moveSpeedX = 0;
            if (mousePosition.y > topTrigger)
            {
                if (map.localPosition.y > -380)
                {
                    moveSpeedY = -cameraSpeed * Time.deltaTime;
                }
            }
            else if (mousePosition.y < bottomTrigger)
            {
                if (map.localPosition.y < 380)
                {
                    moveSpeedY = cameraSpeed * Time.deltaTime;
                }
            }
            if (mousePosition.x > rightTrigger)
            {
                if (map.localPosition.x > -256)
                {
                    moveSpeedX = -cameraSpeed * Time.deltaTime;
                }
            }
            else if (mousePosition.x < bottomTrigger)
            {
                if (map.localPosition.x < 256)
                {
                    moveSpeedX = cameraSpeed * Time.deltaTime;
                }
            }
            map.position += new Vector3(moveSpeedX, moveSpeedY, 0);
        }
    }
}
