using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {

// Create public variables for player speed, jump force, and for the Text UI game objects
public float speed;
public float jumpForce;
public TextMeshProUGUI countText;
public GameObject winTextObject;

    private float movementX;
    private float movementY;
private Rigidbody rb;
private int count;
private bool isGrounded;
private bool canDoubleJump;

// At the start of the game..
void Start ()
{
	// Assign the Rigidbody component to our private rb variable
	rb = GetComponent<Rigidbody>();

	// Set the count to zero 
	count = 0;

	SetCountText ();

            // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
            winTextObject.SetActive(false);
}

void FixedUpdate ()
{
	// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
	Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

	rb.AddForce (movement * speed);

	if (isGrounded)
	{
		canDoubleJump = true;
	}
}

void OnTriggerEnter(Collider other) 
{
	// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
	if (other.gameObject.CompareTag ("PickUp"))
	{
		other.gameObject.SetActive (false);

		// Add one to the score variable 'count'
		count = count + 1;

		// Run the 'SetCountText()' function (see below)
		SetCountText ();
	}
}

void OnMove(InputValue value)
{
	Vector2 v = value.Get<Vector2>();

	movementX = v.x;
	movementY = v.y;
}

void OnJump(InputValue value)
{
	if (isGrounded)
	{
		rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		isGrounded = false;
	}
	else
	{
		if (canDoubleJump)
		{
			rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			canDoubleJump = false;
		}
	}
}

void OnCollisionEnter(Collision collision)
{
	if (collision.gameObject.CompareTag("Ground"))
	{
		isGrounded = true;
	}
}

    void SetCountText()
{
	countText.text = "Count: " + count.ToString();

	if (count >= 12) 
	{
		// Set the text value of your 'winText'
		winTextObject.SetActive(true);
	}
}
}