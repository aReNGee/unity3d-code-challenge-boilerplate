#region Namespaces

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion // Namespaces.

/// <summary>
///     Class that manages a single Inventory space.
///     Only the text is updated on a regular basis.
///     Inventory items do not have a reference to ItemType because there is no inventory interaction.
/// </summary>

public class UIInventoryItem : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    private Image _icon;
    private TextMeshProUGUI _inventoryTextDisplay;

    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Awake()
    {
        _icon = GetComponentInChildren<Image>();
        _inventoryTextDisplay = GetComponentInChildren<TextMeshProUGUI>();
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that sets the inventory image to display the correct sprite.
    ///     Only when the inventory is set up.
    /// </summary>
    public void UpdateInventoryIconSprite(ItemData.ItemType itemType)
    {
        _icon.sprite = AssetController.Instance.ItemAsset.ItemDataList[(int)itemType].Icon;
    }

    /// <summary>
    ///     Method that updates the text display to show the player what item this is and how many they have.
    ///     Called whenever the player's inventory changes.
    /// </summary>
    public void UpdateInventoryItemText (string displayText)
    {
        _inventoryTextDisplay.text = displayText;
    }

    #endregion // Methods.
}
