
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
        // �������λ��ת��Ϊ Tilemap �ϵ���������
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int hoveredCell = tilemap.WorldToCell(mouseWorldPos);

        // �ж�����Ƿ���ͣ����Ƭ��
        if ( hoveredCell != lastHoveredCell)
        {
            // �ָ���һ�����ӵ���ɫ
            if (tilemap.HasTile(lastHoveredCell))
            {
                HighlightTiles(lastHoveredCell, normalColor);
            }

            // �����µĸ���
            if (tilemap.HasTile(hoveredCell))
            {
                HighlightTiles(hoveredCell, highlightColor);
            }
        }
        lastHoveredCell = hoveredCell;
    }

    private void HighlightSurroundingTiles(Vector3Int clickedCell, Color color)
    {
        // ѭ��������Χ�ĸ���
        for (int x = clickedCell.x - 1; x <= clickedCell.x + 1; x++)
        {
            for (int y = clickedCell.y - 1; y <= clickedCell.y + 1; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, clickedCell.z);
                TileBase tile = tilemap.GetTile(currentCell);

                // �������Ƿ������Ƭ
                if (tile != null)
                {
                    // ���ø��ӵ���ɫ�˾�Ϊ��͸����ɫ
                    tilemap.SetTileFlags(currentCell, TileFlags.None);
                    tilemap.SetColor(currentCell, color);
                }
            }
        }
    }
    private void HighlightTiles(Vector3Int clickedCell, Color color)
    {
        TileBase tile = tilemap.GetTile(clickedCell);

        // �������Ƿ������Ƭ
        if (tile != null)
        {
            // ���ø��ӵ���ɫ�˾�Ϊ��͸����ɫ
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
        // ���ýű�ʱ�ָ������ͣ���ӵ���ɫ
        if (tilemap.HasTile(lastHoveredCell))
        {
            HighlightTiles(lastHoveredCell, normalColor);
        }
    }

}
