using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    public static InputMgr Instance { get; private set; } = null;

    int MultiKey = 0;
    float FirstDir = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
    }

    public float GetAxisRaw(KeyCode left, KeyCode right)
    {
        float ReturnDir = 0;

        if (Time.timeScale == 0) return ReturnDir;

        if (Input.GetKey(left))
        {
            ReturnDir = -1;

            if (Input.GetKeyDown(left))
                ++MultiKey;

            if (MultiKey == 1)
                FirstDir = ReturnDir;
        }

        if (Input.GetKey(right))
        {
            ReturnDir = 1;

            if (Input.GetKeyDown(right))
                ++MultiKey;

            if (MultiKey == 1)
                FirstDir = ReturnDir;
        }

        if (MultiKey == 2)
            ReturnDir = -FirstDir;

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
            --MultiKey;

        if (MultiKey == 0)
            FirstDir = 0;

        return ReturnDir;
    }
}
