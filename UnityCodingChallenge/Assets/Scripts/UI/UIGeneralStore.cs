#region Namespaces

#endregion // Namespaces.

/// <summary>
///     Class that extends the base UIStore clasee.
///     Includes custom functoinality for its close button.
/// </summary>

public class UIGeneralStore : UIStore
{
    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Called when the close button is clicked on.
    ///     Calls the appropriate close method in the UIController.
    /// </summary> 
    public void CloseStoreUI()
    {
        UIController.Instance.CloseGeneralStore();
    }

    #endregion // Methods.
}
