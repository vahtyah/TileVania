using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
   public static ScenePersist instance;
    private void Awake()
    {
        if(instance) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
