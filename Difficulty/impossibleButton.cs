using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class impossibleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
public bool impossibleButtonPressed = false;
public bool impossibleButtonEnabled = false;
public playerMovements PlayerMovements;

public void Start()
{
    GetComponent<Button>().interactable = false;
}

public void Update()
{
    if (impossibleButtonEnabled == true)
    {
        GetComponent<Button>().interactable = true;
    }

    if (impossibleButtonEnabled == false)
    {
        GetComponent<Button>().interactable = false;
    }
}
 
public void OnPointerDown(PointerEventData eventData){
     impossibleButtonPressed = true;
     PlayerMovements.CanMove = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    impossibleButtonPressed = false;
}
}