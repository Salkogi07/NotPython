using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;

    Transform CameraLimit;
    Camera cam;

    public Transform[] Limit;

    public Transform[] bossCam;

    float height;
    float width;

    public int bossRoomNum = 0;


    private void Start()
    {
        cam = GetComponent<Camera>();
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        ChangeLimit(0);
    }

    public void ChangeLimit(int x)
    {
        if (Limit[x].CompareTag("BoseStage"))
        {
            cam.orthographicSize = 10;
        }
        CameraLimit = Limit[x];
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        float lx = CameraLimit.localScale.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + CameraLimit.position.x, lx + CameraLimit.position.x);

        float ly = CameraLimit.localScale.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + CameraLimit.position.y, ly + CameraLimit.position.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
