using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteracable
{
    private string message;
    public string InteractMessage => message;

    public bool canInteract => true;

    public void Interact(Interactor interactor)
    {
        if (interactor.currentKey == 0) 
        {
            interactor.currentKey = 1;
            message = "You pick up KEY";
            //pickup object
            Destroy(gameObject);
        }
        else
        {
            message = "Can't pick up, you can only carry one item at a time";
        }
    }
}
