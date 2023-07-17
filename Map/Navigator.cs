using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TachGame {
    public class Navigator : UnitySingleton<Navigator> {
        // Start is called before the first frame update
        public Tilemap tilemap;
        public int ac = 4;
        private int clickedTime = 0;
        private int rows = 0;
        private int columns = 0;
        private int[] AcMap = null;
        private Vector3Int startCell = new Vector3Int(9999, 9999, 9999);
        private Vector3Int endCell = new Vector3Int(9999, 9999, 9999);
        private Vector3Int lastHoveredCell;
        private Vector3Int[] movingBoundary = null;
        private Vector3Int[] trace = null;
        private Vector3Int[,] tracer = null;
        private Color highlightColor = new Color(0.3f, 0.3f, 0.8f);
        private Color avaliableColor = new Color(0.4f, 0.7f, 0.3f);
        private Color cursorColor = new Color(1f, 0.3f, 0.3f);
        private Color traceColor = new Color(0.4f, 0.8f, 0.9f);
        private Color normalColor;
        private readonly Vector3Int[] surroundings = new Vector3Int[4]
    {
    new Vector3Int(0, 1, 0),
    new Vector3Int(1, 0, 0),
    new Vector3Int(0, -1, 0),
    new Vector3Int(-1, 0, 0)
    };



        private void HighlightSurroundingTiles(Vector3Int clickedCell, Color color) {
            // 循环遍历周围的格子
            for (int x = clickedCell.x - 1; x <= clickedCell.x + 1; x++) {
                for (int y = clickedCell.y - 1; y <= clickedCell.y + 1; y++) {
                    Vector3Int currentCell = new Vector3Int(x, y, clickedCell.z);
                    TileBase tile = tilemap.GetTile(currentCell);

                    // 检查格子是否存在瓦片
                    if (tile != null) {
                        // 设置格子的颜色滤镜为半透明红色
                        tilemap.SetTileFlags(currentCell, TileFlags.None);
                        tilemap.SetColor(currentCell, color);
                    }
                }
            }
        }
        private void HighlightTiles(Vector3Int clickedCell, Color color) {
            TileBase tile = tilemap.GetTile(clickedCell);

            // 检查格子是否存在瓦片
            if (tile != null) {
                // 设置格子的颜色滤镜为半透明红色
                tilemap.SetTileFlags(clickedCell, TileFlags.None);
                tilemap.SetColor(clickedCell, color);
            }
        }
        Vector3Int GetTracer(Vector3Int currentCell) { return tracer[rows / 2 + currentCell.x, columns / 2 + currentCell.y]; }
        void SetTracer(Vector3Int currentCell, Vector3Int targetCell) { tracer[rows / 2 + currentCell.x, columns / 2 + currentCell.y] = targetCell; }

        private Vector3Int[] findTrace(Vector3Int start, Vector3Int end) {
            Stack<Vector3Int> trace = new Stack<Vector3Int>();
            trace.Push(end);
            while (trace.Peek() != start) {
                trace.Push(GetTracer(trace.Peek()));
            }
            return trace.ToArray();
        }
        private Vector3Int[] SetMovingBoundary(Vector3Int startPos, int avaliableAC) {
            Queue<Vector3Int> openList = new Queue<Vector3Int>();
            Stack<Vector3Int> closeList = new Stack<Vector3Int>();
            int[,] restAc = new int[rows, columns];
            for (int i = 0; i < restAc.GetLength(0); i++) {
                for (int j = 0; j < restAc.GetLength(1); j++) {
                    restAc[i, j] = -1;
                }
            }
            tracer = new Vector3Int[rows, columns];
            int GetRestAc(Vector3Int currentCell) { return restAc[rows / 2 + currentCell.x, columns / 2 + currentCell.y]; }
            void SetRestAc(Vector3Int currentCell, int ac) { restAc[rows / 2 + currentCell.x, columns / 2 + currentCell.y] = ac; }

            openList.Enqueue(startPos);
            SetRestAc(startPos, avaliableAC);

            while (openList.Count > 0) {
                Vector3Int currentTile = openList.Dequeue();
                if (!closeList.Contains(currentTile)) {
                    closeList.Push(currentTile);
                }
                int currentRestAc = GetRestAc(currentTile) - GetTileAc(currentTile);
                if (currentRestAc < 0) {
                    continue;
                }
                foreach (Vector3Int diff in surroundings) {
                    Vector3Int newTile = currentTile + diff;
                    if (tilemap.HasTile(newTile)) {
                        if (currentRestAc > GetRestAc(newTile)) {
                            SetRestAc(newTile, currentRestAc);
                            SetTracer(newTile, currentTile);
                            if (!openList.Contains(newTile)) {
                                openList.Enqueue(newTile);
                                //Debug.Log(currentRestAc +", " + currentTile + ", " + newTile);
                                //debugOut(restAc);
                            }
                        }
                    }
                }
            }

            return closeList.ToArray();
        }

        private void debugOut(int[,] array2D) {
            int rows = array2D.GetLength(0);
            int columns = array2D.GetLength(1);
            string rowAsString = string.Empty;

            for (int j = columns - 1; j >= 0; j--) {


                for (int i = 0; i < rows; i++) {
                    rowAsString += array2D[i, j].ToString() + "  ";
                }
                rowAsString += "\n";

            }
            Debug.Log(rowAsString);
        }
        private int GetTileAc(Vector3Int currentCell) {
            return AcMap[rows / 2 + currentCell.x + columns / 2 + currentCell.y];
        }
        void Start() {
            normalColor = tilemap.color;
            if (AcMap == null) {
                //AcMap = MapRendererManager.Instance.GetAcMap();
                //rows = AcMap.GetLength(0);
                //columns = AcMap.GetLength(1);
            }


        }
        private void OnDisable() {
            // 禁用脚本时恢复最后悬停格子的颜色
            if (tilemap.HasTile(startCell)) {
                HighlightTiles(startCell, normalColor);
            }
            if (tilemap.HasTile(endCell)) {
                HighlightTiles(endCell, normalColor);
            }
            if (tilemap.HasTile(lastHoveredCell)) {
                HighlightTiles(lastHoveredCell, normalColor);
            }
        }

        public override void UpdateMessage(Message currentMessage) {
            throw new NotImplementedException();
        }

        public override void UpdateElse() {
            if (AcMap == null) {
                AcMap = MapManager.Instance.ACMap;
                //rows = AcMap.GetLength(0);
                //columns = AcMap.GetLength(1);
            }


            // 将鼠标点击位置转换为 Tilemap 上的世界坐标
            Vector3 mouseWorldPos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int hoveredCell = tilemap.WorldToCell(mouseWorldPos1);


            if (Input.GetMouseButtonDown(0)) {
                // 将鼠标点击位置转换为 Tilemap 上的世界坐标
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickedCell = tilemap.WorldToCell(mouseWorldPos);
                if (clickedTime == 0 && tilemap.HasTile(clickedCell)) {
                    startCell = clickedCell;
                    clickedTime++;

                    Debug.Log("Navi: startCell " + startCell + " " + GetTileAc(startCell));
                    movingBoundary = SetMovingBoundary(startCell, ac);
                    //Debug.Log(string.Join(", ",SetMovingBoundary(startCell, ac)));
                }
                if (clickedTime == 1 && clickedCell != startCell && tilemap.HasTile(clickedCell)) {
                    if (movingBoundary.Contains(clickedCell)) {
                        endCell = clickedCell;
                        clickedTime++;
                        trace = findTrace(startCell, endCell);
                        Debug.Log("Navi: endCell " + endCell);
                    }
                }
            }

            if (movingBoundary != null) {
                foreach (var cell in movingBoundary) {
                    HighlightTiles(cell, avaliableColor);
                }
            }

            if (hoveredCell != lastHoveredCell) {
                // 恢复上一个格子的颜色
                if (tilemap.HasTile(lastHoveredCell) && lastHoveredCell != startCell && lastHoveredCell != endCell) {
                    HighlightTiles(lastHoveredCell, normalColor);
                }

                // 高亮新的格子
                if (tilemap.HasTile(hoveredCell) && hoveredCell != startCell && hoveredCell != endCell) {
                    if (movingBoundary != null && clickedTime == 1) {
                        if (movingBoundary.Contains(hoveredCell)) {
                            trace = findTrace(startCell, hoveredCell);
                            //Debug.Log(string.Join(", ", trace));
                        }
                    }

                }
            }
            if (trace != null) {
                foreach (var cell in trace) {
                    HighlightTiles(cell, traceColor);
                }
            }
            if (clickedTime >= 1) {
                HighlightTiles(startCell, highlightColor);
            }
            if (clickedTime >= 2) {
                HighlightTiles(endCell, highlightColor);
            }
            HighlightTiles(hoveredCell, cursorColor);
            lastHoveredCell = hoveredCell;

            if (Input.GetMouseButtonDown(1)) {
                if (movingBoundary != null) {
                    foreach (var cell in movingBoundary) {
                        HighlightTiles(cell, normalColor);
                    }
                }
                if (trace != null) {
                    foreach (var cell in trace) {
                        HighlightTiles(cell, normalColor);
                    }
                }
                HighlightTiles(startCell, normalColor);
                HighlightTiles(endCell, normalColor);
                startCell = new Vector3Int(9999, 9999, 9999);
                endCell = new Vector3Int(9999, 9999, 9999);
                movingBoundary = null;
                trace = null;
                clickedTime = 0;
            }
        }
    }

}

