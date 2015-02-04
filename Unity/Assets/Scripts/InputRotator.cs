using UnityEngine;
using System.Collections;

public class InputRotator : MonoBehaviour {

	private Vector2 mousePosPrev;

	// Use this for initialization
	void Start () {
		mousePosPrev = new Vector2 (0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void HandleRotationInput()
	{
		//if (Input.GetMouseButton (0)) {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			
			if (touch.phase == TouchPhase.Began) {
				OnTouchStart (touch.position);
				
			} else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
				OnTouchMove (touch.position, touch.deltaPosition);
				
			} else if (touch.phase == TouchPhase.Ended) {
				OnTouchEnd (touch.position, touch.deltaPosition);
			}
		} else {
			
			Vector3 inputMousePos = Input.mousePosition;
			Vector2 mousePosCurr = new Vector2( inputMousePos.x, inputMousePos.y );
			Vector2 mousePosRel = mousePosCurr - mousePosPrev;
			
			if ( Input.GetMouseButtonDown(0) )
			{
				OnTouchStart ( mousePosCurr );
				
			}
			else if ( Input.GetMouseButtonUp(0) )
			{
				OnTouchEnd( mousePosCurr, mousePosRel );
			}
			else if ( Input.GetMouseButton(0) )
			{
				OnTouchMove( mousePosCurr, mousePosRel );
			}
			
			mousePosPrev = mousePosCurr;
		}
		
		//transform.Rotate (1.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HandleRotationInput ();

		
		print (rigidbody.angularVelocity.magnitude);
	}

	void OnMouseDown() {
		audio.Play ();

		GameObject polyball = transform.FindChild ("Polyball").gameObject;
		polyball.GetComponent<Polyball>().OnClick ();
	}

	void OnTouchStart( Vector2 aPos ) {
		rigidbody.angularVelocity = Vector3.zero;
	}
	
	void OnTouchEnd( Vector2 aPos, Vector2 aRelativePos ) {
	}
	
	void OnTouchMove( Vector2 aPos, Vector2 aRelativePos ) {
		
		Vector3 force = Vector3.right * aRelativePos.y - Vector3.up * aRelativePos.x;
		
		//rigidbody.rotation
		float speed = 600.0f;
		rigidbody.AddTorque( force * speed );
	}
}
