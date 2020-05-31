using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followObject;
    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        var objPos = cam.WorldToScreenPoint(followObject.position);
        var screenSize = new Vector2(Screen.width, Screen.height);
        if (Vector2.Distance(objPos, screenSize / 2) > 0) {
            var newPos = Vector2.Lerp(transform.position, followObject.position, 1f);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
