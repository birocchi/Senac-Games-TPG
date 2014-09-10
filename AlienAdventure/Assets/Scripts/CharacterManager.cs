using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	//public GameObject player3;

	Vector3 lastPosition;
	Vector2 lastVelocity;
	Quaternion lastRotation;
	GameObject actualPlayer;
	FollowObject followingCamera;

	bool p1Selected, p2Selected;

	void Awake (){
		actualPlayer = GameObject.Find("Player1");
		followingCamera = GameObject.Find("Main Camera").GetComponent<FollowObject>();
	}

	// Update is called once per frame
	void Update () {

		p1Selected = Input.GetKeyDown(KeyCode.Alpha1);
		p2Selected = Input.GetKeyDown(KeyCode.Alpha2);

		if(p1Selected || p2Selected){
			lastPosition = new Vector3(actualPlayer.transform.position.x, actualPlayer.transform.position.y, actualPlayer.transform.position.z);
			lastRotation = new Quaternion(actualPlayer.transform.rotation.x,actualPlayer.transform.rotation.y,actualPlayer.transform.rotation.z,actualPlayer.transform.rotation.w);
			lastVelocity = new Vector3(actualPlayer.rigidbody2D.velocity.x, actualPlayer.rigidbody2D.velocity.y);

			if(p1Selected && !actualPlayer.Equals(player1)){
				Destroy(actualPlayer.gameObject);
				actualPlayer = (GameObject)Instantiate(player1, lastPosition, lastRotation);
			}else if(p2Selected && !actualPlayer.Equals(player2)){
				Destroy(actualPlayer.gameObject);
				actualPlayer = (GameObject)Instantiate(player2, lastPosition, lastRotation);
			}

			actualPlayer.rigidbody2D.velocity = lastVelocity;
			followingCamera.followedObject = actualPlayer.transform;
		}
	}
}
