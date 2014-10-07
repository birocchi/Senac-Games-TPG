using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {

	public GameObject shot;
	public int fireRate = 1;
	public float shotSpeed = 7;

	private Vector2 shotDirection;
	private float fireInterval;
	private float elapsedTime;
	private GameObject shotInstance;
	
	void Start () {
		fireInterval = 1f / fireRate;
		elapsedTime = fireInterval;
	}

	void Update () {
		if(Input.GetButtonDown("Fire1") && elapsedTime >= fireInterval){
			shotInstance = (GameObject)Instantiate(shot, transform.position, transform.rotation);
			shotInstance.GetComponent<ShotController>().Initialize(shotSpeed, transform.rotation * (Vector3)Vector2.right );
			Physics2D.IgnoreCollision(this.collider2D, shotInstance.collider2D);
			elapsedTime = 0;
		}

		if(elapsedTime < fireInterval){
			elapsedTime += Time.fixedDeltaTime;
		}
	}
	
}
