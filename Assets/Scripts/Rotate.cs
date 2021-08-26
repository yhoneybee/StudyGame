using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotate Axis")]
    [SerializeField]
    bool x;
    [SerializeField]
    bool y;
    [SerializeField]
    bool z;
    [SerializeField]
    float force = 1f;

    private void Update()
    {
        transform.Rotate(new Vector3(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0) * Time.deltaTime * force);
    }
}
