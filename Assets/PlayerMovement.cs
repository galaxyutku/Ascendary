using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public GameObject trajectoryDotPrefab;
    public int numberOfDots = 10;
    public float dotSpacing = 0.1f;

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private float dragStartTime;
    private float dragEndTime;
    public float slingForce = 10f;
    public float maxStretch = 5f;

    private List<GameObject> trajectoryDots;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        trajectoryDots = new List<GameObject>();

        // Instantiate trajectory dots
        for (int i = 0; i < numberOfDots; i++)
        {
            GameObject dot = Instantiate(trajectoryDotPrefab, transform.position, Quaternion.identity);
            dot.SetActive(false);
            trajectoryDots.Add(dot);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDragStart();
        }

        if (Input.GetMouseButton(0))
        {
            OnDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
    }

    void OnDragStart()
    {
        dragStartPosition = Input.mousePosition;
        dragStartTime = Time.time;
    }

    void OnDrag()
    {
        DisplayTrajectory();
    }

    void OnDragEnd()
    {
        dragEndPosition = Input.mousePosition;
        dragEndTime = Time.time;

        float dragDuration = dragEndTime - dragStartTime;
        Vector2 slingDirection = (dragStartPosition - dragEndPosition).normalized;
        float slingSpeed = Mathf.Clamp(Vector2.Distance(dragStartPosition, dragEndPosition) / dragDuration, 0f, 1f);

        // Apply sling force
        ApplySlingForce(slingDirection, slingSpeed);

        // Deactivate trajectory dots
        ActivateTrajectoryDots(false);
    }

    void ApplySlingForce(Vector2 direction, float speed)
    {
        Vector2 force = direction * slingForce * speed;
        body.AddForce(force, ForceMode2D.Impulse);
    }

    void DisplayTrajectory()
    {
        Vector2 currentPosition = transform.position;
        Vector2 currentVelocity = (dragStartPosition - (Vector2)Input.mousePosition).normalized * slingForce;

        for (int i = 0; i < numberOfDots; i++)
        {
            float time = i * dotSpacing;
            Vector2 newPos = CalculateTrajectoryPoint(currentPosition, currentVelocity, time);
            trajectoryDots[i].transform.position = newPos;
            trajectoryDots[i].SetActive(true);
        }
    }

    Vector2 CalculateTrajectoryPoint(Vector2 start, Vector2 startVelocity, float time)
    {
        return start + startVelocity * time + 0.5f * Physics2D.gravity * time * time;
    }

    void ActivateTrajectoryDots(bool activate)
    {
        foreach (var dot in trajectoryDots)
        {
            dot.SetActive(activate);
        }
    }

}
