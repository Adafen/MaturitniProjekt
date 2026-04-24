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
    public TilemapRenderer gridVisualRenderer;
    // Track which blocks were placed by the player to manage inventory correctly
    private HashSet<Vector3Int> playerPlacedBlocks = new HashSet<Vector3Int>();

    void Start()
    {
        mainCamera = Camera.main;
        gridVisualRenderer.enabled = false;
        UpdateUI();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        // Toggle build mode on key press
        if (Input.GetKeyDown(InputManager.BuildMode))
        {
            isBuildModeActive = !isBuildModeActive;
            gridVisualRenderer.enabled = isBuildModeActive;
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
        // Convert mouse position to world position and then to tilemap cell position
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3Int cellPos = playerTilemap.WorldToCell(mouseWorldPos);

        if (isBuilding)
        {
            // Logic for placing tiles
            bool isCellEmpty = !playerTilemap.HasTile(cellPos);

            Vector2 cellWorldPos = playerTilemap.GetCellCenterWorld(cellPos);
            Collider2D playerOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Player"));
            Collider2D enemyOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Enemy"));
            Collider2D wallOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Wall"));
            Collider2D hazardOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Hazard"));
            Collider2D groundOverlap = Physics2D.OverlapBox(cellWorldPos, new Vector2(0.8f, 0.8f), 0, LayerMask.GetMask("Ground"));

            // Check if the player has blocks left and the cell is empty, and there are no overlaps with important layers
            if (blocksLeft > 0 && isCellEmpty && playerOverlap == null && enemyOverlap == null && wallOverlap == null && hazardOverlap == null && groundOverlap == null)
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