#region Namespaces

using System;
using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class that holds the information for individual items.
///     Each item has a name (generally the same as its ItemType).
///     Each item also has a price to buy in the shop, as well as an associated icon.
///     Items do not hold their own type,.
///     ItemData is referenced by using their associated ItemType as a index of the ItemAsset's list.
/// </summary>

[Serializable]
public class ItemData
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public enum ItemType
    {
        Grass,
        Flower,
        Shrub
    }

    public string Name = "";
    public int Price = 0;
    public Sprite Icon;

    #endregion // Variables.
}