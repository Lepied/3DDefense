using FishNet;
using FishNet.Managing.Scened;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
 
    public void StartGame()
    {
        SceneLoadData gameSceneData = new SceneLoadData(sceneName: "Game");
        gameSceneData.ReplaceScenes = ReplaceOption.All;
        InstanceFinder.SceneManager.LoadGlobalScenes(gameSceneData);
    }
}
