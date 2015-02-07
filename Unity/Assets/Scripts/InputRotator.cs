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
	
		if (Input.GetKeyDown (KeyCode.Space))
			DoKick ();
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

		
		//print (rigidbody.angularVelocity.magnitude);
	}

	void OnMouseDown() {
		DoKick ();
	}

	void DoKick() 
	{
		audio.Play ();
		
		GameObject polyball = transform.FindChild ("Polyball").gameObject;
		polyball.GetComponent<Polyball>().OnClick ();
		
		GameObject.Find ("Katamari/PolyballLines").gameObject.GetComponent<PolyballLines> ().OnClick ();
	}

	void OnTouchStart( Vector2 aPos ) {
		//rigidbody.angularVelocity = Vector3.zero;
		rigidbody.angularDrag = 10.0f;
	}
	
	void OnTouchEnd( Vector2 aPos, Vector2 aRelativePos ) {

		rigidbody.angularDrag = 0.01f;

		Vector3 force = Vector3.right * aRelativePos.y - Vector3.up * aRelativePos.x;
		
		//transform.ro
		//rigidbody.rotation
		float speed = 600.0f;
		rigidbody.AddTorque( force * speed );

		//rigidbody.angularVelocity *= 50.0f;
	}
	
	void OnTouchMove( Vector2 aPos, Vector2 aRelativePos ) {
		
		Vector3 force = Vector3.right * aRelativePos.y  * Mathf.Abs(aRelativePos.y) - Vector3.up * aRelativePos.x* Mathf.Abs (aRelativePos.x);

		//transform.ro
		//rigidbody.rotation
		float speed = 400.0f;
		rigidbody.AddTorque( force * speed );

		//transform.Rotate
		rigidbody.angularVelocity *= 0.98f;
		rigidbody.angularDrag = Mathf.Lerp( rigidbody.angularDrag, 1.0f, 0.1f );

//		transform.Rotate ( new Vector3(aRelativePos.y, aRelativePos.x,0.0f));
	}
}
