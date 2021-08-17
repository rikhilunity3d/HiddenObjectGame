using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScene : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField]
    GameObject BackgroundImage;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    Vector3 touchStart;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                mainCamera.orthographicSize += zoomModifier;

            if (touchesPrevPosDifference < touchesCurPosDifference)
                mainCamera.orthographicSize -= zoomModifier;

            

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 3.9f, 5.9f);

            
            Debug.Log("Camera Size "+ mainCamera.orthographicSize);
        }

        //Scroll


        
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        }
        if(Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        

    }
}
