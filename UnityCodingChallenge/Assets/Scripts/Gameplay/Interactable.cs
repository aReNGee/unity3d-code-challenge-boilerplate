#region Namespaces

using UnityEngine;

#endregion // Namespaces.

/// <summary>
///     General class that handles Interactables of different types.
///     When the interactable is clicked, Interact() is called.
///     Extend by adding new InteractableTypes and Interact() behaviors.
/// </summary>

public class Interactable : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public enum InteractableType
    {
        GeneralStore,
        Inventory,
        Pen
    }

    [SerializeField] private InteractableType _interactibleType;
    public InteractableType Type
    {
        get { return _interactibleType; }
    }

    #endregion // Variables.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    public void Interact()
    {
        if (_interactibleType == InteractableType.GeneralStore)
        {
            UIController.Instance.OpenGeneralStore();
        }
        else if (_interactibleType == InteractableType.Inventory)
        {
            UIController.Instance.OpenInventory();
        }
        else if (_interactibleType == InteractableType.Pen)
        {
            UIController.Instance.OpenPenMenu();
        }
    }

    #endregion // Methods.
}
