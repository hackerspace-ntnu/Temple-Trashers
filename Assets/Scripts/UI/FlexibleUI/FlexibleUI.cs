using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlexibleUI : MonoBehaviour
{
    public FlexibleUIData skinData;

    /// <summary>
    /// Lets you edit UI and update the corresponding <c>ScriptableObject</c> automatically.
    /// </summary>
    protected virtual void OnSkinUI()
    {}

    public virtual void Awake()
    {
        OnSkinUI();
    }

    public virtual void Update()
    {
        if (Application.isEditor)
            OnSkinUI();
    }
}
