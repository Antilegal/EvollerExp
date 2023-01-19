using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    Vector2 move = Vector2.zero;
    Vector2 moveSpline = Vector2.zero;

    Vector3 rotation = Vector3.zero;

    float fly = 0f;
    float flySpline = 0f;

    float moveSpeed = 10f;

    float zoom = 0;
    float zoomSpline = 0f;

    void Start()
    {

    }

    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
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
        zoomSpline = Mathf.Lerp(zoomSpline, zoom, Time.fixedDeltaTime);

        moveSpline = Vector2.Lerp(moveSpline, move, Time.fixedDeltaTime);
        flySpline = Mathf.Lerp(flySpline, fly, Time.fixedDeltaTime);

        Vector3 shift = new Vector3(moveSpline.x, 0, moveSpline.y) * moveSpeed * Time.fixedDeltaTime;
        transform.position += transform.TransformDirection(shift) + (new Vector3(0f, flySpline, 0f) * moveSpeed * Time.fixedDeltaTime);

        Camera.main.fieldOfView += zoomSpline*100f * Time.fixedDeltaTime;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10f, 90f);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation.y, rotation.x, 0), Time.fixedDeltaTime);
    }

}
