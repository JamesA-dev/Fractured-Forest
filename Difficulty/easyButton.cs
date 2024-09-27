using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class easyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

public playerMovements PlayerMovements;
public bool easyButtonPressed = false;
public bool easyButtonEnabled = false;

public void Start()
{
    PlayerMovements.CanMove = false;
    GetComponent<Button>().interactable = false;
}

public void Update()
{
    if (easyButtonEnabled == true)
    {
        GetComponent<Button>().interactable = true;
    }
    
    if (easyButtonEnabled == false)
    {
        GetComponent<Button>().interactable = false;
    }
}

public void OnPointerDown(PointerEventData eventData){
     easyButtonPressed = true;
     PlayerMovements.CanMove = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    easyButtonPressed = false;
}
}