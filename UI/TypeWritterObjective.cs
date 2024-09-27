using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeWriterObjective : MonoBehaviour
{

	public float delay = 0.1f;
	public string fullText;
	private string currentText = "";

	// Use this for initialization
	void Start()
	{
		StartCoroutine(ShowText());
	}

	IEnumerator ShowText()
	{
		yield return new WaitForSeconds(20f);
		for (int i = 0; i < fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			GetComponent<TMP_Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}
}
