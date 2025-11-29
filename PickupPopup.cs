using UnityEngine;
using TMPro;

public class PickupPopup : MonoBehaviour
{
    [Header("Popup Motion")]
    public float floatSpeed = 0.5f;
    public float fadeSpeed = 7f;
    public float popupOffsetY = 1.2f;
    public float followDuration = 0.5f;
    public float popScale = 1.3f;         // how big the initial "pop" gets
    public float popSpeed = 6f;           // how fast it scales up/down

    private TextMeshPro tmp;
    private Color curColor;
    private Transform followTarget;
    private float followTimer;
    private Vector3 baseScale;
    private bool poppingIn = true;
    private float popTimer = 0f;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        if (tmp == null)
        {
            Debug.LogError("PickupPopup requires a TextMeshPro component.");
            enabled = false;
            return;
        }
        curColor = tmp.color;
        baseScale = transform.localScale;
    }

    public void Initialize(Transform target, string text, Color color)
    {
        followTarget = target;
        tmp.text = text;
        curColor = color;
        tmp.color = curColor;
        followTimer = followDuration;

        if (followTarget != null)
            transform.position = followTarget.position + Vector3.up * popupOffsetY;

        // Start small for pop effect
        transform.localScale = Vector3.zero;
        poppingIn = true;
        popTimer = 0f;
    }

    void Update()
    {
        // Follow target smoothly
        if (followTarget != null && followTimer > 0f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                followTarget.position + Vector3.up * popupOffsetY,
                Time.deltaTime * 8f);
            followTimer -= Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime, Space.World);
        }

        // Pop scale animation
        if (poppingIn)
        {
            popTimer += Time.deltaTime * popSpeed;
            float t = Mathf.SmoothStep(0, 1, popTimer);
            transform.localScale = Vector3.Lerp(Vector3.zero, baseScale * popScale, t);

            if (t >= 1f)
                poppingIn = false;
        }
        else
        {
            // Shrink back gently to base size
            transform.localScale = Vector3.Lerp(transform.localScale, baseScale, Time.deltaTime * 3f);
        }

        // Fade out
        curColor.a -= fadeSpeed * Time.deltaTime;
        tmp.color = curColor;

        if (curColor.a <= 0.05f)
            Destroy(gameObject);

        // Always face camera
        if (Camera.main != null)
        {
            Vector3 dir = Camera.main.transform.position - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(-dir);
        }
    }
}
