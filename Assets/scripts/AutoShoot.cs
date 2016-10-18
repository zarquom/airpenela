using UnityEngine;
using System.Collections;

public class AutoShoot : MonoBehaviour
{
    public float INITIAL_SPAWN_HEIGHT;
    public float STARTING_Y;
    public float STARTING_X;

    private Object mBallPrefab;
    private Vector3 mHoopPosition;

    // Use this for initialization
    void Start()
    {
        mBallPrefab = Resources.Load("prefabs/Ball");
        mHoopPosition = GameObject.Find("Hoop").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform != null)
                {
                    Vector3 ballSpawnPosition = new Vector3(hit.point.x, hit.point.y + INITIAL_SPAWN_HEIGHT, hit.point.z);

                    GameObject newBallObject = Instantiate(mBallPrefab, ballSpawnPosition, Quaternion.identity) as GameObject;
                    ApplyInitialImpulse(newBallObject);
                }
            }
        }

    }

    private void ApplyInitialImpulse(GameObject newBallObject)
    {
        Vector3 impulseForce = CalculateInitialImpulse(newBallObject);
        Rigidbody rigidBody = newBallObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(impulseForce, ForceMode.Impulse);
    }

    private Vector3 CalculateInitialImpulse(GameObject newBallObject)
    {
        Vector3 result = new Vector3();

        Vector3 ballPosition = newBallObject.transform.position;

        Vector3 distance = mHoopPosition - ballPosition;

        result.x = distance.x / 1.39f;
        result.y = STARTING_Y;
        result.z = distance.z / 1.35f;

        return result;
    }

}
