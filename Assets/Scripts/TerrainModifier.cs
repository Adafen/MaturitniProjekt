using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;

public class TerrainModifier : MonoBehaviour
{
    [Header("Building Settings")]
    [SerializeField] private Tilemap playerTilemap;
    [SerializeField] private TileBase blockToBuild;

    [Header("Inventory and UI")]
    [SerializeField] private int blocksLeft = 3;
    [SerializeField] private TextMeshProUGUI inventoryText;

    private Camera mainCamera;
    private bool isBuildModeActive = false;

    // Track which blocks were placed by the player to manage inventory correctly
    private HashSet<Vector3Int> playerPlacedBlocks = new HashSet<Vector3Int>();

    void Start()
    {
        mainCamera = Camera.main;
        UpdateUI();
    }

    void Update()
    {
        // Toggle build mode with the B key
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBuildModeActive = !isBuildModeActive;
            UpdateUI();
        }

        if (!isBuildModeActive) return;

        // Handle left-click for building and right-click for removing
        if (Input.GetMouseButtonDown(0))
        {
            ModifyTerrain(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ModifyTerrain(false);
        }
    }

    private void ModifyTerrain(bool isBuilding)
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3Int cellPos = playerTilemap.WorldToCell(mouseWorldPos);

        if (isBuilding)
        {
            // Logic for placing tiles
            if (blocksLeft > 0 && !playerTilemap.HasTile(cellPos))
            {
                playerTilemap.SetTile(cellPos, blockToBuild);
                blocksLeft--;

                // Track that the player placed this block
                playerPlacedBlocks.Add(cellPos);

                UpdateUI();
            }
        }
        else
        {
            // Logic for removing tiles
            if (playerTilemap.HasTile(cellPos))
            {
                bool didPlayerPlaceIt = playerPlacedBlocks.Contains(cellPos);

                // Remove the tile
                playerTilemap.SetTile(cellPos, null);

                // If the player placed this block, return it to their inventory
                if (didPlayerPlaceIt)
                {
                    blocksLeft++;
                    playerPlacedBlocks.Remove(cellPos);
                }

                UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        if (inventoryText != null)
        {
            string modeStatus = isBuildModeActive ? "<color=green>ON</color>" : "<color=red>OFF</color>";
            inventoryText.text = $"Build Mode: {modeStatus}\nBlocks Left: {blocksLeft}";
        }
    }
}