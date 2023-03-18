using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
