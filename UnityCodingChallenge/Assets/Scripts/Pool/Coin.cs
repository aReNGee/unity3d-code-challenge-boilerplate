#region Namespaces

using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class for the pooled coin object.
///     Contains no information except for setup and pooling.
///     The Player class conatians the information for how many coins to gain from collecting this object.
/// </summary>

[DisallowMultipleComponent]
public class Coin : MonoBehaviour
{

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that sets up the pooled objet.
    ///     In this case, it just sets the position.
    /// </summary>
    public void Setup(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }

    /// <summary>
    ///     Method that returns the pool object back to the pool.
    /// </summary>
    public void ReturnToPool()
    {
        PoolController.Instance.ReturnPooledObject(PoolController.PoolType.Coin, gameObject);
    }

    #endregion // Methods.
}
