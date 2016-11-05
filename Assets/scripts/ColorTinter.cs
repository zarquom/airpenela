using UnityEngine;
using System.Collections;

public class ColorTinter : MonoBehaviour {

    private Material mPlayerColorMaterial;
    private Material mTextureMaterial;

	void Start () {

        mTextureMaterial = Resources.Load("materials/ball", typeof(Material)) as Material;
	}
	
	void Update () {

        Renderer renderer = transform.GetComponent<Renderer>();

        float lerp = Mathf.PingPong(Time.time * 3.0f, 1.0f) / 1.0f;
        renderer.material.Lerp(mTextureMaterial, mPlayerColorMaterial, lerp);
	}

    public void SetPlayerColorMaterial(Material playerColorMaterial)
    {
        mPlayerColorMaterial = playerColorMaterial;
    }
}
