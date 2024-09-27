using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class hardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
public bool hardButtonPressed = false;
public bool hardButtonEnabled = false;
public playerMovements PlayerMovements;

public void Start()
{
    GetComponent<Button>().interactable = false;
}

public void Update()
{
    if (hardButtonEnabled == true)
    {
        GetComponent<Button>().interactable = true;
    }

    if (hardButtonEnabled == false)
    {
        GetComponent<Button>().interactable = false;
    }
}

public void OnPointerDown(PointerEventData eventData){
     hardButtonPressed = true;
     PlayerMovements.CanMove = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    hardButtonPressed = false;
}
}