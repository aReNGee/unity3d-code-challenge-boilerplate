using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{    
    /// <summary>
    ///     Asserts that a llama cannot be captured once in the pen.
    /// </summary>
    [UnityTest]
    public IEnumerator CannotCaptureAPennedLlama()
    {
        GameObject assetController = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Asset Controller"));
        GameObject llamaGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PoolLlama"));
        Llama llama = llamaGameObject.GetComponent<Llama>();
        llama.Setup();
        llama.MoveToPen();

        yield return null;

        // Attempt to capture the penned llama.
        Assert.AreEqual(false, llama.CaptureLlama());

        Object.Destroy(llamaGameObject);
        Object.Destroy(assetController);
    }

    /// <summary>
    ///     Asserts that a llama cannot exceed its maximum health.
    /// </summary>
    [UnityTest]
    public IEnumerator LlamaFeedingCannotExceedMaxHealth()
    {
        GameObject assetController = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Asset Controller"));
        GameObject llamaGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PoolLlama"));
        Llama llama = llamaGameObject.GetComponent<Llama>();
        llama.Setup();
        llama.MoveToPen();

        int maxHealth = llama.MaxHealth;
        llama.Feed();

        yield return null;

        Assert.AreEqual(maxHealth, llama.CurrentHealth);

        Object.Destroy(llamaGameObject);
        Object.Destroy(assetController);
    }

    /// <summary>
    ///     Asserts that a llama health decreases over time.
    /// </summary>
    [UnityTest]
    public IEnumerator LlamaHealthDecrementsWhileCaptured()
    {
        GameObject assetController = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Asset Controller"));
        GameObject llamaGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PoolLlama"));
        Llama llama = llamaGameObject.GetComponent<Llama>();
        llama.Setup();
        llama.MoveToPen();

        int initialHealth = llama.CurrentHealth;

        // Wait for the llama's health to decrease.
        yield return new WaitForSeconds(4f);

        Assert.Less(llama.CurrentHealth, initialHealth);

        Object.Destroy(llamaGameObject);
        Object.Destroy(assetController);
    }
}
