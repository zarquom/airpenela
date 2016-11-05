using UnityEngine;
using System.Collections;

public class AutoShoot : MonoBehaviour
{
    public float INITIAL_SPAWN_HEIGHT;
    public float STARTING_Y;

    private Object mBallPrefab;
    private Vector3 mHoopPosition;

    void Start()
    {
        mBallPrefab = Resources.Load("prefabs/Ball");
        mHoopPosition = GameObject.Find("Hoop").transform.position;
    }

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
                    //ShootBall(hit.point);
                }
            }
        }

    }

    public void ShootBall(Vector3 position, Material playerColorMaterial)
    {
        Vector3 ballSpawnPosition = new Vector3(position.x, position.y + INITIAL_SPAWN_HEIGHT, position.z);

        GameObject newBallObject = Instantiate(mBallPrefab, ballSpawnPosition, Quaternion.identity) as GameObject;
        newBallObject.GetComponent<ColorTinter>().SetPlayerColorMaterial(playerColorMaterial);

        ApplyInitialImpulse(newBallObject);
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

        result.x = distance.x / 1.35f;
        result.y = STARTING_Y;
        result.z = distance.z / 1.35f;

        return result;
    }

}
