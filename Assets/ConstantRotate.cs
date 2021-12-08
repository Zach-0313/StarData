using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    public Vector3 Rotation;
    public float Speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Rotation * Speed * Time.deltaTime);
    }
}
