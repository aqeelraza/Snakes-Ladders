using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConstants 
{
    private static readonly UIConstants instance = new UIConstants();
    public static UIConstants Instance
    {
        get
        {
            return instance;
        }
    }

    public static string MissingReferenceTag = "UIReferenceMissing";
    public bool GameStarted = false;
}
