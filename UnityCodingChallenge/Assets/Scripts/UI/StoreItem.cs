#region Namespaces

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion // Namespaces.

/// <summary>
///     Class that represents a single store item.
///     Diplays the icon, name, and cost of an item.
///     If clicked, will attempt to buy the item it is representing.
/// </summary>

public class StoreItem : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _itemNameText;

    private UIStore _uiStore;
    // The item this is representing.
    private ItemData.ItemType _itemType;

    #endregion // Variables.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method used to set up the store item.
    ///     Unlike other methods, this stores a reference to the item its reprsenting for use when buying the item.
    ///     Also stores a reference to the UIStore class, again for use when buying.
    /// </summary> 
    public void Setup(ItemData.ItemType itemType, UIStore uiStore)
    {
        _uiStore = uiStore;

        ItemData itemData = AssetController.Instance.ItemAsset.ItemDataList[(int)itemType];

        _icon.sprite = itemData.Icon;
        _costText.text = "$" + itemData.Price.ToString();
        _itemNameText.text = itemData.Name;

        _itemType = itemType;
    }

    /// <summary>
    ///     Method that attempts to buy the associated item.
    /// </summary> 
    public void BuyItem()
    {
        _uiStore.BuyItem(_itemType);
    }

    #endregion // Methods.
}
