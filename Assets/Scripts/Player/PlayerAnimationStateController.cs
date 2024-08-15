using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationStateController : MonoBehaviour
{
    Animator animator;

    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    private float acceleration = 5f;
    private float deceleration = 5f;
    [SerializeField] private float maximumMoveVelocity = 1f;

    private int VelocityXHash;
    private int VelocityZHash;
    private Vector2 movement;

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        //Debug.Log(movement);
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityXHash = Animator.StringToHash("VelocityX");
        VelocityZHash = Animator.StringToHash("VelocityZ");
    }

    void Update()
    {
        // float currentVelocity = movement.x != 0 || movement.y != 0 ? maximumMoveVelocity : maximumMoveVelocity;

        ChangeVelocity();

        Debug.Log("Velo " + velocityX + " , " + velocityZ);

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }

    void ChangeVelocity()
    {
        if (movement.y > 0 && velocityZ < maximumMoveVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        else if (movement.y < 0 && velocityZ > -maximumMoveVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        else if (movement.y == 0)
        {
            if (velocityZ > 0f)
            {
                velocityZ -= Time.deltaTime * deceleration;
            }
            else if (velocityZ < 0f)
            {
                velocityZ += Time.deltaTime * deceleration;
            }

            if (Mathf.Abs(velocityZ) < 0.1f)
            {
                velocityZ = 0f;
            }
        }

        if (movement.x < 0 && velocityX > -maximumMoveVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        else if (movement.x > 0 && velocityX < maximumMoveVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        else if (movement.x == 0)
        {
            if (velocityX > 0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }
            else if (velocityX < 0f)
            {
                velocityX += Time.deltaTime * deceleration;
            }

            if (Mathf.Abs(velocityX) < 0.1f)
            {
                velocityX = 0f;
            }
        }

        velocityZ = Mathf.Clamp(velocityZ, -maximumMoveVelocity, maximumMoveVelocity);
        velocityX = Mathf.Clamp(velocityX, -maximumMoveVelocity, maximumMoveVelocity);
    }
}
