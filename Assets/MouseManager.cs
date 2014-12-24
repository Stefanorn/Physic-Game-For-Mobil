using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	Rigidbody2D grabbedObject = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Vector3 mousWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousWorldPos3D.x , mousWorldPos3D.y);
			Vector2 dir = Vector2.zero;

			RaycastHit2D hit = Physics2D.Raycast( mousePos2D, dir );
			if(hit != null && hit.collider != null){
				if(hit.collider.rigidbody2D != null){
					grabbedObject = hit.collider.rigidbody2D;
				}
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			grabbedObject = null;	
		}
	
	}

	void FixedUpdate () {
		if (grabbedObject != null) {
			Vector3 mousWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousWorldPos3D.x , mousWorldPos3D.y);
			grabbedObject.position = mousePos2D;

		}
	}
}
