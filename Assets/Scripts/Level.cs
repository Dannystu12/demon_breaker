using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] int breakableBlocks; // Serialized for debugging

    // Cached reference
    SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockBroken()
    {
       breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
