using UnityEngine;
using System.Collections;

public class InitialVelosity : MonoBehaviour {

	public Vector3 initVel;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = initVel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
