using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleUI : MonoBehaviour
{
    public FlexibleUIData skinData;



    //Let's you edit UI and update the corresponding scriptoableobject automaticly
    protected virtual void onSkinUI()
    {

    }
    public virtual void Awake()
    {
        onSkinUI();
    }

    public virtual void Update()
    {
        if (Application.isEditor)
        {
            onSkinUI();
        }
    }
}
