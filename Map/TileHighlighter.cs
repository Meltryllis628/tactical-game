
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    // Start is called before the first frame update
    private Tilemap tilemap;
    private Vector3Int lastHoveredCell;
    private Color highlightColor = new Color(1f, 0.3f, 0.3f);
    private Color normalColor;
    

    private void Update()
    {
        // 将鼠标点击位置转换为 Tilemap 上的世界坐标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int hoveredCell = tilemap.WorldToCell(mouseWorldPos);

        // 判断鼠标是否悬停在瓦片上
        if ( hoveredCell != lastHoveredCell)
        {
            // 恢复上一个格子的颜色
            if (tilemap.HasTile(lastHoveredCell))
            {
                HighlightTiles(lastHoveredCell, normalColor);
            }

            // 高亮新的格子
            if (tilemap.HasTile(hoveredCell))
            {
                HighlightTiles(hoveredCell, highlightColor);
            }
        }
        lastHoveredCell = hoveredCell;
    }

    private void HighlightSurroundingTiles(Vector3Int clickedCell, Color color)
    {
        // 循环遍历周围的格子
        for (int x = clickedCell.x - 1; x <= clickedCell.x + 1; x++)
        {
            for (int y = clickedCell.y - 1; y <= clickedCell.y + 1; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, clickedCell.z);
                TileBase tile = tilemap.GetTile(currentCell);

                // 检查格子是否存在瓦片
                if (tile != null)
                {
                    // 设置格子的颜色滤镜为半透明红色
                    tilemap.SetTileFlags(currentCell, TileFlags.None);
                    tilemap.SetColor(currentCell, color);
                }
            }
        }
    }
    private void HighlightTiles(Vector3Int clickedCell, Color color)
    {
        TileBase tile = tilemap.GetTile(clickedCell);

        // 检查格子是否存在瓦片
        if (tile != null)
        {
            // 设置格子的颜色滤镜为半透明红色
            tilemap.SetTileFlags(clickedCell, TileFlags.None);
            tilemap.SetColor(clickedCell, color);
        }
    }
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        normalColor = tilemap.color;
    }
    private void OnDisable()
    {
        // 禁用脚本时恢复最后悬停格子的颜色
        if (tilemap.HasTile(lastHoveredCell))
        {
            HighlightTiles(lastHoveredCell, normalColor);
        }
    }

}
