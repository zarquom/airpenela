using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            Destroy(collider.gameObject);
        }
    }
}