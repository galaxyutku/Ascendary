using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private bool isStuck = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GreenPad" && !isStuck)
        {
            Debug.Log("Ball stuck");
            isStuck = true;

            // Disable gravity for the ball
            GetComponent<Rigidbody2D>().gravityScale = 0f;

            // Stop the ball's movement
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GreenPad")
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GreenPad")
        {
            Debug.Log("exit");
            GetComponent<Rigidbody2D>().gravityScale = 1;
            isStuck = false;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
