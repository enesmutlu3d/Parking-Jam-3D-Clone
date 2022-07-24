using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Transform rayPosForward;
    [SerializeField] private Transform rayPosBackward;

    private bool isCarPlacedVertical = true;
    private List<Transform> gapHits;

    private void Awake()
    {
        gapHits = new List<Transform>();
        if (transform.forward == Vector3.right || transform.forward == Vector3.left)
            isCarPlacedVertical = false;
    }

    public void MoveCar (bool isVertical, bool isPositive)
    {
        if (isVertical != isCarPlacedVertical)
            return;
        if (isPositive)
            MoveForward();
        else
            MoveBackward();
    }

    private void MoveForward()
    {
        CheckGap(rayPosForward);
        transform.position = transform.position + transform.forward * 2f * gapHits.Count;
    }

    private void MoveBackward()
    {
        CheckGap(rayPosBackward);
        transform.position = transform.position + transform.forward * -2f * gapHits.Count;
    }

    private void CheckGap(Transform rayPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayPos.transform.position, rayPos.transform.forward, out hit, 100.0f))
        {
            if (hit.transform.TryGetComponent(out ID_EmptyGrid emptyGrid))
            {
                gapHits.Add(hit.transform);
                hit.transform.GetComponent<BoxCollider>().enabled = false;
                CheckGap(rayPos);
            }
            else
            {
                return;
            }
        }
    }

}
