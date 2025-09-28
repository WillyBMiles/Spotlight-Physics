using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public string instanceCheck;

    static Dictionary<string, GameObject> instances = new();

    private void Awake()
    {
        if (string.IsNullOrEmpty(instanceCheck))
            return;
        if (instances.ContainsKey(instanceCheck))
        {
            Destroy(gameObject);
            return;
        }
        instances[instanceCheck] = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
