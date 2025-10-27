using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] float horizonatl;
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] Vector2 crouchOffsetCollider;
    [SerializeField] Vector2 crouchSizeCollider;
    [SerializeField] Vector2 jumpOffsetCollider;
    [SerializeField] Vector2 jumpSizeCollider;
    [SerializeField] Vector2 pushOffsetCollider;
    [SerializeField] Vector2 pushSizeCollider;
    private Vector3 position;
    private float horizontalMovement;

    private BoxCollider2D playerBoxCollider;
    private Animator animator;
    private Rigidbody2D playerRigidBody;

    private bool isCrouching = false;
    private Vector3 originalScale;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;


    void Awake()
    {
         //Get Components
        playerBoxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        originalScale = transform.localScale;
        originalColliderSize = playerBoxCollider.size;
        originalColliderOffset = playerBoxCollider.offset;
    }

    void Update()
    {
        MovePlayer();    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        if (!isCrouching)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            horizontalMovement = moveInput.x;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        }
        

        if (horizontalMovement != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalMovement) * originalScale.x, originalScale.y, originalScale.z);
        }

        

    }

    private void MovePlayer()
    {
        position = transform.position;
        position.x += horizontalMovement * speed * Time.deltaTime;
        transform.position = position;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            isCrouching = true;
            animator.SetBool("isCrouching", true);
            UpdateColliderSize(crouchSizeCollider, crouchOffsetCollider);
        }
        else if (context.canceled)
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);
            UpdateColliderSize(originalColliderSize, originalColliderOffset);
            
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isCrouching)
        {
            animator.SetTrigger("isJumping");
            playerRigidBody.AddForceY(jump, ForceMode2D.Impulse);
        }
    }

    public void OnJumpStart()
    {
        UpdateColliderSize(jumpSizeCollider, jumpOffsetCollider);
    }
    
    public void OnJumpEnd()
    {
        UpdateColliderSize(originalColliderSize, originalColliderOffset);
    }
    

    public void UpdateColliderSize(Vector2 colliderSizeModif, Vector2 colliderOffsetModif)
    {
        playerBoxCollider.size = new Vector2(colliderSizeModif.x, colliderSizeModif.y);
        playerBoxCollider.offset = new Vector2(colliderOffsetModif.x, colliderOffsetModif.y);
    }
}
