#region Namespaces

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


#endregion // Namespaces.

/// <summary>
///     Class that handles all of the player interaction in the scene.
///     This includes moving the player and clicking on interactable objects.
/// </summary>
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables

    public static Player Instance;

    private Camera _mainCamera;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    // The player will stop its walk animation once it is this close to its destination.
    private const float STOPPING_DISTANCE = 0.4f;
    
    // Required Player variables
    private int _health = 0;
    private int _coins = 0;
    public int Coins
    {
        get { return _coins; }
    }
    [SerializeField] private List<InventoryItem> _inventory = new List<InventoryItem>();
    public List<InventoryItem> Inventory
    {
        get { return _inventory; }
    }

    // The amount of coins we get for picking up a single Coin object.
    private const int COIN_VALUE = 5;

    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Awake()
    {
        // Singleton setup.
        if (Instance == null)
        {
            // Make the current instance as the singleton.
            Instance = this;
        }
        else
        {
            // If more than one singleton exists in the scene find the existing reference from the scene and destroy it.
            if (this != Instance)
            {
                Destroy(gameObject);
                return;
            }
        }
        // End of singleton setup.

        _mainCamera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();

        ResetInventory();
    }

    private void Update()
    {
        // If the player is within a certain distance threshold to the end of its path, stop the walk animation.
        if (_navMeshAgent.remainingDistance <= STOPPING_DISTANCE)
        {
            _animator.SetBool("Walking", false);
        }

        HandlePlayerInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Llama llama = collision.gameObject.GetComponent<Llama>();
        Coin coin = collision.gameObject.GetComponent<Coin>();

        // Capture the llama.
        if (llama)
        {
            llama.CaptureLlama();    
            // Play an animation of capturing the llama.
            _animator.SetTrigger("Capture");
        }

        // Collect the coins.
        if (coin)
        {
            _coins += COIN_VALUE;
            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.CollectMoney);
            coin.ReturnToPool();
        }
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that handles the player's input through mouse clicks (left button only).
    ///     Won't accept any input while a UI screen is open.
    ///     
    ///     Uses a raycast to look for Target or Interactable objects.
    ///     If it finds an interactable object, interact with it.
    ///     Otherwise, if a target is found, set the click location as the player's destination.
    /// </summary>
    private void HandlePlayerInput()
    {
        // Don't accept player input while an inventory screen is open.
        if (UIController.Instance.IsAUIScreenOpen) return;

        if (!Input.GetMouseButtonDown(0)) return;

        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        // The player can click on objects that are on the "Target" and "Interactable" layers.
        LayerMask mask = LayerMask.GetMask("Target", "Interactable");

        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            Interactable interactable = hit.transform.gameObject.GetComponent<Interactable>();

            if (interactable)
            {
                // The player clicked on an interactable object, interact with it..
                interactable.Interact();
                // Stop the player moving while they interact.
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                // The player clicked on the ground.
                // Start the player's walk animation.
                _animator.SetBool("Walking", true);
                _navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    /// <summary>
    ///     Method that uses one of the players inventory item.
    ///     The index is a converted ItemType enum.
    /// </summary>
    public void UseInventoryItem(int index, int amountUsed)
    {
        _inventory[index].Amount -= amountUsed;
        if (_inventory[index].Amount < 0) _inventory[index].Amount = 0;
    }

    /// <summary>
    ///     Method that attempts to buy an item.
    ///     If the player has enough coins, they are deducted and the item is added to the player's inventory.
    /// </summary>
    public void BuyItem(ItemData.ItemType itemToBuy)
    {
        ItemData itemData = AssetController.Instance.ItemAsset.ItemDataList[(int)itemToBuy];

        if (_coins >= itemData.Price)
        {
            _coins -= itemData.Price;
            if (_coins < 0) _coins = 0;

            _inventory[(int)itemToBuy].Amount++;
            // Play sound effect.
            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.ShopPurchaseSuccessful);
        }
        else
        {
            // Play failure sound effect.
            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.ShopPurchaseFail);
        }
    }

    /// <summary>
    ///     Method called on Awake() to reset the player's inventory to base values.
    /// </summary>
    private void ResetInventory()
    {
        _inventory = new List<InventoryItem>();
        _inventory.Add(new InventoryItem(ItemData.ItemType.Grass, 0));
        _inventory.Add(new InventoryItem(ItemData.ItemType.Flower, 0));
        _inventory.Add(new InventoryItem(ItemData.ItemType.Shrub, 0));
    }

    #endregion // Methods.
}
