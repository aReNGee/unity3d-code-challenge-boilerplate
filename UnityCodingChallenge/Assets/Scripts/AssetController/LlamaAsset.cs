#region Namespaces

using System.Collections.Generic;
using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     Class the generates the LlamaAsset.asset
///     Contains a list of different materials for the Llama.cs to access, as well as two different colors for UIPen.cs
/// </summary>

[CreateAssetMenu(fileName = "LlamaAsset", menuName = "ScriptableObjects/LlamaAsset", order = 1)]
public class LlamaAsset : ScriptableObject
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables
    public List<Material> LlamaMaterialList = new List<Material>();
    public Color PenItemSafeColor;
    public Color PenItemWarningColor;

    #endregion // Variables.
}
