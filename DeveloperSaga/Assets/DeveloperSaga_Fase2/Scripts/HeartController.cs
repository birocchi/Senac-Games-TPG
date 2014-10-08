using UnityEngine;
using System.Collections;

public class HeartController : MonoBehaviour {

	public int heartNumber;
	
	public Texture2D fullHeartSprite;
	public Texture2D halfHeartSprite;
	public Texture2D emptyHeartSprite;

	//private Texture2D actualSprite;
	private int fullHeart;
	private int halfHeart;
	private LifeManager lifeManager;
	private GUITexture image;

	// Use this for initialization
	void Start () {
		fullHeart = heartNumber * 2;
		halfHeart = fullHeart - 1;
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
		image = GetComponent<GUITexture>();
		image.texture = fullHeartSprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(lifeManager.ActualLife >= fullHeart && !image.texture.Equals(fullHeartSprite)){
			image.texture = fullHeartSprite;
		} else if(lifeManager.ActualLife == halfHeart && !image.texture.Equals(halfHeartSprite)){
			image.texture = halfHeartSprite;
		} else if(lifeManager.ActualLife < halfHeart && !image.texture.Equals(emptyHeartSprite)){
			image.texture = emptyHeartSprite;
		}
	}
}
