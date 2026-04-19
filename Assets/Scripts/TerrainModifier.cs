using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;

public class TerrainModifier : MonoBehaviour
{
    [SerializeField] private Tilemap playerTilemap;
    [SerializeField] private TileBase blockToBuild;
    [SerializeField] private float placementDelay = 1.0f;

    [SerializeField] private int blocksLeft = 3;
    [SerializeField] private TextMeshProUGUI inventoryText;

    private Camera mainCamera;
    private bool isBuildModeActive = false;
    private float lastDeleteTime = -100f;

    // Track which blocks were placed by the player to manage inventory correctly
    private HashSet<Vector3Int> playerPlacedBlocks = new HashSet<Vector3Int>();

    void Start()
    {
        mainCamera = Camera.main;
        UpdateUI();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        // Toggle build mode with the B key
        if (Input.GetKeyDown(InputManager.BuildMode))
        {
            isBuildModeActive = !isBuildModeActive;
            UpdateUI();
        }

        if (!isBuildModeActive) return;

        // Handle left-click for building and right-click for removing
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= lastDeleteTime + placementDelay)
            {
                ModifyTerrain(true);
            }
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
            // Check if the cell is empty and if the player has blocks left
            bool isCellEmpty = !playerTilemap.HasTile(cellPos);
            Vector2 cellWorldPos = playerTilemap.GetCellCenterWorld(cellPos);
            Collider2D playerOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Player"));
            Collider2D enemyOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Enemy"));
            Collider2D groundOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Ground"));
            Collider2D wallOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Wall"));

            // Check if the player has blocks left and the cell is empty, and there are no overlaps with important layers
            if (blocksLeft > 0 && isCellEmpty && playerOverlap == null && enemyOverlap == null && groundOverlap == null && wallOverlap == null)
            {
                playerTilemap.SetTile(cellPos, blockToBuild);
                blocksLeft--;

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
                    lastDeleteTime = Time.time;
                }

                UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        if (inventoryText != null)
        {
            string modeStatus = isBuildModeActive ? "ON" : "OFF";
            inventoryText.text = $"Build Mode: {modeStatus}\nBlocks Left: {blocksLeft}";
        }
    }
}