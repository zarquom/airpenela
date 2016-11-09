using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour
{

    public int mPlayerNum;
    public Material mBallColorMaterial;

    public float VELOCITY;
    public float GRAVITY;
    public float JUMP_INITIAL_VELOCITY;

    private bool mIsJumping = false;
    private float mInitialY;
    private float mVerticalAcceleration = 0.0f;

    private GameObject mAutoShootManager;

    void Start()
    {
        mInitialY = transform.position.y;
        mAutoShootManager = GameObject.Find("AutoShootManager");
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
        }
        else
        {
            float inputValueHorizontal = ReInput.players.GetPlayer(mPlayerNum - 1).GetAxis("Move Horizontal");
            float inputValueVertical = ReInput.players.GetPlayer(mPlayerNum - 1).GetAxis("Move Vertical");

            newPosition.x += inputValueHorizontal * VELOCITY * Time.deltaTime;
            newPosition.z += inputValueVertical * VELOCITY * Time.deltaTime;
        }

        transform.position = newPosition;
    }

    private void CheckInputForJump()
    {
        if ((!mIsJumping) && ((Input.GetKeyDown("joystick " + mPlayerNum + " button 0")) || (Input.GetKeyDown(KeyCode.Space))))
        {
            StartJump();
        }
    }

    private void CheckForShot()
    {
        if ((mIsJumping) && ((Input.GetKeyUp("joystick " + mPlayerNum + " button 0")) || (Input.GetKeyUp(KeyCode.Space))))
        {
            mAutoShootManager.GetComponent<AutoShoot>().ShootBall(transform.position, mBallColorMaterial);
        }
    }

    private void StartJump()
    {
        mIsJumping = true;
        mVerticalAcceleration = JUMP_INITIAL_VELOCITY;
    }

}
