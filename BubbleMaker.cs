using UnityEngine;

// This script MUST be placed on the same GameObject that has the Bubble's MeshRenderer
[RequireComponent(typeof(Renderer))]
public class BubbleMaker : MonoBehaviour
{
    [Header("Highlight Settings")]
    public Color bubbleHighlightColor = Color.white;
    [Range(1f, 10f)] public float bubbleHighlightIntensity = 3f;

    [Header("Shader Property Names")]
    [SerializeField] private string colorPropertyName = "_Color";
    [SerializeField] private string emissionPropertyName = "Fernsel";

    // --- Runtime Variables ---
    private Renderer bubbleRenderer;      // This is now private
    private Material bubbleMatInstance;   // Our unique material instance
    private Color bubbleOriginalColor;
    private Color bubbleOriginalEmission;
    private bool didInit = false;

    void Awake()
       {
       
        // We get the Renderer component that is ON THIS GameObject.
   
        bubbleRenderer = GetComponent<Renderer>();
        if (bubbleRenderer == null)
        {
            Debug.LogError("BubbleMaker: No Renderer found on this object!", this);
            return;
        }

        // Read from sharedMaterial (safe)
        Material sharedMat = bubbleRenderer.sharedMaterial;
        if (sharedMat == null)
        {
            Debug.LogError("BubbleMaker: Renderer has no shared material!", this);
            return;
        }

        // Store original colors
        if (sharedMat.HasProperty(colorPropertyName))
        {
            bubbleOriginalColor = sharedMat.GetColor(colorPropertyName);
        }
        if (sharedMat.HasProperty(emissionPropertyName))
        {
            bubbleOriginalEmission = sharedMat.GetColor(emissionPropertyName);
        }

        didInit = true;
    }

    /// <summary>
    /// Gets the unique material instance *on demand* (at runtime).
    /// </summary>
    private Material GetMaterialInstance()
    {
        // If we don't have an instance yet, get one
        if (bubbleMatInstance == null)
        {
            // This is now 100% safe because 'bubbleRenderer' is guaranteed
            // to be the component from *this* scene instance, not a prefab.
            bubbleMatInstance = bubbleRenderer.material;
        }
        return bubbleMatInstance;
    }


    /// <summary>
    /// Called by the pickup script when the Interactor looks at it.
    /// </summary>
    public void Highlight()
    {
        if (!didInit) return;

        // Get the unique instance
        Material mat = GetMaterialInstance();
        if (mat == null) return;

        Color highlightHDRColor = bubbleHighlightColor * bubbleHighlightIntensity;

        if (mat.HasProperty(colorPropertyName))
        {
            mat.SetColor(colorPropertyName, highlightHDRColor);
        }
        if (mat.HasProperty(emissionPropertyName))
        {
            mat.SetColor(emissionPropertyName, highlightHDRColor);
            mat.EnableKeyword("_EMISSION");
        }
    }

    /// <summary>
    /// Called by the pickup script when the Interactor looks away.
    /// </summary>
    public void Unhighlight()
    {
        if (!didInit) return;

        // Get the unique instance
        Material mat = GetMaterialInstance();
        if (mat == null) return;

        if (mat.HasProperty(colorPropertyName))
        {
            mat.SetColor(colorPropertyName, bubbleOriginalColor);
        }
        if (mat.HasProperty(emissionPropertyName))
        {
            mat.SetColor(emissionPropertyName, bubbleOriginalEmission);
        }
    }
}