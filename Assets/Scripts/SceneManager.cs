using FishNet;
using FishNet.Managing.Scened;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void Update()
    {
        if (!InstanceFinder.IsServer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnloadScene("Game");
            LoadScene("Main");
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnloadScene("Main");
            LoadScene("Game");        
        }

    }
    public void LoadScene(string sceneName)
    {
        if (!InstanceFinder.IsServer)
        {
            return;
        }

        SceneLoadData sld = new SceneLoadData(sceneName);
        //sld.ReplaceScenes = ReplaceOption.All;
        InstanceFinder.SceneManager.LoadGlobalScenes(sld);
    }

    public void UnloadScene(string sceneName)
    {
        if (!InstanceFinder.IsServer)
        {
            return;
        }

        SceneUnloadData sld = new SceneUnloadData(sceneName);
        InstanceFinder.SceneManager.UnloadGlobalScenes(sld);
    }
}
