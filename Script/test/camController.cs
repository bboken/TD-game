using UnityEngine;
using System.Collections;

public class camController : MonoBehaviour {
    //long
    public float dragSpeed = 2f;
    Vector2 drag;
    public Vector2 dragMax;
    public Vector2 dragMin;
    public float zoomSpeed = 5f;
    public float zoomMax;
    public float zoomMin;


    void Update()
    {

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            drag.x = -touch.deltaPosition.x * dragSpeed * Time.deltaTime;
            drag.y = -touch.deltaPosition.y * dragSpeed * Time.deltaTime;
            transform.Translate(new Vector3(drag.x, 0, drag.y));

            Vector3 tempPos;
            tempPos = transform.position;
            tempPos.x = Mathf.Clamp(tempPos.x, dragMin.x, dragMax.x);
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
            Camera.main.transform.Translate(Vector3.forward * zoom);
            Debug.Log("2point");

            float CamHeight = Camera.main.transform.position.y;
            if (Mathf.Clamp(CamHeight, zoomMin, zoomMax) != CamHeight)
                Camera.main.transform.Translate(Vector3.forward * -zoom);
        }
    }
    //long
}
