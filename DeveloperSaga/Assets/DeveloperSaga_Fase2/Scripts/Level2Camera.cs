using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class Level2Camera : MonoBehaviour {

	public Transform player;

	public float dampingTime = 0.1f;
	public float xMin;
	public float xMax;
	public float groundLevel;
	public float minY1;
	public float minY2;

	private float actualMin;

	
	Vector3 velocity = Vector3.zero;
	
	void FixedUpdate () {
		//Smoothly follows the object
		if(player != null){

			if(player.position.y >= groundLevel){
				actualMin = minY1;
			} else{
				actualMin = minY2;
			}

			transform.position = new Vector3(Mathf.Clamp(Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, dampingTime),xMin,xMax),
			                                 Mathf.Clamp(Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, dampingTime),actualMin,100),
			                                 transform.position.z);
		}
	}

//	void OnDrawGizmos(){
//		Gizmos.DrawWireSphere(new Vector3(transform.position.x, actualMin),1);
//		Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y),1);
//		Gizmos.DrawWireSphere(new Vector3(transform.position.x, groundLevel),1);
//		Gizmos.DrawWireSphere(new Vector3(transform.position.x, minY1),1);
//		Gizmos.DrawWireSphere(new Vector3(transform.position.x, minY2),1);
//	}
}
