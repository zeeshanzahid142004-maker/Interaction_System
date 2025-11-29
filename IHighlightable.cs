public interface IHighlightable
{
    /// <summary>
    /// Called when the Interactor starts looking at this object.
    /// </summary>
    public void OnHighlight();

    /// <summary>
    /// Called when the Interactor stops looking at this object.
    /// </summary>
    public void OnUnhighlight();
}