using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public bool isPc;
    [SerializeField] private Transform interactorSource;
    [SerializeField] private float range;

    //private PlayerCustomActions playerCustomActions;
    private Vector2 movement, mouseLook, joystickLook;
    private Vector3 rotationTarget;

    InteractableObject currentInteractable;

    // private void Awake()
    // {
    //     playerCustomActions = new PlayerCustomActions();
    // }

    //Allow to use input from new input system
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        //Debug.Log(movement);
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
        //Debug.Log(mouseLook);
    }
    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
        //Debug.Log(joystickLook);
    }
    // private void StartInteraction()
    // {

    // }
    // private void EndInteraction()
    // {

    // }

    // private void OnEnable()
    // {
    //     playerCustomActions.Enable();
    // }

    // private void OnDisable()
    // {
    //     playerCustomActions.Disable();
    // }

    void Start()
    {
        
    }

    void Update()
    {
        if (isPc)
        {
            CheckInteraction();
            if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
            {
                currentInteractable.Interact();
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }
            PlayerMovementWithAim();
        }
        else
        {
            if (joystickLook.x == 0 && joystickLook.y == 0)
            {
                PlayerMovement();
            }
            else
            {
                PlayerMovementWithAim();
            }
        }
    }

    private void PlayerMovement()
    {
        Vector3 playerMovement = new Vector3(movement.x, 0f, movement.y);

        if (playerMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement), 0.2f);
        }
        transform.Translate(playerMovement * speed * Time.deltaTime, Space.World);
    }

    private void PlayerMovementWithAim()
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

    public void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(interactorSource.transform.position, interactorSource.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.tag == "Structure")
            {
                InteractableObject newInteractable = hit.collider.GetComponent<InteractableObject>();
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(InteractableObject newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HubController.instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable()
    {
        HubController.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
