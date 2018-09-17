using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] AudioClip breakSound;
    [SerializeField] AudioClip crackSound;
    [SerializeField] GameObject blockBreakVFX;
    [SerializeField] Sprite[] hitSprites;

    Level level;

    [SerializeField] int numHits; // Serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level = FindObjectOfType<Level>();
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        numHits++;
        int maxHits = hitSprites.Length;
        if (numHits > maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = numHits - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
            AudioSource.PlayClipAtPoint(crackSound, Camera.main.transform.position);
        }
        else
        {
            Debug.Log("Block sprite is missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        level.BlockBroken();
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        TriggerBreakVFX();
        Destroy(gameObject);
    }

    private void TriggerBreakVFX()
    {
        ParticleSystem.MainModule settings = blockBreakVFX.GetComponent<ParticleSystem>().main;
        settings.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        GameObject vfx = Instantiate(blockBreakVFX, transform.position, transform.rotation);
        Destroy(vfx, 2f);
        
    }
}
