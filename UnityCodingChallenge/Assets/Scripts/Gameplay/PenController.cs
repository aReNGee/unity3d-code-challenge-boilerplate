#region Namespaces

using System.Collections.Generic;
using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class that handles physically storing llamas in the pen as well as providing a reference point for UIPen.
///     Includes methods for capturing, feeding, and releasing llamas.
///     Llamas handle their own health decay.
/// </summary>

public class PenController : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables
    public static PenController Instance;

    [SerializeField] private UIPen _uiPen;

    // Reference to the locations to move the captured llamas to.
    [SerializeField] private List<Transform> _capturePositionsList = new List<Transform>();

    // Reference to the captured llamas.
    private List<Llama> _capturedLlamaList = new List<Llama>();
    public List<Llama> CapturedLlamaList
    {
        get { return _capturedLlamaList; }
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
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Checks if we have captured the llama we're attempting to feed.
    ///     Then it checks if the player has any inventory items of the appropriate type.
    ///     If they do, spend one and feed the llama.
    /// </summary>    
    public void FeedLlama(int indexOfLlama)
    {
        if (indexOfLlama < _capturedLlamaList.Count)
        {
            // Find the location of the appropriate item in the inventory.
            int inventoryIndex = (int)ConvertDietToAssociatedItem(_capturedLlamaList[indexOfLlama].Diet);
            // If we have any items of that type, use on and feed the llama.
            if (Player.Instance.Inventory[inventoryIndex].Amount > 0)
            {
                _capturedLlamaList[indexOfLlama].Feed();
                Player.Instance.UseInventoryItem(inventoryIndex, 1);
                _uiPen.UpdatePen();
                // Play feed llama sound fx.
                AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.LlamaFeedSuccessful);
            }
            else
            {
                // Play "uanble to feed llama" sound fx.
                AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.LlamaFeedFail);
            }
        }
    }

    /// <summary>
    ///     Adds the cpatured llama to the ist.
    ///     Returns the position in the pen to move the captured llama to.
    /// </summary>    
    public Vector3 CaptureLlama(Llama llama)
    {
        _capturedLlamaList.Add(llama);
        return _capturePositionsList[_capturedLlamaList.Count - 1].position;
    }

    /// <summary>
    ///     Removes a captured llama from the list.
    ///     Called when a llama dies.
    /// </summary>    
    public void ReleaseLlama(Llama llama)
    {
        if (_capturedLlamaList.Contains(llama))
        {
            _capturedLlamaList.Remove(llama);
        }
    }

    /// <summary>
    ///     Method that converts a llama's diet into the associated item that feeds that diet.
    /// </summary> 
    private ItemData.ItemType ConvertDietToAssociatedItem(Llama.DietType diet)
    {
        if (diet == Llama.DietType.Grass)
        {
            return ItemData.ItemType.Grass;
        }
        else if (diet == Llama.DietType.Flower)
        {
            return ItemData.ItemType.Flower;
        }
        else if (diet == Llama.DietType.Shrub)
        {
            return ItemData.ItemType.Shrub;
        }

        return ItemData.ItemType.Grass;
    }

    #endregion // Methods.
}
