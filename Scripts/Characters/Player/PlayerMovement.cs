using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    [SerializeField] 
    private float runSpeed = 25f;

    void Update()
    {
        // Check the direction of movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

        // Reduce movement speed if player moves diagonally
        if(Mathf.Abs(horizontalMove) > 0 && Mathf.Abs(verticalMove) > 0) {
            horizontalMove /= Mathf.Sqrt(2);
            verticalMove /= Mathf.Sqrt(2);
        }

        animator.SetFloat("speed",Mathf.Max(Mathf.Abs(horizontalMove), Mathf.Abs(verticalMove)));
    }

    void FixedUpdate() {
        // Apply movement to the player
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
    }
}
