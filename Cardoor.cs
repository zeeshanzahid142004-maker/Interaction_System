using UnityEngine;
using UnityEngine.Events;


public class Cardoor : MonoBehaviour,Iinteractable
{

    [SerializeField] private string prompt;
    [SerializeField] private Sprite sprite;
    public string interactionPrompt => prompt;
    [SerializeField] private UnityEvent _onInteract;
    public Sprite interactionSprite => sprite;


   
    public bool Interact(Interactor interactor)
    {
        Debug.Log("cardooropened");

        //


        //

        _onInteract?.Invoke();
        return true;
    }
}
