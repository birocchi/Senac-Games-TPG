using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	private float shotSpeed;
	private Vector2 direction;
	private float destroyTime;

	void Start(){
		AudioSource.PlayClipAtPoint(audio.clip,transform.position);
		StartCoroutine(TimedDestroy(destroyTime));
	}

	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2D.velocity = direction.normalized * shotSpeed;
	}

	public void Initialize(float _shotSpeed, Vector2 _direction, float _destroyTime){
		shotSpeed = _shotSpeed;
		direction = _direction;
		destroyTime = _destroyTime;
	}

	IEnumerator TimedDestroy(float seconds){
		yield return new WaitForSeconds(seconds);
		Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag.Equals("Enemy") && this.gameObject.tag.Equals("PlayerShot")){
			other.gameObject.GetComponent<EnemyController>().Die();
		}

		if(other.gameObject.tag.Equals("Boss") && this.gameObject.tag.Equals("PlayerShot")){
			other.rigidbody.velocity = (-other.contacts[0].normal + Vector2.up) * 3;
			other.gameObject.GetComponent<BossController>().HurtBoss(1);
		}

		if(other.gameObject.tag.Equals("EnemyShot") && this.gameObject.tag.Equals("PlayerShot") ||
		   other.gameObject.tag.Equals("PlayerShot") && this.gameObject.tag.Equals("EnemyShot")){
			Destroy(other.gameObject);
		}

		if(other.gameObject.tag.Equals("Player") && this.gameObject.tag.Equals("EnemyShot")){
			other.rigidbody.velocity = (-other.contacts[0].normal + Vector2.up) * 3;
			other.gameObject.GetComponent<PlayerController_Fase2>().HurtPlayer(1);
		}

		Destroy(gameObject);

	}
}
