using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] 
	private float movementSmoothing = 0.05f;	// How much to smooth out the movement
	private Rigidbody2D rigid; // For physics calculation
	private bool facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero; // Player's velocity
	const float moveMult = 10f; // Multiply movement value by this constant
	private Crosshair crosshair;
	private BoxCollider2D col;
	// /private RaycastHit hit;
	[SerializeField]
	LayerMask mask;
	const float magical_speed_control = 100f/71f;
	const float magical_size_mult = 5f/11f;
	const float magical_size_mult_down = 8f/11f;
	private SpriteRenderer spriteRend;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
		crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
		spriteRend = this.GetComponentInChildren<SpriteRenderer>();
	}

	public void Move(float moveX, float moveY)
	{
		// While near the wall there is a speed reduction while moving against the wall
		// It may not be the best way to fix it, but it works, so it is good enough for now
		// Multiplying by this constant (around 1/0.7) makes the player faster, so after speed reduction
		// the value is the same as it should be normally

		if(Physics2D.Raycast(transform.position, Vector2.left, spriteRend.size.x*magical_size_mult, mask) && moveX < 0 ||
		   Physics2D.Raycast(transform.position, Vector2.right, spriteRend.size.x*magical_size_mult, mask) && moveX > 0) {
			moveY *= magical_speed_control;
		}

		if(Physics2D.Raycast(transform.position, Vector2.up, spriteRend.size.x*magical_size_mult, mask) && moveY > 0 ||
		   Physics2D.Raycast(transform.position, Vector2.down, spriteRend.size.x*magical_size_mult_down, mask) && moveY < 0) {
			moveX *= magical_speed_control;
		}

		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveX * moveMult, moveY * moveMult);
		// And then smoothing it out and applying it to the character
		rigid.velocity = Vector3.SmoothDamp(rigid.velocity, targetVelocity, ref velocity, movementSmoothing);

		// New flip implementation - flip the player if the crosshair is behind his back
		if ((crosshair.position.x > transform.position.x && !facingRight) || (crosshair.position.x < transform.position.x && facingRight)) {
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