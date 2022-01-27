#region Namespaces

using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class that holds references to all the scriptableObjects used in the project.
///     Singleton pattern.
/// </summary>

public class AssetController : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public static AssetController Instance;

    public LlamaAsset LlamaAsset;
    public AudioAsset AudioAsset;
    public ItemAsset ItemAsset;

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

    #endregion // MonoBehaviour Methods.
}
