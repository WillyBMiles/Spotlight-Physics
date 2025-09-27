using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraControls : MonoBehaviour
{
    [SerializeField]
    float distance;
    [SerializeField]
    GameObject cameraParent;

    [SerializeField]
    float moveSensitivity;
    [SerializeField]
    GameObject cam;

    Vector3 Angles;
    public float sensitivityX;
    public float sensitivityY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    float xAngle;
    float yAngle;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        Vector2 move = moveSensitivity * Time.deltaTime * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        xAngle = Mathf.Clamp(xAngle + move.y, -90, 90);
        yAngle -= move.x;

        cameraParent.transform.SetPositionAndRotation(-cameraParent.transform.forward * distance, Quaternion.Euler(xAngle, yAngle, 0f));


        float rotationY = Input.GetAxis("Mouse Y") * sensitivityX;
        float rotationX = Input.GetAxis("Mouse X") * sensitivityY;
        if (rotationY > 0)
            Angles = new Vector3(Mathf.MoveTowards(Angles.x, -90, rotationY), Angles.y + rotationX, 0);
        else
            Angles = new Vector3(Mathf.MoveTowards(Angles.x, 90, -rotationY), Angles.y + rotationX, 0);
        cam.transform.localEulerAngles = Angles;

        
    }


    private void LateUpdate()
    {
        cam.transform.position = cameraParent.transform.position;
    }
}
