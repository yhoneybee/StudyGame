using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
