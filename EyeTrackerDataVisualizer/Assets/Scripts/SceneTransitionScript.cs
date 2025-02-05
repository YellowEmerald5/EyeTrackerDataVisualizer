using System.Collections;
using System.Collections.Generic;
using DataAccessAndPreparation;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour
{
    [SerializeField] private StorageSO storage;
    public int NextScene;
    
    /// <summary>
    /// Prepares for replay and changes the scene to the visualization scene
    /// </summary>
    public void ChangeScene()
    {
        var preparedData = DatabaseHandler.PrepareReplay(storage.GameList);
        storage.GameList = preparedData.PreparedGames;
        //storage.Timestamps = preparedData.TimeStamps;
        storage.SensorData = preparedData.SensorData;
        storage.TotalTimestampEntries = preparedData.TotalTimestamps;
        storage.StartAndEndPoints = preparedData.StartAndEndPoints;
        SceneManager.LoadScene(NextScene);
    }
}
