#region Namespaces

using UnityEngine;
using System;
using System.Collections.Generic;


#endregion // Namespaces.

/// <summary>
///     Traditionally I'd use Dictionaries for this, but I believe you need a third party plugin to serialize them.
///     So we'll be using a List instead, to hold the different Sound Effect types.
///     We'll navigate through the list using SoundEffectType as an enum instead of a key.
/// </summary>

[CreateAssetMenu(fileName = "AudioAsset", menuName = "ScriptableObjects/Audio Asset", order = 1)]
[Serializable]
public class AudioAsset : ScriptableObject
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    [SerializeField] public AudioData BackgroundMusicData;

    // Example of dictionary implementation.
    //[SerializeField] public Dictionary<AudioController.SoundEffectType, AudioData> SoundEffectDictionary;
    [SerializeField] public List<AudioData> SoundEffectList;

    #endregion // Variables.
}

