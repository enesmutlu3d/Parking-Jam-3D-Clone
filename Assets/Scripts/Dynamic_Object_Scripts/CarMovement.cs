using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Transform rayPosForward;
    [SerializeField] private Transform rayPosBackward;

    private bool isCarPlacedVertical = true;
    private bool doExit = false;
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
        CheckEmpty(rayPosForward);
        CheckExit(rayPosForward);
        transform.position = transform.position + transform.forward * 2f * gapHits.Count;
        if (doExit)
        {
            ForwardExit();
        }
        GridReEnable();
    }

    private void MoveBackward()
    {
        CheckEmpty(rayPosBackward);
        CheckExit(rayPosBackward);
        if (doExit)
        {
            transform.position = transform.position + transform.forward * -2f * (gapHits.Count + 1);
        }
        else
        {
            transform.position = transform.position + transform.forward * -2f * gapHits.Count;
        }
        GridReEnable();
    }

    private void CheckEmpty(Transform rayPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayPos.transform.position, rayPos.transform.forward, out hit, 100.0f))
        {
            if (hit.transform.TryGetComponent(out ID_EmptyGrid emptyGrid))
            {
                gapHits.Add(hit.transform);
                hit.transform.GetComponent<BoxCollider>().enabled = false;
                CheckEmpty(rayPos);
            }
            else
            {
                return;
            }
        }
    }

    private void CheckExit(Transform rayPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayPos.transform.position, rayPos.transform.forward, out hit, 100.0f))
        {
            if (hit.transform.TryGetComponent(out ID_ExitWay exitWay))
            {
                doExit = true;
            }
        }
    }

    private void ForwardExit()
    {

    }

    private void BackwardExit()
    {

    }

    private void GridReEnable()
    {
        for (int i = 0; i < gapHits.Count;)
        {
            gapHits[i].GetComponent<BoxCollider>().enabled = true;
            gapHits.Remove(gapHits[i]);
        }
        doExit = false;
    }

}
