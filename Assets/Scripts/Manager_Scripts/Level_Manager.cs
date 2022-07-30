using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] private GameObject CarsParent;
    [HideInInspector] public List<GameObject> CarList;

    private void Awake()
    {
        CarList = new List<GameObject>();

        AddCars();
    }

    private void AddCars ()
    {
        for (int i = 0; i < CarsParent.transform.childCount; i++)
        {
            CarList.Add(CarsParent.transform.GetChild(i).gameObject);
        }
    }

    public void LevelCompleted()
    {

    }
}
