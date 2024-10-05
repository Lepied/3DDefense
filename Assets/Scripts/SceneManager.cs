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
    public static SceneManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���ٸ� ���� ����
            if (instance == null)
            {
                GameObject go = new GameObject("SceneManager");
                instance = go.AddComponent<SceneManager>();
                DontDestroyOnLoad(go); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
            }
            return instance;
        }
    }

    private void Update()
    {
        if (!InstanceFinder.IsServerStarted)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnloadScene("Game");
            UnloadScene("Lobby");
            LoadScene("Main");
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnloadScene("Main");
            UnloadScene("Game");
            LoadScene("Lobby");        
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnloadScene("Main");
            UnloadScene("Lobby");
            LoadScene("Game");
        }

    }
    public void LoadScene(string sceneName)
    {
        if (!InstanceFinder.IsServerStarted)
        {
            return;
        }
        SceneLoadData sld = new SceneLoadData(sceneName);
        //sld.ReplaceScenes = ReplaceOption.All;
        InstanceFinder.SceneManager.LoadGlobalScenes(sld);
        Debug.Log("���̵� :" + sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        if (!InstanceFinder.IsServerStarted)
        {
            return;
        }

        SceneUnloadData sld = new SceneUnloadData(sceneName);
        InstanceFinder.SceneManager.UnloadGlobalScenes(sld);
    }
}
