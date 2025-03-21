using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LocationStorer : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rgbd2D;
    private Collider2D _collider2D;

    [SerializeField]private Vector3[] pastPos;
    private int _lastPosID;

    private bool _isRewind;
    private int _rewindStep;

    void Awake()
    {
        TryGetComponent(out _transform);
        TryGetComponent(out _rgbd2D);
        TryGetComponent(out _collider2D);
    }
    void Start()
    {
        _lastPosID = pastPos.Length - 1;
        Debug.Log("Last position ID in Array =" + _lastPosID);
    }


    void Update()
    {
        if (_isRewind == true)
        {
            RewindStep();
        }
        else
        {
            StorePosition();
        }
    }
    void StorePosition()
    {
        for (int i = pastPos.Length - 1; i >= 1; i--)
            pastPos[i] = pastPos[i - 1];

        pastPos[0] = _transform.position;
    }

    void StartRewind()
    {
        _isRewind = true;
        _rewindStep = 0;

        _collider2D.enabled = false;
        _rgbd2D.gravityScale = 0;
    }

    void RewindStep()
    {
        if (_rewindStep < _lastPosID)
        {
            _transform.position = pastPos[_rewindStep];
            Debug.Log("rewind step " + _rewindStep);
            _rewindStep += 2;
        }
        else 
        {
            _isRewind = false;

            _collider2D.enabled = true;
            _rgbd2D.gravityScale = 1;
            gameObject.SendMessage("StateToNormal");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        for (int i = 1; i < _lastPosID - 1 ; i++)
            Gizmos.DrawSphere(pastPos[i], 0.1f);

        Gizmos.DrawSphere(pastPos[_lastPosID], 0.5f);
    }

}
