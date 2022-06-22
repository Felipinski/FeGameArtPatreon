using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotateBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = Vector3.up;
    [SerializeField]
    private float speed;

    void Update()
    {
        this.transform.Rotate(axis * speed * Time.deltaTime);
    }
}
