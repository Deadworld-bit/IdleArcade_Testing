using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public bool isPc;
    private Vector2 movement, mouseLook, joystickLook;
    private Vector3 rotationTarget;

    //Allow to use input from new input system
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (isPc)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }
            playerMovementWithAim();
        }
        else
        {
            if (joystickLook.x == 0 && joystickLook.y == 0)
            {
                playerMovement();
            }
            else
            {
                playerMovementWithAim();
            }
        }
    }

    private void playerMovement()
    {
        Vector3 playerMovement = new Vector3(movement.x, 0f, movement.y);

        if (playerMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement), 0.2f);
        }
        transform.Translate(playerMovement * speed * Time.deltaTime, Space.World);
    }

    private void playerMovementWithAim()
    {
        if (isPc)
        {
            var lookPosition = rotationTarget - transform.position;
            lookPosition.y = 0;
            var rotation = Quaternion.LookRotation(lookPosition);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }

        Vector3 playerMovement = new Vector3(movement.x, 0f, movement.y);
        transform.Translate(playerMovement * speed * Time.deltaTime, Space.World);
    }
}
