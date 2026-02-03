using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed of the cloud movement (Wind speed).")]
    [SerializeField] private float speed = 1f;

    [Header("Float Settings")]
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float phase = 1f;

    private float _startY;
    private float _startX;
    private float _length;

    void Start()
    {
        _startY = transform.position.y;
        _startX = transform.position.x;
        _length = CalculateLength();
    }

    private float CalculateLength()
    {
        // Calculate the total width of the object by encapsulating the bounds of all child renderers
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        
        if (renderers.Length > 0)
        {
            Bounds b = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                b.Encapsulate(renderers[i].bounds);
            }
            return b.size.x;
        }
        
        // Fallback if no renderers are found (though unlikely for a cloud object)
        return 0f; 
    }

    void Update()
    {
        if (_length > 0)
        {
            MoveClouds();
            CheckBounds();
        }
    }

    private void MoveClouds()
    {
        // Move Horizontally based on speed
        float currentX = transform.position.x;
        float newX = currentX + speed * Time.deltaTime;

        // Move Vertically (Float effect relative to initial Y)
        float newY = _startY + Mathf.Sin(Time.time * phase) * amplitude;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void CheckBounds()
    {
        // Check if the cloud has moved past its length relative to its start position
        // This ensures the loop happens exactly when the tile has fully traversed its own width
        
        float distTraveled = transform.position.x - _startX;

        if (distTraveled > _length)
        {
            // Shift back by length to loop seamlessly
            // We shift the transform but keep the relative offset to avoid visual popping
             transform.position -= Vector3.right * _length;
        }
        else if (distTraveled < -_length)
        {
             transform.position += Vector3.right * _length;
        }
    }
}
