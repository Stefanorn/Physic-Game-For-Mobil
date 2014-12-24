using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	Rigidbody2D grabbedObject = null;
	public float dragSpeed = 5f;
	public LineRenderer dragLine;
	SpringJoint2D springJoint = null;
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
			if(hit.collider != null){
				if(hit.collider.rigidbody2D != null){
					grabbedObject = hit.collider.rigidbody2D;
					dragLine.enabled = true;

					springJoint = grabbedObject.gameObject.AddComponent<SpringJoint2D>();
					springJoint.anchor = grabbedObject.transform.InverseTransformPoint(hit.point);
					springJoint.connectedAnchor = mousWorldPos3D;
					springJoint.distance = 0.5f;
					springJoint.dampingRatio = 0;
					springJoint.frequency = 1;

					springJoint.collideConnected = true;
					springJoint.connectedBody = null;
				}
			}
		}
		if (Input.GetMouseButtonUp (0) && grabbedObject != null) {
			Destroy(springJoint);
			grabbedObject = null;	
			dragLine.enabled = false;
		}
		if (springJoint != null) {
			Vector3 mouseWorldPoint3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			springJoint.connectedAnchor = mouseWorldPoint3D;
		}
	}

//	void FixedUpdate () {
//		if (grabbedObject != null) {
//			Vector3 mousWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			Vector2 mousePos2D = new Vector2(mousWorldPos3D.x , mousWorldPos3D.y);
//
//			Vector2 dir = mousePos2D - grabbedObject.position;
//
//			dir *= dragSpeed;
//			grabbedObject.velocity = dir;
//
//			dragLine.SetPosition(0, new Vector3(grabbedObject.position.x, grabbedObject.position.y , -1));
//			dragLine.SetPosition(1, new Vector3(mousePos2D.x,mousePos2D.y, -1));
//
//
//		}
//	}
	void LateUpdate(){
		if (grabbedObject != null) {
				Vector3 worldAncor = grabbedObject.transform.TransformPoint(springJoint.anchor);

				dragLine.SetPosition (0, new Vector3 (worldAncor.x, worldAncor.y, -1));
				dragLine.SetPosition (1, new Vector3 (springJoint.connectedAnchor.x, springJoint.connectedAnchor.y, -1));
			}

	}
}
