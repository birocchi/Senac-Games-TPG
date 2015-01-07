
using UnityEngine;
using System.Collections;

public class ClothTimeWorkaround : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale < 0.1f ) 
		{ 
			if(this.GetComponent<InteractiveCloth>().enabled) 
			{ 
				this.GetComponent<InteractiveCloth>().enabled = false; 
			} 
		}else 
		{ 
			if(!this.GetComponent<InteractiveCloth>().enabled) 
			{ 
				this.GetComponent<InteractiveCloth>().enabled = true; 
			} 
		} 
	}
}
