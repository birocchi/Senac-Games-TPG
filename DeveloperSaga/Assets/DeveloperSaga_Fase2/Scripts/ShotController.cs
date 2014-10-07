using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	private float shotSpeed;
	private Vector2 direction;

	void Start(){
		AudioSource.PlayClipAtPoint(audio.clip,transform.position);
		StartCoroutine(TimedDestroy(1));
	}

	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2D.velocity = direction.normalized * shotSpeed;
	}

	public void Initialize(float _shotSpeed, Vector2 _direction){
		shotSpeed = _shotSpeed;
		direction = _direction;
	}

	IEnumerator TimedDestroy(float seconds){
		yield return new WaitForSeconds(seconds);
		Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag.Equals("Enemy") && this.gameObject.tag.Equals("PlayerShot")){
			other.gameObject.GetComponent<EnemyController>().Die();
		}

		Destroy(gameObject);

	}
}
