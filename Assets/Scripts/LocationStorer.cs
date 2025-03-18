using System;
using UnityEngine;

public class LocationStorer : MonoBehaviour
{
    private Transform _transform;

    [SerializeField]private Vector3[] pastPos;

    void Start()
    {
        TryGetComponent(out _transform);
    }


    void Update()
    {
        //Debug.Log(pastPos[0]);
        pastPos[0] = transform.position;
    }

    void StorePosition()
    {

    }
}
