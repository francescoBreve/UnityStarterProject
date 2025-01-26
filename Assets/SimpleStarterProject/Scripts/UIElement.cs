using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[ExecuteAlways]
public class UIElement : MonoBehaviour
{
    

    private void Awake()
    {
    }
    
    private void Reset()
    {
   

        if (DuplicateValidation())
        {
            Debug.LogError("Already existing object in hierarchy with UIKey: " + gameObject.name);
        }
    }

    private void OnValidate()
    {
        Reset();
    }

    private bool DuplicateValidation()
    {
        if(this.gameObject.name == "" ){
            Debug.LogError("Object with empty UIKey existing in hierarchy with: " + gameObject.name);
            return false;
        }
        UIElement[] elm = Resources.FindObjectsOfTypeAll<UIElement>();
        var dup = elm.Where(i => i.gameObject.name == this.gameObject.name);
        return dup.ToList().Count > 1 ? true : false;
    }
}