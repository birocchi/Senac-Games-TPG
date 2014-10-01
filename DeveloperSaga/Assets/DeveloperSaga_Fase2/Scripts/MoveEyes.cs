using UnityEngine;
using System.Collections;

public class MoveEyes : MonoBehaviour {

	const float EYE_BODY_HEIGHT = 0.05f;
	const float EYE_LIMIT = 0.05f;

	Transform eyes;
	Transform pupils;

	float horizontal;
	float vertical;

	void Awake () {
		//Set up the references to the eyes parts
		eyes = transform.FindChild("BoxyEyes");
		pupils = eyes.FindChild("BoxyPupils");
	}

	void Update () {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		eyes.localPosition =	new Vector2(horizontal * EYE_LIMIT, vertical * EYE_LIMIT + EYE_BODY_HEIGHT);
		pupils.localPosition =	new Vector2(horizontal * EYE_LIMIT, vertical * EYE_LIMIT);
	}
}
