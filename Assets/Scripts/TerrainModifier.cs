using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainModifier : MonoBehaviour
{
    [Header("Building settings")]
    [SerializeField] private Tilemap playerTilemap;
    [SerializeField] private TileBase blockToBuild;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button click to build and right mouse button click to destroy
        if (Input.GetMouseButtonDown(0))
        {
            ModifyTerrain(true);
        }

        // Check for right mouse button click to destroy
        if (Input.GetMouseButtonDown(1))
        {
            ModifyTerrain(false);
        }
    }
    // Method to modify the terrain based on the mouse position and action (build or destroy)
    private void ModifyTerrain(bool isBuilding)
    {
        // Get the mouse position in screen coordinates
        Vector3 mouseScreenPosition = Input.mousePosition;
        // Convert the mouse position to world coordinates
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        // Set the z-coordinate to 0 to ensure we are working in the 2D plane
        mouseWorldPosition.z = 0;
        // Get the cell position in the tilemap based on the mouse world position
        Vector3Int cellPosition = playerTilemap.WorldToCell(mouseWorldPosition);
        // Modify the tile at the cell position based on the action (build or destroy)
        if (isBuilding)
        {
            playerTilemap.SetTile(cellPosition, blockToBuild);
        }
        else
        {
            playerTilemap.SetTile(cellPosition, null);
        }
    }
}