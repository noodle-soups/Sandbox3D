using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    Rigidbody rb;
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputForward = Input.GetAxis("Vertical");
        var moveVector = new Vector3(inputHorizontal, 0, inputForward);

        if (moveVector.sqrMagnitude > 1)
        {
            moveVector.Normalize();
        }

        var movePlayer = moveVector * moveSpeed * Time.deltaTime;

        transform.Translate(movePlayer);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("Hit Ground");
        }
    }
}
