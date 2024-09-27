using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class normalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
public bool normalButtonPressed = false;
public bool normalButtonEnabled = false;
public playerMovements PlayerMovements;

public void Start()
{
    GetComponent<Button>().interactable = false;
}

public void Update()
{
    if (normalButtonEnabled == true)
    {
        GetComponent<Button>().interactable = true;
    }

    if (normalButtonEnabled == false)
    {
        GetComponent<Button>().interactable = false;
    }
}
 
public void OnPointerDown(PointerEventData eventData){
     normalButtonPressed = true;
     PlayerMovements.CanMove = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    normalButtonPressed = false;
}
}