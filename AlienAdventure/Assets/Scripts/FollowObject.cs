using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public Transform followedObject;
	public float dampingTime;
	public Vector2 minLimit = new Vector2(1,1);
	public Vector2 maxLimit = new Vector2(26,26);

	Vector3 velocity = Vector3.zero;
	
	void FixedUpdate () {
		//Smoothly follows the object
		if(followedObject != null){
			transform.position = new Vector3(Mathf.Clamp(Mathf.SmoothDamp(transform.position.x, followedObject.position.x, ref velocity.x, dampingTime),minLimit.x,maxLimit.x),
		    	                             Mathf.Clamp(Mathf.SmoothDamp(transform.position.y, followedObject.position.y, ref velocity.y, dampingTime),minLimit.y,maxLimit.y),
		        	                         transform.position.z);
		}
	}
}
