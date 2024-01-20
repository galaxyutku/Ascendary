using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;
	private bool isStuck = false;
	[SerializeField] public CinemachineVirtualCamera vCam;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	#endregion

	Camera cam;

	public Ball ball;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;
	public Animator animator;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;

	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	//---------------------------------------
	void Start()
	{
		cam = Camera.main;
		ball.DesactivateRb();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			OnDragStart();
		}
		if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			OnDragEnd();
		}

		if (isDragging)
		{
			OnDrag();
		}

		if (rb.velocity == Vector2.zero)
        {
			animator.SetBool("isJumping", false);
		}
		else
        {
			if (rb.velocity.x < 0)
			{
				// Moving to the left
				spriteRenderer.flipX = true;
			}
			else if(rb.velocity.x > 0)
			{
				spriteRenderer.flipX = false;
			}
			animator.SetBool("isJumping", true);
		}

	}

	//-Drag--------------------------------------
	void OnDragStart()
	{
		ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		trajectory.Show();
	}

	void OnDrag()
	{
		endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		distance = Vector2.Distance(startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		Debug.DrawLine(startPoint, endPoint);


		trajectory.UpdateDots(ball.pos, force);
	}

	void OnDragEnd()
	{
		//push the ball
		ball.ActivateRb();

		ball.Push(force);

		trajectory.Hide();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "GreenPad" && !isStuck)
		{
			// Stop the ball's movement
			isStuck = true;
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		if(collision.gameObject.tag == "Room1" || collision.gameObject.tag == "Room2")
        {
			vCam.Follow = collision.transform;
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "GreenPad" && isStuck)
		{
			// Stop the ball's movement
			isStuck = false;
		}
	}
}
