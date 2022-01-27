#region Namespaces

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion // Namespaces.

/// <summary>
///     Class that was designed to handle all of the UI Shop.
///     Use to control the UI Shop in the application.
/// 
///     How to setup:
///     - Create a new GameObject in the scene and call it UI Shop.
///     - Attach to UI Shop GameObject.
///     - Hierarchy: UI Controller > UI Canvas > UI Shop (Here it is located).
/// </summary>

public class UIController : MonoBehaviour
{
    
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public static UIController Instance;

    [SerializeField] private UIInventory _UIInventory;
    [SerializeField] private UIPen _UIPen;
    private UIGeneralStore _UIGeneralStore;

    private bool _isAUIScreenOpen = true;
    public bool IsAUIScreenOpen
    {
        get { return _isAUIScreenOpen; }
    }

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
        
        // Loads the general store scene into the main scene.
        SceneManager.LoadScene("GeneralStore",LoadSceneMode.Additive);
    }

    private void Start()
    {
        // Closes all the UI screens.
        _UIGeneralStore = FindObjectOfType<UIGeneralStore>();
        CloseInventory(false);
        ClosePenMenu(false);
        CloseGeneralStore(false);
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that opens the Inventory UI.
    /// </summary>
    public void OpenInventory(bool playSFX = true)
    {
        _UIInventory.gameObject.SetActive(true);
        _UIInventory.UpdateInventory();
        _isAUIScreenOpen = true;

        if (playSFX) AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.OpenInventory);
    }

    /// <summary>
    ///     Method that closes the Inventory UI.
    /// </summary>
    public void CloseInventory(bool playSFX = true)
    {
        _UIInventory.gameObject.SetActive(false);
        _isAUIScreenOpen = false;

        if (playSFX) AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.CloseInventory);
    }

    /// <summary>
    ///     Method that opens the Pen UI.
    /// </summary>
    public void OpenPenMenu(bool playSFX = true)
    {
        _UIPen.gameObject.SetActive(true);
        _UIPen.UpdatePen();
        _isAUIScreenOpen = true;

        if (playSFX) AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.OpenInventory);
    }

    /// <summary>
    ///     Method that closes the Pen UI.
    /// </summary>
    public void ClosePenMenu(bool playSFX = true)
    {
        _UIPen.gameObject.SetActive(false);
        _isAUIScreenOpen = false;

        if (playSFX) AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.CloseInventory);
    }

    /// <summary>
    ///     Method that opens the Store UI.
    /// </summary>
    public void OpenGeneralStore(bool playSFX = true)
    {
        _UIGeneralStore.gameObject.SetActive(true);
        _UIGeneralStore.UpdateCashDisplay();
        _isAUIScreenOpen = true;

        if (playSFX) AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.OpenInventory);
    }

    /// <summary>
    ///     Method that closes the Store UI.
    /// </summary>
    public void CloseGeneralStore(bool playSFX = true)
    {
        _UIGeneralStore.gameObject.SetActive(false);
        _isAUIScreenOpen = false;

        if (playSFX) AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.CloseInventory);
    }

    #endregion // Methods.
}
