using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {

	public GameObject shot;
	public float fireRate = 1;
	public float shotSpeed = 7;
	public float shotLifeTime = 0.6f;
	public float armUpTime = 1.2f;

	[HideInInspector]
	public bool isShooting;
	
	private float fireInterval;
	private float elapsedTime;
	private float armUpCounter;
	private GameObject shotInstance;
	
	void Start () {
		fireInterval = 1f / fireRate;
		elapsedTime = fireInterval;
	}

	void Update () {
		if(Input.GetButtonDown("Fire1") && elapsedTime >= fireInterval){
			isShooting = true;
			shotInstance = (GameObject)Instantiate(shot, transform.FindChild("ShotSpawn").position, transform.rotation);
			shotInstance.GetComponent<ShotController>().Initialize(shotSpeed, transform.rotation * (Vector3)Vector2.right, shotLifeTime );
			Physics2D.IgnoreCollision(this.collider2D, shotInstance.collider2D);
			elapsedTime = 0;
			armUpCounter = armUpTime;
		}

		if(elapsedTime < fireInterval){
			elapsedTime += Time.deltaTime;
		}

		if(armUpCounter <= 0){
			isShooting = false;
		}else{
			armUpCounter -= Time.deltaTime;
		}
	}


	
}
