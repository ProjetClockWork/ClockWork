using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LocationStorer : MonoBehaviour
{
    private Transform _transform;

    [SerializeField]private Vector3[] pastPos;
    private int LastPosID;

    void Start()
    {
        TryGetComponent(out _transform);
        LastPosID = pastPos.Length - 1;
        Debug.Log("Last position ID in Array =" + LastPosID);
    }


    void Update()
    {
       StorePosition();
       //Debug.Log(pastPos[0]);
       //Debug.Log(pastPos[pastPos.Length - 1]);
       //Debug.Log("----");
    }


    void StorePosition()
    {
        for (int i = pastPos.Length - 1; i >= 1; i--)
        {
            pastPos[i] = pastPos[i - 1];
        }
        pastPos[0] = _transform.position;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pastPos[LastPosID], 1);
    }

}
