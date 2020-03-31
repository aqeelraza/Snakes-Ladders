using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionTests : MonoBehaviour
{
    public static event Action<String> myEvent;

    // Start is called before the first frame update
    void Start()
    {
        myEvent += Foo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Foo(string str) { 
        
    }
}
