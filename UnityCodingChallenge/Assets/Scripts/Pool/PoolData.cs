#region Namespaces

using System;
using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class that is inteded for use with the PoolController's _poolList.
/// </summary>

[Serializable]
public class PoolData
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    // The prefab to add to the pool
    public GameObject PoolPrefab;

    // How many copies of the object we want to make on startup.
    public int NumberOfObjectsToCreateOnLaunch = 0;

    // Controls if we can add more objects later.
    public bool CanCreateAdditionalObjectsAtRuntime;

    // A reference to the transform to store our pooled objects.
    private Transform _containerTransform;
    public Transform ContainerTransform
    {
        get { return _containerTransform; }
    }

    #endregion // Variables.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that adds a reference to a container transform.
    ///     We do this using a method so the variable doesn't show up in the inspector.
    /// </summary>
    public void AddContainerTransform(Transform container)
    {
        _containerTransform = container;
    }

    #endregion // Methods.
}
