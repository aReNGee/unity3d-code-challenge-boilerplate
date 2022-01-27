#region Namespaces

using System.Collections.Generic;
using UnityEngine;
using TMPro;

#endregion // Namespaces.

/// <summary>
///     General class intended to handle all stores in the game.
///     Implement by extending the base class and adding custom functionality.
///     Supports individualized available items and store names.
///     Does not support custom item prices.
/// </summary>

public class UIStore : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    [SerializeField] private TextMeshProUGUI _storeNameText = null;
    [SerializeField] private TextMeshProUGUI _coinDisplayText = null;

    // The name of this store.
    [SerializeField] private string _storeName = "Store";

    // The items that are availabe at this particualr store.
    [SerializeField] private List<ItemData.ItemType> _itemsSoldAtThisStore = new List<ItemData.ItemType>();

    // The visual display of the store items.
    private List<StoreItem> _storeItemList = new List<StoreItem>();

    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Start()
    {
        InitialInventorySetUp();
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods
    
    /// <summary>
    ///     Method that sets up the store page by duplicating out the number of display items we'll need.
    ///     Creates a number of items equal to the number of items sold at this store.
    /// </summary>
    private void InitialInventorySetUp()
    {
        StoreItem firstStoreItem = GetComponentInChildren<StoreItem>();
        GameObject firstStoreItemGameObject = firstStoreItem.gameObject;
        Transform containerTransform = firstStoreItemGameObject.transform.parent;
        firstStoreItemGameObject.gameObject.name = "Store Item (" + _itemsSoldAtThisStore[0].ToString() + ")";
        firstStoreItem.Setup(_itemsSoldAtThisStore[0], this);
        //firstStoreItem.UpdateInventoryIconSprite(Player.Instance.Inventory[0].Item);


        for (int i = 1; i < Player.Instance.Inventory.Count; i++)
        {
            StoreItem newStoreItem = Instantiate(firstStoreItemGameObject, containerTransform).GetComponent<StoreItem>();
            newStoreItem.gameObject.name = "Store Item (" + _itemsSoldAtThisStore[i].ToString() + ")";
            _storeItemList.Add(newStoreItem);
            newStoreItem.Setup(_itemsSoldAtThisStore[i], this);
        }

        _storeNameText.text = _storeName;
        _coinDisplayText.text = "Cash: $" + Player.Instance.Coins.ToString();
    }

    /// <summary>
    ///     Method that displays the player's current cash amount on this store UI.
    /// </summary>
    public void UpdateCashDisplay()
    {
        _coinDisplayText.text = "Cash: $" + Player.Instance.Coins.ToString();
    }

    /// <summary>
    ///     Method that attempts to buy a specific item.
    ///     Called by the StoreItems when clicked.
    /// </summary>
    public void BuyItem(ItemData.ItemType itemToBuy)
    {
        Player.Instance.BuyItem(itemToBuy);
        UpdateCashDisplay();
    }

    #endregion // Methods.
}
