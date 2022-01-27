#region Namespaces

using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class attached to the main camera of the scene.
///     Used to have the cameara locked behind the player object when it turns.
/// </summary>
[DisallowMultipleComponent]
public class CameraFollow : MonoBehaviour
{
    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Update()
    {
        // Snap the camera behind the player, even as the player turns.
        transform.position = new Vector3(Player.Instance.transform.position.x, transform.position.y, Player.Instance.transform.position.z) - (Player.Instance.transform.forward * 15f);
        // Rotate the camera to face the player.
        transform.LookAt(Player.Instance.transform);
    }

    #endregion // MonoBehaviour Methods.

}
