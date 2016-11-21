using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour
{

    public int mPlayerNum;
    public Material mBallColorMaterial;
    public GameObject hoop;
    public Animator anim;

    public float VELOCITY;
    public float GRAVITY;
    public float JUMP_INITIAL_VELOCITY;

    private bool mIsJumping = false;
    private float mInitialY;
    private float mVerticalAcceleration = 0.0f;

    private GameObject mAutoShootManager;
    private Joystick m_joystick;

    void Start()
    {
        mInitialY = transform.position.y;
        mAutoShootManager = GameObject.Find("AutoShootManager");
        m_joystick = (ReInput.players.GetPlayer(mPlayerNum - 1).controllers.GetController(ControllerType.Joystick, mPlayerNum - 1) as Joystick);
    }

    void Update()
    {
        CheckInputForJump();

        CheckForShot();

        Vector3 newPosition = transform.position;
        if (mIsJumping)
        {
            //if (mVerticalAcceleration < 0.0f) Debug.Log("MAX Y: " + transform.position.y);

            newPosition.y += mVerticalAcceleration * Time.deltaTime;
            mVerticalAcceleration -= GRAVITY;

            if (newPosition.y < mInitialY)
            {
                mIsJumping = false;
                newPosition.y = mInitialY;
                mVerticalAcceleration = 0.0f;
            }
            Vector3 bodyDirection = hoop.transform.position - this.transform.position;
            transform.forward = new Vector3(bodyDirection.x, 0, bodyDirection.y);
        }
        else
        {
            if (m_joystick == null) return;
            float inputValueHorizontal = m_joystick.GetAxisRawById(0);
            float inputValueVertical = -m_joystick.GetAxisRawById(1);

            newPosition.x += inputValueHorizontal * VELOCITY * Time.deltaTime;
            newPosition.z += inputValueVertical * VELOCITY * Time.deltaTime;

            if (inputValueHorizontal != 0 || inputValueVertical != 0)
            {
                transform.forward = new Vector3(inputValueHorizontal * VELOCITY * Time.deltaTime, 0, inputValueVertical * VELOCITY * Time.deltaTime);
            }
        }

        transform.position = newPosition;
    }

    private void CheckInputForJump()
    {
        if ((!mIsJumping) && (m_joystick.GetAnyButtonDown() || (Input.GetKeyDown(KeyCode.Space))))
        {
            StartJump();
        }
    }

    private void CheckForShot()
    {
        if ((mIsJumping) && (m_joystick.GetAnyButtonUp() || (Input.GetKeyUp(KeyCode.Space))))
        {
            mAutoShootManager.GetComponent<AutoShoot>().ShootBall(transform.position, mBallColorMaterial);
        }
    }

    private void StartJump()
    {
        mIsJumping = true;
        mVerticalAcceleration = JUMP_INITIAL_VELOCITY;
        anim.Play("jump");
    }

}
