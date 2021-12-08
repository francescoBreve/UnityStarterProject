using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class UIElement
{
    [SerializeField] public string UIKey;
    [SerializeField] public GameObject UIObject;
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIElement[] UIElements;
    public Dictionary<string, GameObject> _UIElements;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
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
            _UIElements.Add(x.UIKey, x.UIObject);
        }
    }

    public void ShowElement(string UIelementName)
    {
        _UIElements[UIelementName].SetActive(true);
    }
    public void HideElement(string UIelementName)
    {
        _UIElements[UIelementName].SetActive(false);
    }
}
