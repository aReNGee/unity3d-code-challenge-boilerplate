#region Namespaces

using UnityEngine;
using UnityEngine.AI;

#endregion // Namespaces.

/// <summary>
///     Class for the Llama pooled object.
///     Handles the majority of the operation of the llama, including capture, feeding, and what happens to it in the pen.
///     
///     Once spawned, llamas will wander around until captured.
///     Once captured and in the pen, llamas will constantly lose health.
///     Once their health reaches zero, llamas will die.
///     You can feed llamas to restore their health.
/// </summary>
[DisallowMultipleComponent]
public class Llama : MonoBehaviour
{
    // ########################################
    // Variables.
    // ########################################

    #region Variables
    
    // Enum that handles the llama's preferred diet.
    public enum DietType
    {
        Grass,
        Flower,
        Shrub
    }

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    // The player will stop its walk animation once it is this close to its destination.
    private const float STOPPING_DISTANCE = 0.4f;
    
    // Required Llama variables    
    [SerializeField] private int _maxHealth = 0;
    [SerializeField] private int _age = 0;
    [SerializeField] private DietType _dietType = DietType.Flower;

    // Control Variables
    [SerializeField] private int _currentHealth = 0;
    [SerializeField] private bool _inPen = false;
    [SerializeField] private bool _belowTwentyPercentHealth = false;
    [SerializeField] private float _penTimer = 0f;

    // Reference to the child object so we're not changing the size of our actual gameobject.
    // Only a visual change.
    [SerializeField] private Transform _llamaMeshTransform = null;

    //Used to indicate that a llama is low health.
    [SerializeField] private GameObject _lowHealthWarning = null;

    private const float MAX_PEN_TIMER = 3F;
    private const int FEEDING_RECOVERY_AMOUNT = 20;

    // Public Accessors
    public int MaxHealth
    {
        get { return _maxHealth; }
    }
    public int CurrentHealth
    {
        get { return _currentHealth; }
    }
    public int Age
    {
        get { return _age; }
    }
    public DietType Diet
    {
        get { return _dietType; }
    }
    public bool LowHealth
    {
        get { return _belowTwentyPercentHealth; }
    }


    #endregion // Variables.

    // ########################################
    // MonoBehaviour Methods.
    // ########################################

    #region MonoBehaviour

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Unpenned llamas wander.
    // Penned llamas lose health over time.
    private void Update()
    {     
        if (_inPen)
        {
            _penTimer -= Time.deltaTime;
            if (_penTimer <= 0)
            {
                _currentHealth--;
                
                // Check if the Llama is dead or below 20% health.
                if (_currentHealth <= 0)
                {
                    // The Llama has died
                    PenController.Instance.ReleaseLlama(this);
                    ReturnToPool();
                }
                else if ((float)_currentHealth/(float)_maxHealth <= 0.2f)
                {
                    // Use a boolean to ensure we don't activate the low health notification multiple times.
                    if (!_belowTwentyPercentHealth)
                    {
                        _belowTwentyPercentHealth = true;
                        _lowHealthWarning.SetActive(true);
                    }
                }
                else
                {
                    // Use a boolean to ensure we don't disable the low health notification multiple times.
                    if (_belowTwentyPercentHealth)
                    {
                        _belowTwentyPercentHealth = false;
                        _lowHealthWarning.SetActive(false);
                    }
                }
                _penTimer = MAX_PEN_TIMER;
            }
        }
        else
        {
            // Llama's wander around if they're not in the pen.
            // If the llama is within a certain distance threshold to the end of its path, find a new path.
            if (_navMeshAgent.remainingDistance <= STOPPING_DISTANCE)
            {
                _navMeshAgent.SetDestination(GetRandomPosition());
            }
        }
    }

    #endregion // MonoBehaviour Methods.

    // ########################################
    // Methods.
    // ########################################

    #region Methods

