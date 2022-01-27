#region Namespaces

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion // Namespaces.

/// <summary>
///     Class that represents a single llama in the pen visually.
///     Displays that llama's health, age, and diet.
///     Clicking on the object will attempt to feed the llama.
/// </summary>

public class PenItem : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    private Image _image;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _ageText;
    [SerializeField] private TextMeshProUGUI _dietText;

    // The index of the llama in the PenController's list.
    private int _associatedIndex = 0;

    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods
    
    /// <summary>
    ///     Method that stores a reference to the associated llama's location in the list.
    /// </summary> 
    public void Setup(int index)
    {
        _associatedIndex = index;
    }

    /// <summary>
    ///     Method that attempts to feed the associated llama.
    ///     Called when the object is clicked on.
    /// </summary> 
    public void FeedLlama()
    {
        PenController.Instance.FeedLlama(_associatedIndex);
    }

    /// <summary>
    ///     Method that updates the color and text values of the object.
    ///     Called every couple seconds by UIPen.
    /// </summary> 
    public void UpdatePenItem(int health, bool lowHealth, int age, Llama.DietType dietType)
    {
        if (lowHealth)
        {
            _image.color = AssetController.Instance.LlamaAsset.PenItemWarningColor;
        }
        else
        {
            _image.color = AssetController.Instance.LlamaAsset.PenItemSafeColor;
        }

        _healthText.text = "Health: " + health.ToString();
        _ageText.text = "Age: " + age.ToString();
        _dietText.text = "Diet: " + dietType.ToString();
    }

    #endregion // Methods.
}
