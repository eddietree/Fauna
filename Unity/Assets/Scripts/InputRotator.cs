using UnityEngine;
using System.Collections;

public class InputRotator : MonoBehaviour {

	private Vector2 mousePosPrev;
	private Vector3 startPos;

	private Vector3 goalRotationalVel;

	// Use this for initialization
	void Start () {
		mousePosPrev = new Vector2 (0.0f, 0.0f);
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space))
			DoKick ();

		transform.position = startPos;
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

		rigidbody.angularVelocity = Vector3.Lerp (rigidbody.angularVelocity, goalRotationalVel, 0.05f);
	}

	void OnMouseDown() {
		//DoKick ();
	}

	void DoKick() 
	{
		audio.Play ();
		
		GameObject polyball = transform.FindChild ("Polyball").gameObject;
		polyball.GetComponent<Polyball>().OnClick ();
		
		GameObject.Find ("Katamari/PolyballLines").gameObject.GetComponent<PolyballLines> ().OnClick ();
	}

	void OnTouchStart( Vector2 aPos ) {

		goalRotationalVel = Vector3.zero;
	}
	
	void OnTouchEnd( Vector2 aPos, Vector2 aRelativePos ) {
		//goalRotationalVel = Vector3.zero;
		return;
	}
	
	void OnTouchMove( Vector2 aPos, Vector2 aRelativePos ) {

		if (rigidbody.isKinematic)
			return;

		Vector3 force = Vector3.right * aRelativePos.y  * Mathf.Abs(aRelativePos.y) - Vector3.up * aRelativePos.x* Mathf.Abs (aRelativePos.x);
		goalRotationalVel = force * 30.0f;

	}
}
