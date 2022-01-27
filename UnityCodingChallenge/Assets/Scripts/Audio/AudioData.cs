#region Namespaces

using System;
using UnityEngine;
using UnityEngine.Audio;

#endregion // Namespaces.

/// <summary>
///     Class that holds the information for audio effects to be used in the game.
///     Mostly used for Sound Effects, but supports BGM too.
/// </summary>

[Serializable]
public class AudioData
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    // Not used in code, just there to help seperate the items in the list in a readable way.
    public string HumanReadableName = "";

    public AudioClip AudioClip;
    // Specifies which mixer group to play through.
    public AudioMixerGroup AudioMixerGroup = null;

    #endregion // Variables.
}
