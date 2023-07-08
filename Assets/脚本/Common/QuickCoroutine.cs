using UnityEngine;
using System.Collections;
using System;

public class CoroutineMono : MonoBehaviour
{

}

public class QuickCoroutine : Singleton<QuickCoroutine>
{
    GameObject _coroutineRoot;
    MonoBehaviour _coroutineMono;
    public void Init()
    {
        _coroutineRoot = new GameObject("QuickCoroutine");
        GameObject.DontDestroyOnLoad(_coroutineRoot);
        _coroutineMono = _coroutineRoot.AddComponent<CoroutineMono>();
    }

    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return _coroutineMono.StartCoroutine(coroutine);
    }
    public Coroutine StartCoroutine(string coroutineName)
    {
        return _coroutineMono.StartCoroutine(coroutineName);
    }

    public void StopCoroutine(string coroutineName)
    {
        _coroutineMono.StopCoroutine(coroutineName);
    }
    public void StopCoroutine(IEnumerator coroutine)
    {
        _coroutineMono.StopCoroutine(coroutine);
    }
}

