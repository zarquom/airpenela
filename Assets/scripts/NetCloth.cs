using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetCloth : MonoBehaviour
{

    private const float GARBAGE_COLLECTOR_TIME_IN_SECONDS = 5.0f;
    private float mElapsedTime;

    private Cloth mClothComponent;

    void Start()
    {

        mElapsedTime = 0.0f;
        mClothComponent = GameObject.Find("NetCloth").GetComponent<Cloth>();
    }

    void Update()
    {
        mElapsedTime += Time.deltaTime;
        if (mElapsedTime >= GARBAGE_COLLECTOR_TIME_IN_SECONDS)
        {
            mElapsedTime = 0.0f;

            ClearMissingColliders();
        }
    }

    public void AddBallCollider(GameObject ballGameObject)
    {
        int currentAmountOfColliders = mClothComponent.sphereColliders.Length;

        SphereCollider newBallSphereCollider = ballGameObject.GetComponent<SphereCollider>();

        ClothSphereColliderPair[] newSphereColliders = new ClothSphereColliderPair[currentAmountOfColliders + 1];


        for (int i = 0; i < currentAmountOfColliders; ++i)
        {
            newSphereColliders[i] = mClothComponent.sphereColliders[i];
        }


        newSphereColliders[currentAmountOfColliders] = new ClothSphereColliderPair(newBallSphereCollider);

        mClothComponent.sphereColliders = newSphereColliders;
    }

    private void ClearMissingColliders()
    {
        List<ClothSphereColliderPair> n = new List<ClothSphereColliderPair>();
        foreach (ClothSphereColliderPair colliderPair in mClothComponent.sphereColliders)
        {
            if (colliderPair.first != null)
            {
                n.Add(colliderPair);
            }
        }

        ClothSphereColliderPair[] newSphereColliders = new ClothSphereColliderPair[n.Count];
        for (int i = 0; i < n.Count; ++i)
        {
            newSphereColliders[i] = n[i];
        }

        mClothComponent.sphereColliders = newSphereColliders;
    }
}
