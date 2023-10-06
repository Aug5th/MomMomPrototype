using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMonoBehaviour : MonoBehaviour
{
    private void Reset()
    {
        LoadComponents();
    }

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        // for override
        // preparation codes for all classes are written here
    }
}
