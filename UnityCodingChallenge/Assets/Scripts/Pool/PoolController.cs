#region Namespaces

using UnityEngine;
using System.Collections.Generic;

#endregion // Namespaces.

/// <summary>
///     Class that was designed to handle all the pooled GameObjects in the game.
///     
///     I'd usually use a Dictionary and custom classes but I would need to use a third party extension to serialize it.
///     [SerializeField] private Dictionary<PoolType, PoolData> _poolDictionary = new Dictionary<PoolType, PoolData>();
///     
///     In this implementation I'm using a list to handle the various pooldata variables.
///     Navigate through the list using PoolType enums as indicies.
///     Pooled objects are stored inactive in designated container child objects.
/// </summary>

[DisallowMultipleComponent]
public class PoolController : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public enum PoolType
    {
        Llama,
        Coin
    };

    public static PoolController Instance;    

    [SerializeField] private List<PoolData> _poolList = new List<PoolData>();

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

        // Pooled Object Setup.
        // Create the pool items, put them into the proper container.
        for (int j = 0; j < _poolList.Count; j++){
            // Create a new container for these pooled objects and store it in a list.
            GameObject newContainerGameObject = new GameObject();
            newContainerGameObject.transform.SetParent(transform);
            newContainerGameObject.name = ((PoolType)j).ToString() + " Container";
            _poolList[j].AddContainerTransform(newContainerGameObject.transform);

            // Iterate through a list to create the number of pooled objects we wanted.
            for (int i = 0; i < _poolList[j].NumberOfObjectsToCreateOnLaunch; i++)
            {
                GameObject tempGameObject = Instantiate(_poolList[j].PoolPrefab, _poolList[j].ContainerTransform);
                tempGameObject.name = "Pool " + ((PoolType)j).ToString() + " (" + i + ")";
                tempGameObject.SetActive(false);
            }
        }
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Get a GameObject from the pool and return the GameObject.
    /// </summary>
    public GameObject GetPooledObject(PoolType poolType)
    {
        int indexOfPool = (int)poolType;
        // Confirm that we have a valid poolType.
        if (_poolList.Count > indexOfPool){
            if (_poolList[indexOfPool].ContainerTransform.childCount > 0)
            {
                // Return the first object in the container;
                GameObject tempGameObject = _poolList[indexOfPool].ContainerTransform.GetChild(0).gameObject;
                return tempGameObject;
            }
            else if (_poolList[indexOfPool].CanCreateAdditionalObjectsAtRuntime)
            {
                // Create a new object to return.
                Debug.LogWarning("Creating a pooled object at runtime!");
                GameObject tempGameObject = Instantiate(_poolList[indexOfPool].PoolPrefab, _poolList[indexOfPool].ContainerTransform);
                tempGameObject.name = "Pool " + ((PoolType)indexOfPool).ToString() + " (Runtime)";
                tempGameObject.SetActive(false);
                return tempGameObject;
            }
        }

        return null;
    }

    /// <summary>
    ///     Return a GameObject back into the pool. 
    ///     This should be the GameObject that was taken from the pool, no other GameObject.
    /// </summary>
    public void ReturnPooledObject(PoolType poolType, GameObject tempGameObject)
    {
        int indexOfPool = (int)poolType;
        // Confirm that we have a valid poolType.
        if (_poolList.Count > indexOfPool)
        {
            // Move the object back into its container and deactivate it.
            tempGameObject.transform.SetParent(_poolList[indexOfPool].ContainerTransform);
            tempGameObject.SetActive(false);
        }
    }

    #endregion // Methods.
}
