using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayModeTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayModeTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayModeTestScriptWithEnumeratorPasses()
    {
        // Target scene name
        string sName = "SampleScene";

        // Timeout for setting active scene 
        int timeoutCounter = 0;
        int timeoutCounterThreshold = 10;

        // Load the target scene
        SceneManager.LoadScene(sName,LoadSceneMode.Additive);
        // Skip a frame to wait for the scene to be loaded
        yield return null;

        // Get a reference to the target scene
        Scene testScene = SceneManager.GetSceneByName(sName);
        
        if (testScene.IsValid())
        {
            if (testScene.isLoaded)
            {
                while (!SceneManager.SetActiveScene(testScene))
                {
                    Debug.Log($"In the loop: {timeoutCounter}");
                    timeoutCounter++;
                    if (timeoutCounter == timeoutCounterThreshold)
                    {
                        Assert.Fail($"Timeout: loading {testScene.name} ");
                    }    
                        
                    // Suspend coroutine for a few seconds
                    yield return new WaitForSeconds(1);
                }

                Assert.Pass($"{sName} is loaded");
            }
        }
        else
        {
            Assert.Fail($"{sName} does not exist. Existing scene is {SceneManager.GetActiveScene().name}");
        }   
    }
}
