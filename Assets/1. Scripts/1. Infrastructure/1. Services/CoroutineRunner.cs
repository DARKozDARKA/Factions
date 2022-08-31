using System.Collections;
using System.Collections.Generic;
using CodeBase.Infastructure;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    private void OnEnable()
    {
        DontDestroyOnLoad(this);

    }

}
