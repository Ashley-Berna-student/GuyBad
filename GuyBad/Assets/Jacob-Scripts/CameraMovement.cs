
using UnityEngine;
using Unity.Netcode;

public class CameraMovement : NetworkBehaviour
{
    public static float rotatex;
    public static float rotatey;
    public static float rotatez = 0f;
    [SerializeField] public float sensX;
    [SerializeField] public float sensY;
    [SerializeField] public Transform orientation;
    private bool canRotate = true;
    public float rotateSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            if (canRotate)
            {
                rotatey += mouseX;

            rotatex += mouseY;
            rotatex = Mathf.Clamp(rotatex, -90f, 90f);

                transform.rotation = Quaternion.Euler(rotatex, rotatey, 0);
                orientation.rotation = Quaternion.Euler(0, rotatey, 0);
            }
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                canRotate = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                canRotate = true;
            }
        
    }
}
