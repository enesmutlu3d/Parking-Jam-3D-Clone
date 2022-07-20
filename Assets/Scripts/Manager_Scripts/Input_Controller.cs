using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    private Vector2 mousePosFirst;
    private Vector2 mouseVector;
    private bool isVertical;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosFirst = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseVector.x = Input.mousePosition.x - mousePosFirst.x;
            mouseVector.y = Input.mousePosition.y - mousePosFirst.y;
            CalculateDirection(mouseVector.x, mouseVector.y);
        }
    }

    private void CalculateDirection(float x, float y)
    {
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            isVertical = false;
        }
        else
        {
            isVertical = true;
        }
    }

}
