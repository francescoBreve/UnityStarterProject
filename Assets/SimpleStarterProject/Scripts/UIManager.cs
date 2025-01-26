using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<UIElement> UIElements = new List<UIElement>();
    public Dictionary<string, UIElement> _UIElements = new Dictionary<string, UIElement>();

    public UIElement DefaultLoadingScreen; 

    //Omg not actually a private value but for some reason i'm exposing the cache here.
    //gotta figure out why I did it.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            CacheUIElementsIndexes();
            //Rest of your Awake code
        }
        else
        {
            if(this.UIElements.Count > 0)
            {
                foreach(UIElement u in this.UIElements)
                {
                    UIManager.instance.UIElements.Add(u);
                }
                CacheUIElementsIndexes();
            }
            Destroy(this);
        }
        
    }

    private void OnValidate()
    {
        UIElement[] elm = Resources.FindObjectsOfTypeAll<UIElement>();
        UIElements.Clear();
        foreach (UIElement e in elm)
        {
            UIElements.Add(e);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
      

    }
    // Update is called once per frame
    void Update()
    {
    
    }
   
    public GameObject GetUIElement(string UIkey)
    {
        return _UIElements[UIkey].gameObject;
    }

   public string GetElementUIKey(int x){
        return UIElements[x].gameObject.name;
   }

    public void RefreshUIElemets()
    {
        UIElements = UIElements.Where(item => item != null).ToList();
        var badKeys = _UIElements.Where(pair => pair.Value == null)
                        .Select(pair => pair.Key)
                        .ToList();
        foreach (var badKey in badKeys)
        {
            _UIElements.Remove(badKey);
        }
    }
    private void CacheUIElementsIndexes()
    {
        foreach(UIElement x in instance.UIElements)
        {   
            if(!instance._UIElements.ContainsKey(x.gameObject.name))
                instance._UIElements.Add(x.gameObject.name, x);
        }
    }

    public static void ShowElement(string UIelementName)
    {
        instance._UIElements[UIelementName].gameObject.SetActive(true);
    }

    public static void HideElement(string UIelementName)
    {
        instance._UIElements[UIelementName].gameObject.SetActive(false);
    }
}
