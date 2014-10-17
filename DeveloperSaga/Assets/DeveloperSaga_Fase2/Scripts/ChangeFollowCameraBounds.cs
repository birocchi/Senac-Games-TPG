using UnityEngine;
using System.Collections;

public class ChangeFollowCameraBounds : MonoBehaviour {

	public FollowObject followingCamera;
	public float newLowerLimit;

	void OnTriggerEnter2D(Collider2D other){
		followingCamera.minLimit.y = newLowerLimit;
	}
}
