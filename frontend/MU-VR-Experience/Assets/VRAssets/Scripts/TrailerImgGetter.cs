using UnityEngine;
using System;
using System.Collections;

public class TrailerImgGetter : MonoBehaviour
{
	public void Base64ToSprite(string baseString)
	{
		SpriteRenderer trailerImg = gameObject.transform.Find("TrailerImg").GetComponent<SpriteRenderer>();
		byte[] imageBytes = Convert.FromBase64String(baseString);
		Texture2D texture = new Texture2D(2, 2);
		texture.LoadImage(imageBytes);
		trailerImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, 350, 530), new Vector2(0.5f, 0.5f), 100.0f);
		// Images from backend must come at 350x530 resolution aproximately
	}
}
