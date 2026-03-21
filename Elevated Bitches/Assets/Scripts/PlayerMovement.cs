using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movespeed = 5f;
	Vector3 moveDirection;



	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

		//movement
		moveDirection += transform.forward * Input.GetAxis("Vertical");
		moveDirection += transform.right * Input.GetAxis("Horizontal");

		rb.MovePosition(transform.position + moveDirection * Time.deltaTime * movespeed);
        moveDirection = Vector3.zero;
	}

    //public void Move(InputAction.CallbackContext context)
    //{
    //    var v = context.ReadValue<Vector2>();
    //    moveDirection = new Vector3(v.x,0,v.y);
    //    Debug.Log(v);
    //}
}
