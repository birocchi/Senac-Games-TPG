using UnityEngine;
using System.Collections;

public class Boss3Controller : MonoBehaviour
{
	
		static float bossEnergy = 100.0f;

		public Animator animator;
		public GameObject[] handTargets;
		public GameObject bullet;
		private static string SHOTING_RIGHT_ANIMATION_NAME = "BossHandRightTwoFingerShotShot";
		private static string SHOTING_LEFT_ANIMATION_NAME = "BossHandLeftTwoFingerShotShot";
		private static string SLAP_RIGHT_ANIMATION_NAME = "BossHandRightSlapAlign";
		private static string SLAP_LEFT_ANIMATION_NAME = "BossHandLeftSlapAlign";
		public int lastBossMode = 0;
		public int bossMode = 0;
		private int modeRunning = 0; 
		public int shotsPerHand; 

		public GUIStyle energyBar;
		public Texture2D bgImage;
		public Texture2D fgImage;

		public ParticleSystem[] eyeParticles;
		public GameObject cabeca;
		private float cabecaX;

		private int numberOfShotsRight;	
		private int numberOfShotsLeft;	

		private bool bossEnabled = false;
		private float previousHealth;

		public ParticleSystem explosionParticle;

		// Use this for initialization
		void Start ()
		{
				previousHealth = bossEnergy;
		}
	
		// Update is called once per frame
		void Update ()
		{					
				if (bossEnabled) {
						this.ControlBossState ();

						if (previousHealth != bossEnergy) {				
								animator.SetInteger ("bossExpression", 2);
								previousHealth = bossEnergy;
						}


						if (bossEnergy <= 0) {		

								SongController.songToPlay = 0; 

								bossEnabled = false;
								ParticleSystem instance = (ParticleSystem)Instantiate (explosionParticle, transform.position, transform.rotation);
								instance.enableEmission = true;
								instance.Play ();
								
								Renderer[] rs = GetComponentsInChildren<Renderer> ();
								foreach (Renderer r in rs)
										r.enabled = false;

								Collider[] col = GetComponentsInChildren<Collider> ();
								foreach (Collider c in col)
										c.enabled = false;

								Light[] ls = GetComponentsInChildren<Light> ();
								foreach (Light l in ls)
										l.enabled = false;
								
								Destroy (instance, 3);
								Destroy (this.gameObject, 5);

								StartCoroutine (Exit ());

						}
						
				}
		}

		IEnumerator PlayBossSong (int secs)
		{		
				yield return new WaitForSeconds (secs);		
				if (SongController.songToPlay != 2) {
						SongController.songToPlay = 2;
				}
		}


		IEnumerator PrepareShotRight (float timeToWait)
		{		

				modeRunning++;
				
				
		
				animator.SetInteger ("rightHandAnimation", 0);
				
		
				yield return new WaitForSeconds (timeToWait + 1f);
				

				animator.SetInteger ("rightHandAnimation", -1);
				yield return new WaitForSeconds (3.5f);
				modeRunning--;
				
		}

		IEnumerator Exit ()
		{		
				yield return new WaitForSeconds (3);
				Application.LoadLevel ("cutscene4");
				
		}

		IEnumerator PrepareShotLeft (float timeToWait)
		{		
		
				modeRunning++;
		
		
		
				animator.SetInteger ("leftHandAnimation", 0);
		
		
				yield return new WaitForSeconds (timeToWait + 1f);
		
		
				animator.SetInteger ("leftHandAnimation", -1);
				yield return new WaitForSeconds (3.5f);
				modeRunning--;
		
		}

		IEnumerator Slap (string slapAnimationName, string handNameAnimation)
		{				
				if (bossEnabled) {
						modeRunning++;
						animator.SetInteger (handNameAnimation, 1);		
		
						yield return new WaitForSeconds (4f);		
		
						animator.SetInteger (handNameAnimation, -1);
						yield return new WaitForSeconds (3.5f);
						modeRunning--;
				}
		
		}

		IEnumerator EyeLaser (int command)
		{				
				if (bossEnabled) {
						modeRunning++;

						foreach (ParticleSystem particle in eyeParticles) {
								particle.enableEmission = true;		
								particle.Play ();
						}
					

						animator.SetInteger ("moveHead", command);
		
						yield return new WaitForSeconds (5f);		
		
						foreach (ParticleSystem particle in eyeParticles) {
								particle.enableEmission = false;		
								particle.Stop ();
						}

						animator.SetInteger ("moveHead", 0);
		
						modeRunning--;
				}
		}
	
		IEnumerator ControlStateChange ()
		{		
				animator.SetInteger ("bossLightMode", 1);
				animator.SetInteger ("bossExpression", 1);
				yield return new WaitForSeconds (2.5f);
				animator.SetInteger ("bossLightMode", 0);	 
				animator.SetInteger ("bossExpression", 0);
		}

