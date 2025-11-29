using UnityEngine;
using UnityEngine.Events;


public class Cardoor : MonoBehaviour,Iinteractable
{

    [SerializeField] private string prompt;
    [SerializeField] private Sprite sprite;
    public string interactionPrompt => prompt;
    [SerializeField]private UnityEvent _onInteract;
    public Sprite interactionSprite => sprite;

UnityEvent Iinteractable.unityEvent

    { get => _onInteract; set => _onInteract=value; }


   
    public bool Interact(Interactor interactor)
    {
        Debug.Log("cardooropened");

        //


        //

        _onInteract.Invoke();
        return true;
    }
}
