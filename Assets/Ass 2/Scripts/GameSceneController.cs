using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public Player player;
    public Camera gameCamera;
    public GameObject[] blockPrefabs;
    
    private float blockPointer;
    private float safeSpace = 20;
    private int blocksGenerated = 0;
    private bool hasError = false;
    
    void Start()
    {
        // Validate all references
        ValidateReferences();
        
        if (hasError) return;
        
        // Set starting position based on player
        if (player != null)
        {
            blockPointer = player.transform.position.x - 5;
            Debug.Log($"Starting blockPointer: {blockPointer}");
            
            // Generate first block immediately
            GenerateBlocks();
        }
    }
    
    void ValidateReferences()
    {
        hasError = false;
        
        if (player == null)
        {
            Debug.LogError("❌ PLAYER REFERENCE MISSING: Drag Player object to the player field in Inspector!");
            hasError = true;
        }
        
        if (gameCamera == null)
        {
            Debug.LogError("❌ CAMERA REFERENCE MISSING: Drag Main Camera to the gameCamera field in Inspector!");
            hasError = true;
        }
        
        if (blockPrefabs == null || blockPrefabs.Length == 0)
        {
            Debug.LogError("❌ BLOCK PREFABS ARRAY IS EMPTY: Set array size and assign prefabs in the Inspector!");
            hasError = true;
        }
        else
        {
            Debug.Log($"✅ Found {blockPrefabs.Length} block prefabs in array");
            
            // Check each prefab
            for (int i = 0; i < blockPrefabs.Length; i++)
            {
                if (blockPrefabs[i] == null)
                {
                    Debug.LogError($"❌ BLOCK PREFAB AT INDEX {i} IS NULL: Remove this slot or assign a prefab!");
                    hasError = true;
                }
                else
                {
                    Debug.Log($"✅ Prefab {i}: {blockPrefabs[i].name} is valid");
                    
                    // Check if prefab has Block component
                    Block testBlock = blockPrefabs[i].GetComponent<Block>();
                    if (testBlock == null)
                    {
                        Debug.LogError($"❌ PREFAB '{blockPrefabs[i].name}' IS MISSING BLOCK COMPONENT!");
                        hasError = true;
                    }
                    else
                    {
                        Debug.Log($"✅ Prefab '{blockPrefabs[i].name}' has Block component with size: {testBlock.size}");
                    }
                }
            }
        }
    }
    
    void Update()
    {
        if (hasError || player == null) return;
        
        // Move camera
        if (gameCamera != null)
        {
            gameCamera.transform.position = new Vector3(
                player.transform.position.x,
                gameCamera.transform.position.y,
                gameCamera.transform.position.z
            );
        }
        
        // Generate blocks
        GenerateBlocks();
    }
    
    void GenerateBlocks()
    {
        if (blockPrefabs == null || blockPrefabs.Length == 0) return;
        
        int safetyCounter = 0;
        float playerX = player.transform.position.x;
        
        while (blockPointer <= playerX + safeSpace)
        {
            safetyCounter++;
            if (safetyCounter > 20) 
            {
                Debug.LogError("Infinite loop detected! Breaking.");
                break;
            }
            
            // Select random prefab
            int randomIndex = Random.Range(0, blockPrefabs.Length);
            GameObject selectedPrefab = blockPrefabs[randomIndex];
            
            if (selectedPrefab == null)
            {
                Debug.LogError($"Prefab at index {randomIndex} is null!");
                continue;
            }
            
            blocksGenerated++;
            Debug.Log($"Generating block {blocksGenerated} from prefab: {selectedPrefab.name}");
            
            // Instantiate the block
            GameObject blockObject = Instantiate(selectedPrefab);
            blockObject.transform.SetParent(this.transform);
            
            // Get Block component
            Block block = blockObject.GetComponent<Block>();
            if (block == null)
            {
                Debug.LogError($"Prefab {selectedPrefab.name} is missing Block component!");
                Destroy(blockObject);
                continue;
            }
            
            // Position the block
            float blockX = blockPointer + block.size/2;
            blockObject.transform.position = new Vector3(blockX, 0, 0);
            
            Debug.Log($"Block placed at X: {blockX}, size: {block.size}");
            
            blockPointer += block.size;
        }
    }
    
    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector3(player.transform.position.x + safeSpace, -10, 0), 
                           new Vector3(player.transform.position.x + safeSpace, 10, 0));
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(blockPointer, -10, 0), 
                           new Vector3(blockPointer, 10, 0));
        }
    }
}