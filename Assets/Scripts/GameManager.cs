using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Button spawnButton;

    private bool isSpawning = false;
    private Coroutine spawnCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.onClick.AddListener(ToggleSpawn);
    }

    private void ToggleSpawn()
    {
        if(!isSpawning) 
        {
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }
        else
        {
            StopCoroutine(spawnCoroutine);
        }
        isSpawning = !isSpawning;
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnEnemy()
    {
        //GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        NetworkObject enemy = NetworkManager.GetPooledInstantiated(enemyPrefab,spawnPoint.position,Quaternion.identity,true);
        Spawn(enemy);
    }



}
