using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;  
    public float distance = 5f;                    
    public float speed = 2f;

    Rigidbody rb;
    Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * speed, 2f * distance) - distance;
        rb.MovePosition(startPos + moveDirection.normalized * t);
    }
}