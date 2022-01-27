#region Namespaces

using UnityEngine;
using System.Collections.Generic;

#endregion // Namespaces.

/// <summary>
///     Class that handles object spawning in the scene via calls to PoolController.
///     Creates 5 llamas on startup.
///     Adds a new llama after a delay whenever a llama is captured.
///     Also handles the logic for spawning coins around a capured llama.
/// </summary>

[DisallowMultipleComponent]
public class WorldController : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public static WorldController Instance;

    // List used to queue multiple delayed llama spawns if necessary.
    private List<float> _llamaDelayedSpawnTimerList = new List<float>();

    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Awake()
    {
        // Singleton setup.
        if (Instance == null)
        {
            // Make the current instance as the singleton.
            Instance = this;
        }
        else
        {
            // If more than one singleton exists in the scene find the existing reference from the scene and destroy it.
            if (this != Instance)
            {
                Destroy(gameObject);
                return;
            }
        }
        // End of singleton setup.
    }

    private void Start()
    {
        // Start the game by spawning 5 llamas.
        SpawnLlama();
        SpawnLlama();
        SpawnLlama();
        SpawnLlama();
        SpawnLlama();
    }

    private void Update()
    {
        // Increment through all the delayed spawn timers.
        for (int i = 0; i < _llamaDelayedSpawnTimerList.Count; i++)
        {
            _llamaDelayedSpawnTimerList[i] -= Time.deltaTime;
            // When the timer runs out, spawn a new llama and remove the timer from the list.
            if (_llamaDelayedSpawnTimerList[i] <= 0)
            {
                SpawnLlama();
                _llamaDelayedSpawnTimerList.RemoveAt(i);
                i--;
            }
        }
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that spawns a group of coins at equidistant points in a circle around a centre point.
    /// </summary>
    public void SpawnCoins(int numberOfCoinsToSpawn, Vector3 centerpoint)
    {
        // Find the points on a circle to space out the coins.

        for (int i = 0; i < numberOfCoinsToSpawn; i++)
        {
            // Get the selected object from the pool and set it to active.
            GameObject coinGameObject = PoolController.Instance.GetPooledObject(PoolController.PoolType.Coin);
            // If the pool has no available objects and can't grow, we will receive a null reference.
            if (coinGameObject)
            {
                // Set the object active, then move the object into the appropriate container and set its position.
                coinGameObject.SetActive(true);            
                Transform coinGameObjectTransform = coinGameObject.transform;
                coinGameObjectTransform.SetParent(transform);

                // Find the appropriate points to spawn the coin at.
                // We want the to spawn at equally spaced points aroudn the centre.
                Vector3 spawnPosition = new Vector3(centerpoint.x + Mathf.Cos(Mathf.PI * 2 * i / numberOfCoinsToSpawn), 1.5f, centerpoint.z + Mathf.Sin(Mathf.PI * 2 * i / numberOfCoinsToSpawn));
            
                // Call the pooled object's setup script.
                coinGameObject.GetComponent<Coin>().Setup(spawnPosition);
            }
        }
    }

    /// <summary>
    ///     Method that creates a random delay and adds it to the _llamaDelayedSpawnTimerList.
    ///     When the timers in this list count down to zero, SpawnLlama() is called.
    ///     This method is called when a llama is captured.
    /// </summary>
    public void ReplaceCapturedLlama()
    {
        float delay = Random.Range(3f, 7f);
        _llamaDelayedSpawnTimerList.Add(delay);
    }

    /// <summary>
    ///     Method that spawns a single llama at a random position in the world.
    /// </summary>
    private void SpawnLlama()
    {
        // Get the selected object from the pool and set it to active.
        GameObject llamaGameObject = PoolController.Instance.GetPooledObject(PoolController.PoolType.Llama);
        // If the pool has no available objects and can't grow, we will receive a null reference.
        if (llamaGameObject)
        {
            // Set the object active, then move the object into the appropriate container and set its position.
            llamaGameObject.SetActive(true);            
            Transform llamaGameObjectTransform = llamaGameObject.transform;
            llamaGameObjectTransform.SetParent(transform);
            
            // Call the pooled object's setup script.
            llamaGameObject.GetComponent<Llama>().Setup();
        }
    }   

    #endregion // Methods.
}
