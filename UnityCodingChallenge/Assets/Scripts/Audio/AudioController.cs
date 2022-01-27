#region Namespaces

using UnityEngine.Audio;
using System.Collections.Generic;
using System;
using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class that handles all of the audio in the game.
///     All AudioSources in the scene are created on the AudioController object.
///     Plays background music automatically.
///     Use PlaySoundEffect() to call sound effects.    
/// </summary>

public class AudioController : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    // Keys to the sound effect "dictionary"
    // Since we're using a list, be sure to add sound effects in exactly this order.
    public enum SoundEffectType
    {
        Footsteps,
        CaptureLlama,
        CollectMoney,
        OpenInventory,
        CloseInventory,
        LlamaFeedSuccessful,
        LlamaFeedFail,
        ShopPurchaseSuccessful,
        ShopPurchaseFail
    }

    public static AudioController Instance;

    // AudioListener for the entire scene.
    private AudioListener _audioListener;

    // The master audio mixer.
    [SerializeField] private AudioMixer _audioMixer = null;

    // AudioSource list for all the sound effects that will be handled for the scene.
    private readonly List<AudioSource> _soundEffectsAudioSourceList = new List<AudioSource>();


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

            // Make it persistent.
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // End of singleton setup.

        _audioListener = GetComponent<AudioListener>();

        SetupBackgroundMusic();
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Setup background music for the scene.
    /// </summary>
    public void SetupBackgroundMusic()
    {
        // Setup background music AudioSource
        AudioData backgroundMusicData = AssetController.Instance.AudioAsset.BackgroundMusicData;

        AudioSource tempBackgroundMusicAudioSource = _audioListener.gameObject.AddComponent<AudioSource>();
        tempBackgroundMusicAudioSource.rolloffMode = AudioRolloffMode.Linear;
        tempBackgroundMusicAudioSource.loop = true;
        tempBackgroundMusicAudioSource.volume = 1.0f;
        tempBackgroundMusicAudioSource.pitch = 1.0f;
        tempBackgroundMusicAudioSource.clip = backgroundMusicData.AudioClip;
        tempBackgroundMusicAudioSource.outputAudioMixerGroup = backgroundMusicData.AudioMixerGroup;
        tempBackgroundMusicAudioSource.ignoreListenerVolume = true;
        tempBackgroundMusicAudioSource.playOnAwake = false;
        tempBackgroundMusicAudioSource.Play();
    }

    /// <summary>
    ///     Play any sound effect using the SoundEffectType as an index to the SoundEffect list.
    /// </summary>
    /// <param name="soundEffectType"></param>
    public void PlaySoundEffect(SoundEffectType soundEffectType)
    {
        PlayOneShotSoundEffect(AssetController.Instance.AudioAsset.SoundEffectList[(int)soundEffectType]);
    }

    /// <summary>
    ///     Play the AudioClip that was passed in once and not on loop.
    /// </summary>
    /// <param name="playAudioClip"> AudioClip that will be played. </param>
    private void PlayOneShotSoundEffect(AudioData soundEffectData)
    {
        bool isPlaySuccess = false;

        // Check if there is an available AudioSource for sound effects and use that.
        if (_soundEffectsAudioSourceList.Count > 0)
        {
            for (int i = 0; i < _soundEffectsAudioSourceList.Count; i++)
            {
                if (_soundEffectsAudioSourceList[i].isPlaying == false)
                {
                    // Play sound effect.
                    _soundEffectsAudioSourceList[i].volume = 1.0f;
                    _soundEffectsAudioSourceList[i].pitch = 1.0f;

                    _soundEffectsAudioSourceList[i].outputAudioMixerGroup = soundEffectData.AudioMixerGroup;
                    _soundEffectsAudioSourceList[i].PlayOneShot(soundEffectData.AudioClip);
                    isPlaySuccess = true;
                    break;
                }
            }
        }

        // If there is not an available AudioSource for sound effects create a new one, add to sound effects list, and use that.
        if (isPlaySuccess == false)
        {
            // Play sound effect.
            AudioSource playAudioSource = _audioListener.gameObject.AddComponent<AudioSource>();
            playAudioSource.rolloffMode = AudioRolloffMode.Linear;
            playAudioSource.playOnAwake = false;
            playAudioSource.volume = 1.0f;
            playAudioSource.pitch = 1.0f;
            playAudioSource.outputAudioMixerGroup = soundEffectData.AudioMixerGroup;
            //Debug.Log("Audio Pitch: " + playAudioSource.pitch);

            playAudioSource.PlayOneShot(soundEffectData.AudioClip);

            // Add to sound effect list.
            _soundEffectsAudioSourceList.Add(playAudioSource);
        }
    }

    #endregion // Methods.
}
