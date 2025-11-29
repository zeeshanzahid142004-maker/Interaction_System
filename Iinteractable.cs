using UnityEngine;

public interface Iinteractable
{
    string interactionPrompt { get; }
    Sprite interactionSprite { get; }
    bool Interact(Interactor interactor);
}
