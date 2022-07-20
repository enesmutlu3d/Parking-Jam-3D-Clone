using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private bool isCarPlacedVertical = true;

    private void Awake()
    {
        if (transform.forward == Vector3.right || transform.forward == Vector3.left)
            isCarPlacedVertical = false;
    }

    public void MoveCar (bool isVertical, bool isPositive)
    {
        if (isVertical != isCarPlacedVertical)
            return;
        if (isPositive)
            transform.position = transform.position + transform.forward * 10f;
        else
            transform.position = transform.position + transform.forward * -10f;
        Debug.Log("movecar function");
    }

}
