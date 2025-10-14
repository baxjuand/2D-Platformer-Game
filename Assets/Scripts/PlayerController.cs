using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator animator;

    private Vector3 originalScale;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision: " + other.gameObject.name);
    }

    void Start()
    {
        originalScale = transform.localScale;  
    }

    void Update()
    {
        speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));

        if (speed < 0) 
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); 
        }
        else if (speed > 0) 
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); 
        }
            
    }
}
