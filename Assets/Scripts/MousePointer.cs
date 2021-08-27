using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    Vector3 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    void Update()
    {
        transform.position = new Vector3(MousePos.x, MousePos.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Mouse Trigger");
    }
}
