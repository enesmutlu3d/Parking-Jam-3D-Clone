using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    private Vector2 mousePosFirst;
    private Vector2 mouseVector;

    private bool isVertical;
    private bool isPositive;

    private GameObject selectedCar;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit, 100.0f))
            {
                if (hit.transform.TryGetComponent(out ID_SelectibleCar selectibleCar))
                {
                    selectedCar = hit.transform.gameObject;
                }
                else
                {
                    return;
                }
            }

            mousePosFirst = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseVector.x = Input.mousePosition.x - mousePosFirst.x;
            mouseVector.y = Input.mousePosition.y - mousePosFirst.y;
            CalculateDirection(mouseVector.x, mouseVector.y);
            MoveSelectedCar();
        }
    }

    private void CalculateDirection(float x, float y)
    {
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            isVertical = false;
            if (x < 0)
                isPositive = false;
            else
                isPositive = true;
        }
        else
        {
            isVertical = true;
            if (y < 0)
                isPositive = false;
            else
                isPositive = true;
        }
    }

    private void MoveSelectedCar ()
    {
        if (selectedCar != null)
            selectedCar.GetComponent<CarMovement>().MoveCar(isVertical, isPositive);
        selectedCar = null;
    }

}
