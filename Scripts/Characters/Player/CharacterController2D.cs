using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] 
	private float movementSmoothing = .05f;	// How much to smooth out the movement
	private Rigidbody2D rigid; // For physics calculation
	private bool facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero; // Player's velocity
	const float moveMult = 10f; // Multiply movement value by this constant

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}


	public void Move(float moveX, float moveY)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveX * moveMult, moveY * moveMult);

		// And then smoothing it out and applying it to the character
		rigid.velocity = Vector3.SmoothDamp(rigid.velocity, targetVelocity, ref velocity, movementSmoothing);

		// If the input is moving the player right and the player is facing left flip the player
		// Analogously if the player is moving left
		if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight)) {
			Flip();
		}
	}


	private void Flip()
	{
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}