using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Transform rayPosForward;
    [SerializeField] private Transform rayPosBackward;
    [SerializeField] private MeshCollider _meshCollider;
    [SerializeField] private float Speed;
    [SerializeField] private Level_Manager _levelManager;

    private bool isCarPlacedVertical = true;
    private List<Transform> gapHits;
    private Transform rayPos;
    private float gapSize;
    private int carCount;

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

        VarSet(isPositive);

        CheckEmpty(rayPos);
        GridReEnable();
    }

    private void VarSet (bool isPositive)
    {
        if (isPositive)
        {
            rayPos = rayPosForward;
            gapSize = 2f;
        }
        else
        {
            rayPos = rayPosBackward;
            gapSize = -2f;
        }
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
            else if (hit.transform.TryGetComponent(out ID_ExitWay exitway))
            {
                _meshCollider.enabled = false;
                transform.DOMove(transform.position + transform.forward * gapSize * gapHits.Count, gapHits.Count * (1 / Speed) * 1.5f)
                    .OnComplete(() => DoExit(rayPos));
            }
            else
            {
                transform.DOMove(transform.position + transform.forward * gapSize * gapHits.Count, gapHits.Count * (1 / Speed) * 1.5f);
                return;
            }
        }
    }

    private void DoExit(Transform rayPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayPos.transform.position, rayPos.transform.forward, out hit, 100.0f))
        {
            if (hit.transform.TryGetComponent(out ID_ExitWay exitway))
            {
                transform.DOMove(hit.transform.position + (hit.transform.forward * -1), (1 / Speed)).SetEase(Ease.Linear);
                transform.DORotate(hit.transform.rotation.eulerAngles, (1 / Speed)).SetEase(Ease.Linear)
                    .OnComplete(() => DoExit(rayPos));
            }
        }
        else
        {
            transform.DOMove(transform.position + transform.forward * 50f, (1 / Speed) * 50f);
            _levelManager.CarList.RemoveAt(_levelManager.CarList.Count - 1);
            if (_levelManager.CarList.Count == 0)
            {
                _levelManager.LevelCompleted();
            }
        }
    }

    private void GridReEnable()
    {
        for (int i = 0; i < gapHits.Count;)
        {
            gapHits[i].GetComponent<BoxCollider>().enabled = true;
            gapHits.Remove(gapHits[i]);
        }
    }

}
