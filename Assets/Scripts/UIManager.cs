using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<UIElement> UIElements = new List<UIElement>();
    public Dictionary<string, UIElement> _UIElements = new Dictionary<string, UIElement>();

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
            Destroy(this);
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
    
    public void CacheUIElementsIndexes()
    {
        foreach(UIElement x in UIElements)
        {
            _UIElements.Add(x.UIKey, x);
        }
    }

    public void ShowElement(string UIelementName)
    {
        _UIElements[UIelementName].gameObject.SetActive(true);
    }

    public void HideElement(string UIelementName)
    {
        _UIElements[UIelementName].gameObject.SetActive(false);
    }
}
