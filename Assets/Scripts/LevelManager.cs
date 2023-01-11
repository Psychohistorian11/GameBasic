using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static LevelManager sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPosition;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        GenerateInitialBLock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock()
    {
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count);
        LevelBlock block;
        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0) 
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }

        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x, spawnPosition.y - block.startPoint.position.y,0);
        block.transform.localPosition = correction;
        currentLevelBlocks.Add(block);
    }

    public void RemoveLevelBlock()
    {

    }

    public void RemoveAllLevelBlock()
    {

    }

    public void GenerateInitialBLock()
    {
        for(int i= 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
 