    /// <summary>
    ///     Method that handles setup when the llama is taken out of the pool.
    ///     Randomizes the llama's age, health, coloring, and diet.
    ///     The llamas size will change depending on its age.
    /// </summary>
    public void Setup()
    {
        // Randomize starting variables
        _maxHealth = Random.Range(50, 101);
        _currentHealth = _maxHealth;
        _age = Random.Range(1, 101);
        _dietType = (DietType)Random.Range(0, 3);
        _inPen = false;
        _belowTwentyPercentHealth = false;
        _penTimer = MAX_PEN_TIMER;
        _lowHealthWarning.SetActive(false);

        // Use the llama's age to set its size. 
        _llamaMeshTransform.localScale = Vector3.one * (0.5f + ((float)_age / 100f));

        // Set the llama's coloring based on its diet.
        _skinnedMeshRenderer.material = AssetController.Instance.LlamaAsset.LlamaMaterialList[(int)_dietType];

        // Randomize the llama's starting position.
        transform.position = GetRandomPosition();
        // Start wandering.
        if (_navMeshAgent.isActiveAndEnabled && _navMeshAgent.isOnNavMesh){
            _navMeshAgent.SetDestination(GetRandomPosition());
        }
    }

    /// <summary>
    ///     Method that restores health to the llama equal to its FEEDING_RECOVERY_AMOUNT.
    ///     The llama's current health can't exceed its maximum health.
    /// </summary>
    public void Feed()
    {
        _currentHealth += FEEDING_RECOVERY_AMOUNT;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }

    /// <summary>
    ///     Method that attempts to capture a llama.
    ///     Llama's already in the pen can't be caputred.
    ///     Called when the player collides with a llama.
    public bool CaptureLlama()
    {
        // You can't recapture llamas in the pen.
        if (_inPen) return false;

        _inPen = true;
        // Stop the llama wandering around.
        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;

        if (PenController.Instance.CapturedLlamaList.Count < 15)
        {
            // Spawn some coins around the llama.
            WorldController.Instance.SpawnCoins(Random.Range(3, 9), transform.position);
            // Add the llama to the captured llama list in PenController and get a pen position to store the llama in the pen.
            Vector3 penPosition = PenController.Instance.CaptureLlama(this);
            _navMeshAgent.Warp(penPosition);
        }
        else
        {
            // The pen can only hold 15 llamas, just return this to the pool.
            ReturnToPool();
        }

        // Play a teleport Sound effect.
        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffectType.CaptureLlama);

        // Replace the llama we just captured after a delay.
        WorldController.Instance.ReplaceCapturedLlama();

        return true;
    }

    /// <summary>
    ///     Method that finds a random position that is inside the edge of the world but outside the central town.
    ///     The edge of the world is approximately at (-95, 95) for top left and (93, -93) for bottom right.
    ///     The main town area is approximately contained within (-28,20) for top left and (23, -10) for bottom right.
    ///     We'll simplify the bounds to reduce calculations.
    ///     
    ///     Returns a random position such that the llamas spawn outside the town area but still inside the edge of the world.
    /// </summary>
    public Vector3 GetRandomPosition()
    {
        float xLocation = Random.Range(28f, 93f);
        float zLocation = Random.Range(20f, 93f);
        if (Random.value > 0.5f)
        {
            xLocation *= -1;
        }
        if (Random.value > 0.5f)
        {
            zLocation *= -1;
        }
        return new Vector3(xLocation, 1f, zLocation);
    }

    /// <summary>
    ///     Method that returns the llama to the pool.
    /// </summary>
    private void ReturnToPool()
    {
        PoolController.Instance.ReturnPooledObject(PoolController.PoolType.Llama, gameObject);
    }

    #if UNITY_EDITOR
        
    /// <summary>
    ///     Editor only method used for unit testing.
    ///     Moves the llama directly to the pen.
    /// </summary>
    public void MoveToPen()
    {
        _inPen = true;
    }

    #endif

    #endregion // Methods.

    
}
