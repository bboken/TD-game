using UnityEngine;
using System.Collections;

public class camCon : MonoBehaviour
{
    public Transform cam;
    Vector3 offset;

    [Header("mobile")]
    public float dragSpeed = 2f;
    Vector2 drag;
    public Vector2 dragMax;
    public Vector2 dragMin;
    public float zoomSpeed = 5f;
    public float zoomMax;
    public float zoomMin;

    [Header("PC")]
    public float speed;
    public float PCzoomSpeed = 5f;

    void Start()
    {
        offset =  cam.position - transform.position;
    }


    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            drag.x = -touch.deltaPosition.x * dragSpeed * Time.deltaTime;
            drag.y = -touch.deltaPosition.y * dragSpeed * Time.deltaTime;
            transform.Translate(new Vector3(drag.x, 0, drag.y));

            Vector3 tempPos;
            tempPos = transform.position;
            tempPos.x = Mathf.Clamp(tempPos.x, dragMin.x, dragMax.x);
            tempPos.y = Mathf.Clamp(tempPos.y, zoomMin, zoomMax);
            tempPos.z = Mathf.Clamp(tempPos.z, dragMin.y, dragMax.y);
            transform.position = tempPos;
            print(tempPos);

        }
        if (Input.touchCount == 2)
        {

            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            Vector2 prevPos0 = touch0.position - touch0.deltaPosition;
            Vector2 prevPos1 = touch1.position - touch1.deltaPosition;
            float prevTouchDist = Vector2.Distance(prevPos0, prevPos1);
            float touchDist = Vector2.Distance(touch0.position, touch1.position);
            float move = touchDist - prevTouchDist;
            float zoom = move * zoomSpeed * Time.deltaTime;
            cam.Translate(Vector3.forward * zoom);
            Debug.Log("2point");

            float CamHeight = Camera.main.transform.position.y;
            if (Mathf.Clamp(CamHeight, zoomMin, zoomMax) != CamHeight)
                cam.Translate(Vector3.forward * -zoom);
        }
#endif

        Movement();
        Scale();

    }

    private void Scale()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel")  * PCzoomSpeed * Time.deltaTime;
        cam.Translate(Vector3.forward * zoom);
        float CamHeight = Camera.main.transform.position.y;
        if (Mathf.Clamp(CamHeight, zoomMin, zoomMax) != CamHeight)
            cam.Translate(Vector3.forward * -zoom);
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 targetDirection = new Vector3(h, 0, v);
            float y = cam.rotation.eulerAngles.y;
            targetDirection = Quaternion.Euler(0, y, 0) * targetDirection;

            transform.Translate(targetDirection * Time.deltaTime * speed, Space.World);


            Vector3 tempPos;
            tempPos = transform.position;
            tempPos.x = Mathf.Clamp(tempPos.x, dragMin.x, dragMax.x);
           // tempPos.y = Mathf.Clamp(tempPos.y, zoomMin, zoomMax);
            tempPos.z = Mathf.Clamp(tempPos.z, dragMin.y, dragMax.y);
            transform.position = tempPos;
        }
    }

}
