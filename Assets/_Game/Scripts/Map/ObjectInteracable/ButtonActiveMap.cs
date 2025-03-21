using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActiveMap : MonoBehaviour ,IInteracable
{
    public string message;
    public string InteractMessage => message;
    public bool canInteract => !isActived;


    public bool isActived = false;

    public static event Action OnButtonPressed;

    public void Interact(Interactor interactor)
    {
        Debug.Log("Button is pressed");
        if(!isActived)
        {
            isActived = true;
            OnButtonPressed?.Invoke();
        }

    }
}