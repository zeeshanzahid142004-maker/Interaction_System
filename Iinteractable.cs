using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;

public interface Iinteractable
{
    public UnityEvent unityEvent{ get; protected set;}
    public string interactionPrompt { get; }
    public Sprite interactionSprite{ get; }
    public bool Interact(Interactor interactor);

}
