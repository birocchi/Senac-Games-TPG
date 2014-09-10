using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartController : MonoBehaviour {

	public int heartNumber;
	
	public Sprite fullHeartSprite;
	public Sprite halfHeartSprite;
	public Sprite emptyHeartSprite;

	private Sprite actualSprite;
	private int fullHeart;
	private int halfHeart;
	private LifeManager lifeManager;
	private Image image;

	// Use this for initialization
	void Start () {
		fullHeart = heartNumber * 2;
		halfHeart = fullHeart - 1;
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
		image = GetComponent<Image>();
		actualSprite = fullHeartSprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(lifeManager.ActualLife == fullHeart && !actualSprite.Equals(fullHeartSprite)){
			image.sprite = fullHeartSprite;
		} else if(lifeManager.ActualLife == halfHeart && !actualSprite.Equals(halfHeartSprite)){
			image.sprite = halfHeartSprite;
		} else if(lifeManager.ActualLife < halfHeart && !actualSprite.Equals(emptyHeartSprite)){
			image.sprite = emptyHeartSprite;
		}
	}
}
