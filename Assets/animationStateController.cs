using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{

    Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float deceleration = 2.0f;
    public float acceleration = 2.0f;
    int velocityXHash;
    int velocityZHash;

    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;



    void Start()
    {
        animator = GetComponent<Animator>();

        velocityZHash = Animator.StringToHash("VelocityZ");
        velocityXHash = Animator.StringToHash("VelocityX");
    }


    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {

        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }


        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool sprintPressed, float currentMaxVelocity)
    {

        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }



        if (!leftPressed && !rightPressed && velocityX != 0 && (velocityX > -0.5 && velocityX < 0.05))
        {
            velocityX = 0.0f;
        }


        if (forwardPressed && sprintPressed & velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05))
        {
            velocityZ = currentMaxVelocity;
        }



        if (leftPressed && sprintPressed & velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }

        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        if (rightPressed && sprintPressed & velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05))
        {
            velocityZ = currentMaxVelocity;
        }

    }



    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = sprintPressed ? maximumRunVelocity : maximumWalkVelocity;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, sprintPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, sprintPressed, currentMaxVelocity);


        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }
}
