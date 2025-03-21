using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject pickUpButton;
    public GameObject unlockObjectOnPlayer;
    private void Start()
    {
        pickUpButton.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUpButton.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
                unlockObjectOnPlayer.SetActive(true);
                pickUpButton.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUpButton.SetActive(false);
        }
    }
}