		private void ControlBossState ()
		{
				if (modeRunning <= 0) {				
						StartCoroutine (ControlStateChange ());
						int handInUse = -1;
						int currentBossModeRightHand = -1;
						int currentBossModeLeftHand = -1;
						int eyeAttack = -1;
						int eyeAttackDir = 1;
						float timeToWait = 3f;
						Random random = new Random ();
						if (bossEnergy > 77f) {
								timeToWait = 5f;
								currentBossModeRightHand = Random.Range (1, 3);
								currentBossModeLeftHand = Random.Range (1, 3);
								eyeAttack = 0;
								handInUse = Random.Range (1, 3);
						} else if (bossEnergy > 33f) {
								timeToWait = 10f;
								currentBossModeRightHand = Random.Range (1, 3);
								currentBossModeLeftHand = Random.Range (1, 3);
								eyeAttack = Random.Range (1, 4);
								handInUse = Random.Range (1, 4);
						} else if (bossEnergy > 0f) {
								timeToWait = 15f;
								currentBossModeRightHand = Random.Range (1, 3);
								currentBossModeLeftHand = Random.Range (1, 3);
								eyeAttack = Random.Range (1, 6);
								handInUse = 3;
						}
						
						if (eyeAttack >= 3) {
								eyeAttackDir = Random.Range (1, 3);
								StartCoroutine (EyeLaser (eyeAttackDir));			
						} else {
								if (handInUse == 1 || handInUse == 3) {
										if (currentBossModeRightHand == 1) {
												StartCoroutine (PrepareShotRight (timeToWait));					
										} else {
												StartCoroutine (Slap (SLAP_RIGHT_ANIMATION_NAME, "rightHandAnimation"));	 				
										}
								}
								if (handInUse == 2 || handInUse == 3) {
										if (currentBossModeLeftHand == 1) {
												StartCoroutine (PrepareShotLeft (timeToWait));					
										} else {
												StartCoroutine (Slap (SLAP_LEFT_ANIMATION_NAME, "leftHandAnimation"));					
										}
								}
						}
				}
		}
	
		public void ShotRight ()
		{			
				if (bossEnabled) {
						for (int i = 1 * shotsPerHand - shotsPerHand; i < (1 * shotsPerHand); i++) {
								GameObject target = handTargets [i];
								GameObject bulletInstance = (GameObject)Instantiate (bullet, target.transform.position, target.transform.rotation);
								BulletController bc = bulletInstance.GetComponent<BulletController> ();
								bc.damage = 2; 
								bulletInstance.rigidbody.velocity = target.transform.up * 5; 
								Destroy (bulletInstance, 5f);						
						}
				}
		}

		public void ShotLeft ()
		{		
				if (bossEnabled) {
						for (int i = 2 * shotsPerHand - shotsPerHand; i < (2 * shotsPerHand); i++) {
								GameObject target = handTargets [i];
								GameObject bulletInstance = (GameObject)Instantiate (bullet, target.transform.position, target.transform.rotation);
								BulletController bc = bulletInstance.GetComponent<BulletController> ();
								bc.damage = 2; 
								bulletInstance.rigidbody.velocity = target.transform.up * 5; 
								Destroy (bulletInstance, 5f);						
						}
				}
		}
	
		public void OnGUI ()
		{
				if (bossEnergy > 0f && bossEnabled) {
						float barPosX = Screen.width - 300f;
						float barPosY = 50f;
		
						GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("label"));
						style.fontSize = 28;
						style.fontStyle = FontStyle.Bold;

						GUI.Label (new Rect (barPosX - 80, barPosY - 7, 100, 50), "Boss", style);		
				

						GUI.BeginGroup (new Rect (barPosX, barPosY, 256, 32));

						GUI.Box (new Rect (0, 0, 256, 32), bgImage, energyBar);
		
				
						GUI.BeginGroup (new Rect (0, 0, bossEnergy / 100 * 256, 32));
		
				
						GUI.Box (new Rect (0, 0, 256, 32), fgImage, energyBar);

		
						// End both Groups
						GUI.EndGroup ();
						GUI.EndGroup ();
				}
		}



		public void DoDamage (float damage)
		{
				bossEnergy -= damage;
		}
	 
		IEnumerator EnableBoss ()
		{	

				StartCoroutine (PlayBossSong (0));				
		
				yield return new WaitForSeconds (2f);				

				animator = this.GetComponent<Animator> ();
				animator.SetBool ("enableBoss", true);

				yield return new WaitForSeconds (5f);
				
				cabecaX = cabeca.transform.position.x;
				this.bossEnabled = true;
		
		}	
	
		public void StartBoss ()
		{
				StartCoroutine (EnableBoss ());
		}
}
