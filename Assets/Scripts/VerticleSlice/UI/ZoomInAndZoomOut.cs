using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInAndZoomOut : MonoBehaviour
{
    private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifer;

    private Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField] private float zoomModifierSpeed = 0.1f;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    private bool canZoomInAndOut = true;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifer = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed * Time.deltaTime;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                transform.localScale += Vector3.one * zoomModifer;
            }
            else if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                transform.localScale -= Vector3.one * zoomModifer;
            }
        }

        Vector3 parentLocalScale = transform.localScale;
        parentLocalScale.x = Mathf.Clamp(parentLocalScale.x, minScale, maxScale);
        parentLocalScale.y = Mathf.Clamp(parentLocalScale.y, minScale, maxScale);
        parentLocalScale.z = Mathf.Clamp(parentLocalScale.z, minScale, maxScale);

        transform.localScale = parentLocalScale;


    }
}
