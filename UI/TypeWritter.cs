using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeWriter : MonoBehaviour
{
	public float delay = 0.1f;
	public float delayBetweenSentence = 2f;
	public string fullText;
	public string fullText2;
	public string fullText3;
	private string currentText = "";

	// Use this for initialization
	void Start()
	{
		StartCoroutine(ShowText());
	}

	IEnumerator ShowText()
	{
		yield return new WaitForSeconds(4f);
		for (int i = 0; i < fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
            GetComponent<TMP_Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
		yield return new WaitForSeconds(delayBetweenSentence);
		for (int i = 0; i < fullText2.Length; i++)
		{
			currentText = fullText2.Substring(0, i);
			GetComponent<TMP_Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
		yield return new WaitForSeconds(delayBetweenSentence);
		for (int i = 0; i < fullText3.Length; i++)
		{
			currentText = fullText3.Substring(0, i);
			GetComponent<TMP_Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
		yield return new WaitForSeconds(6f);
		gameObject.SetActive(false);
	}
}
