using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class sceneFader : MonoBehaviour
{

	#region FIELDS
	public RawImage fadeOutUIImage;
	public TMP_Text fadeOutText;
	public float fadeSpeed = 0.8f;
	public bool fadeIn = true;
	public GameObject music;
	public bool faded = false;
	public bool text = false;
	public bool needsKeyPressed = false;


	public void Start()
	{
		Time.timeScale = 1f;
		if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
	}
	
	public enum FadeDirection
	{
		In, //Alpha = 1
		Out // Alpha = 0
	}
	#endregion

	void Update()
	{
		if (needsKeyPressed == true)
		{
			if (Input.anyKey && !faded)
        	{
            	faded = true;
				music.SetActive(true);
				if (fadeIn == true)
				{
					StartCoroutine(Fade(FadeDirection.In));
				}
				else if (fadeIn == false)
				{
					StartCoroutine(Fade(FadeDirection.Out));
				}
        	}
		}
		
		if (needsKeyPressed == false)
		{
			if (faded == false)
			{
				faded = true;
				music.SetActive(true);
				if (fadeIn == true)
				{
					StartCoroutine(Fade(FadeDirection.In));
				}
				else if (fadeIn == false)
				{
					StartCoroutine(Fade(FadeDirection.Out));
				}
			}
        }
	}

	#region FADE
	private IEnumerator Fade(FadeDirection fadeDirection)
	{
		float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
		float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
		if (fadeDirection == FadeDirection.Out)
		{
			while (alpha >= fadeEndValue)
			{
				SetColorImage(ref alpha, fadeDirection);
				yield return null;
			}
			StartCoroutine(fadeWait());
		}
		else
		{
			fadeOutText.enabled = true;
			fadeOutUIImage.enabled = true;
			while (alpha <= fadeEndValue)
			{
				SetColorImage(ref alpha, fadeDirection);
				yield return null;
			}
			StartCoroutine(fadeWait());
		}
	}
	#endregion

	#region HELPERS
	public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
	{
		yield return Fade(fadeDirection);
		SceneManager.LoadScene(sceneToLoad);
	}

	private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
	{
		if(text == false)
		{
			fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
		}
		else if(text == true)
		{
			fadeOutText.color = new Color(fadeOutText.color.r, fadeOutText.color.g, fadeOutText.color.b, alpha);
		}
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
	}
	#endregion

	IEnumerator fadeWait()
	{
		yield return new WaitForSeconds(2f);
		fadeOutUIImage.enabled = false;
		fadeOutText.enabled = false;
	}
}