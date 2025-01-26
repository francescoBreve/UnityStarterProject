using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/UIAddons/Linear Progress Bar")]
    public static void AddLinearPogressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UIAddonsPrefabs/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

    [MenuItem("GameObject/UI/UIAddons/Radial Progress Bar")]
    public static void AddRadialPogressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UIAddonsPrefabs/Linear Progress Bar")); 
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public Image fill;
    public Color color;

    private void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;

        float fillamount = currentOffset / maximumOffset;
        mask.fillAmount = fillamount;

        fill.color = color;
    }

}
