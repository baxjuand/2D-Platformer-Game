using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    Animator animator;
    [SerializeField] Vector2 crouchOffsetCollider;
    [SerializeField] Vector2 crouchSizeCollider;
    
    [SerializeField] Vector2 pushOffsetCollider;
    [SerializeField] Vector2 pushSizeCollider;
    
    BoxCollider2D playerBoxCollider;

    private Vector3 originalScale;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

   

    void Start()
    {
        //Get Components
        playerBoxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        originalScale = transform.localScale;
        originalColliderSize = playerBoxCollider.size;
        originalColliderOffset = playerBoxCollider.offset;

        //UpdateColliderSize();


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

    public void OnCrouch(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            animator.SetBool("isCrouching", true);
            UpdateColliderSize(crouchSizeCollider, crouchOffsetCollider);
        }
        else if (context.canceled)
        {
            animator.SetBool("isCrouching", false);
            UpdateColliderSize(originalColliderSize, originalColliderOffset);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("isJumping");
        }
    }
    

    public void UpdateColliderSize(Vector2 colliderSizeModif, Vector2 colliderOffsetModif)
    {
        playerBoxCollider.size = new Vector2(colliderSizeModif.x, colliderSizeModif.y);
        playerBoxCollider.offset = new Vector2(colliderOffsetModif.x, colliderOffsetModif.y);
    }
}
