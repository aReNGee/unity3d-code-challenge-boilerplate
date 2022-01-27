#region Namespaces

using UnityEngine;
using System;
using System.Collections.Generic;

#endregion // Namespaces.

/// <summary>
///     Traditionally I'd use Dictionaries for this, but I believe you need a third party plugin to serialize them.
///     Uses a list to hold the ItemData for the different items.
///     Navigate the list by using Enums as indexes.
/// </summary>

[CreateAssetMenu(fileName = "ItemAsset", menuName = "ScriptableObjects/Item Asset", order = 1)]
[Serializable]
public class ItemAsset : ScriptableObject
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    [SerializeField] public List<ItemData> ItemDataList = new List<ItemData>();

    #endregion // Variables.
}