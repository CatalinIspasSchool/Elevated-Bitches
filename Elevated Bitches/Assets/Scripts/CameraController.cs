using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField] float sensitivityX = 5f;
	[SerializeField] float sensitivityY = 5f;
	float yRotation;
	float xRotation;

	[SerializeField] Transform player;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    // Update is called once per frame
    void Update()
    {

		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

		yRotation += mouseX;
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		player.rotation = Quaternion.Euler(0, yRotation, 0);
	}
}
