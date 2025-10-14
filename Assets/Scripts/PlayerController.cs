using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator animator;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision: " + other.gameObject.name);
    }

    void Update()
    {
        Vector3 scale = transform.localScale;

        speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));
        if (speed < 0) { scale.x = -1f * Mathf.Abs(scale.x); }
        else if (speed > 0) { scale.x = Mathf.Abs(scale.x); }
            
    }
}
