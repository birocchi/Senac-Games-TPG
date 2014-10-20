using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	
		public static bool follow = true;
		public float tempoMovimento = 0.005f;
		private Vector3 veloc = Vector3.zero;
		public Transform target;

		void Start ()
		{
				follow = true;
		}
	
		void Update ()
		{
				if (target && follow) {
						Vector3 pontoJogador = camera.WorldToViewportPoint (target.position);
						Vector3 deltaJogadorCamera = target.position - camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.3f, pontoJogador.z));
						Vector3 destinoCamera = transform.position + deltaJogadorCamera;
						transform.position = Vector3.SmoothDamp (transform.position, destinoCamera, ref veloc, tempoMovimento);
				}
		}
}
