using UnityEngine;
using System.Collections;

public class FollowObjectBasic : MonoBehaviour {
	public Transform followedObject;
	
	void FixedUpdate () {
		if(followedObject != null){
			transform.position = new Vector3(followedObject.position.x, followedObject.position.y, -3);
		}
	}
}
