#region Namespaces

using System;

#endregion // Namespaces.

/// <summary>
///     Class used to store data on the players inventory.
///     Notes what type of item is in this slot as well as how many the player has.
/// </summary>

[Serializable]
public class InventoryItem
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables
    
    public ItemData.ItemType Item = ItemData.ItemType.Flower;
    public int Amount = 0;

    // Constructor for convinence.
    public InventoryItem(ItemData.ItemType itemType, int amount)
    {
        Item = itemType;
        Amount = amount;
    }

    #endregion // Variables.
}