using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform spawnPoint;
    public Transform respawnPoint;
    public Transform PlatformSpawnPoint;
    public Transform Grid;
    public GameObject playerPrefab;
    public GameObject DarkPrefab;
    public GameObject LightPrefab;
    public string Mode;

    void Start()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        Mode = "Dark";
    }

    private void Awake()
    {
        instance = this;
    }

    public void Respawn ()
    {
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        if (Mode == "Light")
        {
            ChangeMode();
        }

    }

    void Update()
    {

    }

    public void ChangeMode()
    {
        if (Mode == "Dark")
        {
            Mode = "Light";
            Instantiate(LightPrefab, PlatformSpawnPoint.position, Quaternion.identity, Grid);
        }
        else if (Mode == "Light")
        {
            Mode = "Dark";
            Instantiate(DarkPrefab, PlatformSpawnPoint.position, Quaternion.identity, Grid);
        }
    }

}   


