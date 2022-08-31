using System.Collections;
using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}