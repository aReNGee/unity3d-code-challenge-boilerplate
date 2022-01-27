#region Namespaces

using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class intended specifically to play the footstep sound effect in the player's walk/run animation.
/// </summary>
[DisallowMultipleComponent]
public class Footsteps : MonoBehaviour
{
    #region Methods

    /// <summary>
    ///     Method that is called by the footstep event on the player's walk/run animation.
    ///     Uses AudioController to play the appropriate sound effect.
    /// </summary>
    public void FootstepEvent()
    {
        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.Footsteps);
    }

    #endregion // Methods.
}
