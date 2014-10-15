using UnityEngine;
using System.Collections;

public class SpeedOnStart : MonoBehaviour {
	public Vector2 speed;

	void Start () {
		this.rigidbody2D.velocity = speed;
	}
}
