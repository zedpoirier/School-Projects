using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    public float speed = 5f;
    public float rotY;
    public Vector3 moveDir;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            rotY = transform.rotation.eulerAngles.y;
        }
        if (Input.GetButton("Jump"))
        {
            CenterYourself();
        }
    }

    void Move()
    {
        Vector3 desiredMove = transform.right * Input.GetAxisRaw("Horizontal")
            + transform.forward * Input.GetAxisRaw("Vertical");

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hitInfo,
                           controller.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        if (transform.position.y - hitInfo.point.y > controller.height * 0.6f)
        {
            moveDir.y += Physics.gravity.y * Time.deltaTime * 0.5f;
        }
        else
        {
            moveDir.y = 0f;
        }

        moveDir.x = desiredMove.x * speed * Time.deltaTime;
        moveDir.z = desiredMove.z * speed * Time.deltaTime;

        controller.Move(moveDir);
    }

    void CenterYourself()
    {
        Quaternion current = Quaternion.Euler(transform.rotation.eulerAngles);
        Quaternion target = Quaternion.identity * Quaternion.Euler(0 , rotY, 0);
        transform.rotation = Quaternion.Slerp(current, target, 0.1f);
    }
}
