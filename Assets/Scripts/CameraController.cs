using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    private float speed = 2;

    Vector2 move = Vector2.zero;
    Vector2 moveSpline = Vector2.zero;

    Vector3 rotation = Vector3.zero;

    float fly = 0f;
    float flySpline = 0f;

    float moveSpeed = 10f;

    float zoom = 0;
    float zoomSpline = 0f;

    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector3 look = value.Get<Vector2>();

        look.y = -look.y;
        
        rotation += look*0.1f;
    }

    private void OnZoom(InputValue value)
    {
        zoom = value.Get<float>();
    }

    private void OnJump(InputValue value)
    {
        fly = value.Get<float>();
    }

    private void FixedUpdate()
    {
        float delta = Time.unscaledDeltaTime * speed;

        zoomSpline = Mathf.Lerp(zoomSpline, zoom, delta);

        moveSpline = Vector2.Lerp(moveSpline, move, delta);
        flySpline = Mathf.Lerp(flySpline, fly, delta);

        Vector3 shift = new Vector3(moveSpline.x, 0, moveSpline.y) * moveSpeed * delta;
        transform.position += transform.TransformDirection(shift) + (new Vector3(0f, flySpline, 0f) * moveSpeed * delta);

        Camera.main.fieldOfView += zoomSpline * 100f * delta;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10f, 90f);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation.y, rotation.x, 0), delta);
    }

}
