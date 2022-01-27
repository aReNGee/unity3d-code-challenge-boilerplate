#region Namespaces

using System.Collections.Generic;
using UnityEngine;
using TMPro;

#endregion // Namespaces.

/// <summary>
///     Class that displays the player's current inventory.
///     There is no interaction with the inventory UI, you can only close it.
/// </summary>

public class UIInventory : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    [SerializeField] private TextMeshProUGUI _coinDisplayText = null;

    private List<UIInventoryItem> _UIInventoryItemList = new List<UIInventoryItem>();

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
    ///     Method that sets up the inventory items in the grid.
    ///     Allows us to have a different amount of inventory items each playthrough.
    /// </summary>
    private void InitialInventorySetUp()
    {
        UIInventoryItem firstUIInventoryItem = GetComponentInChildren<UIInventoryItem>();
        GameObject firstInventoryGameObject = firstUIInventoryItem.gameObject;
        Transform containerTransform = firstInventoryGameObject.transform.parent;
        firstInventoryGameObject.gameObject.name = "Inventory Item (" + Player.Instance.Inventory[0].Item.ToString() + ")";
        firstUIInventoryItem.UpdateInventoryIconSprite(Player.Instance.Inventory[0].Item);
        _UIInventoryItemList.Add(firstUIInventoryItem);


        for (int i = 1; i < Player.Instance.Inventory.Count; i++)
        {
            UIInventoryItem newInventoryItem = Instantiate(firstInventoryGameObject, containerTransform).GetComponent<UIInventoryItem>();
            newInventoryItem.gameObject.name = "Inventory Item (" + Player.Instance.Inventory[i].Item.ToString() + ")";
            _UIInventoryItemList.Add(newInventoryItem);
            newInventoryItem.UpdateInventoryIconSprite(Player.Instance.Inventory[i].Item);
        }
    }

    /// <summary>
    ///     Method that visually updates the items in the inventory.
    ///     Only called when opening the inventory.
    /// </summary>
    public void UpdateInventory()
    {
        // Update each of the inventory items
        for (int i = 0; i < _UIInventoryItemList.Count; i++)
        {
            ItemData itemData = AssetController.Instance.ItemAsset.ItemDataList[(int)Player.Instance.Inventory[i].Item];
            string itemDescription = itemData.Name + " x " + Player.Instance.Inventory[i].Amount.ToString();
            _UIInventoryItemList[i].UpdateInventoryItemText(itemDescription);
        }

        // Update the player's coin display.
        _coinDisplayText.text = "Cash: $" + Player.Instance.Coins.ToString();
    }

    #endregion // Methods.
}
