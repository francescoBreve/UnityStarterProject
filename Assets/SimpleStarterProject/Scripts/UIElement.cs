using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class UIElement : MonoBehaviour
{
    [SerializeField] public string UIKey;

    private void Reset()
    {
        UIKey = gameObject.name;

        if (DuplicateValidation())
        {
            Debug.LogError("Already existing object in hierarchy with UIKey: " + UIKey);
        }
    }

    private void OnValidate()
    {
        if (DuplicateValidation())
        {
           Debug.LogError("Already existing object in hierarchy with UIKey: " + UIKey);
        }
    }

    private bool DuplicateValidation()
    {
        UIElement[] elm = Resources.FindObjectsOfTypeAll<UIElement>();
        var dup = elm.Where(i => i.UIKey == this.UIKey);
        return dup.ToList().Count > 1 ? true : false;
    }
}