#region Namespaces

using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

#endregion // Namespaces.

/// <summary>
///     Class that displays the status of the different llamas in the pen.
///     No more than 15 llamas can be in the pen at one time.
/// </summary>

public class UIPen : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    [SerializeField] private List<TextMeshProUGUI> _inventoryTextList = new List<TextMeshProUGUI>();

    private List<PenItem> _UIPenItemList = new List<PenItem>();

    // Pen UI is only updated periodically because llama health is only updated periodically.
    private float _updateTimer = 0;
    private const float TIME_BETWEEN_UPDATES = 3f;

    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Start()
    {
        // Get a reference to the pen items and set them up.
        _UIPenItemList = GetComponentsInChildren<PenItem>().ToList<PenItem>();

        for (int i = 0; i < _UIPenItemList.Count; i++)
        {            
            _UIPenItemList[i].Setup(i);
        }

        _updateTimer = TIME_BETWEEN_UPDATES;
    }

    private void Update()
    {
        // Every 3 seconds, update the pen UI.
        _updateTimer -= Time.deltaTime;
        if (_updateTimer < 0)
        {
            UpdatePen();
            _updateTimer = TIME_BETWEEN_UPDATES;
        }
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that updates the PenUI with the latest information from llamas in the pen. 
    ///     Called every 3 seconds while the UI is active.
    /// </summary>
    public void UpdatePen()
    {
        // Update Player Inventory Variables
        for (int i = 0; i < _inventoryTextList.Count; i++)
        {
            ItemData itemData = AssetController.Instance.ItemAsset.ItemDataList[(int)Player.Instance.Inventory[i].Item];
            string itemDescription = itemData.Name + " x " + Player.Instance.Inventory[i].Amount.ToString();
            _inventoryTextList[i].text = itemDescription;
        }
        
        // Update Pen Items
        for (int i = 0; i < _UIPenItemList.Count; i++)
        {
            // Check if we have a llama in the pen.
            if (i < PenController.Instance.CapturedLlamaList.Count)
            {
                _UIPenItemList[i].gameObject.SetActive(true);
                Llama tempLlama = PenController.Instance.CapturedLlamaList[i];
                _UIPenItemList[i].UpdatePenItem(tempLlama.CurrentHealth, tempLlama.LowHealth, tempLlama.Age, tempLlama.Diet);
            }
            else
            {
                _UIPenItemList[i].gameObject.SetActive(false);
            }
        }
    }

    #endregion // Methods.
}
