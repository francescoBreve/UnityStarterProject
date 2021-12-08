using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum GameState
{
    GAME_SETUP,
    GAME_LOOP,
    GAME_OVER
}

public enum SceneIndexs
{
    MANAGERS = 0
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

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
        switch (gameState) 
        {
            case GameState.GAME_SETUP:
                //State Code
                break;
            case GameState.GAME_LOOP:
                //State Code
                break;
            case GameState.GAME_OVER:
                //State Code
                break;
        }
    }

    //LoadSceneWithCurtain allows you to load and unload one or multiple scenes
    //In addition it supports the visualization of the UI Element declared in the UI Manager
    #region LoadSceneWithCurtain
    public void LoadSceneWithCurtain(int[] load, string loadingScreen)
    {
        UIManager.instance.ShowElement(loadingScreen);
        for (int i = 0; i < load.Length; i++)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(load[i], LoadSceneMode.Additive));
        }

        StartCoroutine(GetSceneLoadProgress(loadingScreen));
    }
    public void LoadSceneWithCurtain(int load, string loadingScreen)
    {
        scenesLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(loadingScreen));
    }
    public void LoadSceneWithCurtain(int[] unload, int[] load, string loadingScreen)
    {
        UIManager.instance.ShowElement(loadingScreen);

        for(int i=0; i<unload.Length; i++)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(unload[i]));
        }
        for (int i = 0; i < load.Length; i++)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(load[i], LoadSceneMode.Additive));
        }

        StartCoroutine(GetSceneLoadProgress(loadingScreen));
    }
    public void LoadSceneWithCurtain(int unload, int load, string loadingScreen)
    {
        UIManager.instance.ShowElement(loadingScreen);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(unload));
        scenesLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(loadingScreen));
    }
    public void LoadSceneWithCurtain(int[] unload, int load, string loadingScreen)
    {
        UIManager.instance.ShowElement(loadingScreen);
        for (int i = 0; i < unload.Length; i++)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(unload[i]));
        }
        scenesLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(loadingScreen));
    }
    public void LoadSceneWithCurtain(int unload, int[] load, string loadingScreen)
    {
        UIManager.instance.ShowElement(loadingScreen);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(unload));
        for (int i = 0; i < load.Length; i++)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(load[i], LoadSceneMode.Additive));
        }
        StartCoroutine(GetSceneLoadProgress(loadingScreen));
    }

    /*This method is meant to be called by a Coroutine. It will check the scenesLoading array and automatically disable
     *the UIElement with "loadingScreen" when the loading is complete.*/
    public IEnumerator GetSceneLoadProgress(string loadingScreen)
    {
        for(int i=0; i<scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        if(loadingScreen != null)
            UIManager.instance.HideElement(loadingScreen);
    }

    #endregion
}
