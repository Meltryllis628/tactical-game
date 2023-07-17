using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachGame;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace TachGame {
    internal class CharacterRendererData {
        private Texture2D spriteSheet; // Sprite Sheet 图片
        public int frameWidth = 64; // 每帧的宽度
        public int frameHeight = 64; // 每帧的高度
        private Sprite[] frames; // 存储分割后的 Sprite 帧
        public Sprite[] Frames { get { return frames; } }

        public CharacterRendererData(string path) {
            spriteSheet = LoadTextureFromFile(path);
            SplitFramesFromSpriteSheet();
        }

        
        


        private Texture2D LoadTextureFromFile(string filePath) {
            byte[] imageData;
            // 从磁盘加载图片数据
            imageData = System.IO.File.ReadAllBytes(filePath);

            // 创建一个新的 Texture2D 对象，并加载图片数据
            Texture2D textureLoad = new Texture2D(2, 2);
            textureLoad.LoadImage(imageData);


            return textureLoad;
        }
        private void SplitFramesFromSpriteSheet() {
            // 计算每行每列的帧数
            int rows = spriteSheet.height / frameHeight;
            int columns = spriteSheet.width / frameWidth;

            // 创建一个数组来存储分割后的帧
            frames = new Sprite[rows * columns];

            // 遍历每行每列，分割帧并存储到数组中
            for (int row = 0; row < rows; row++) {
                for (int column = 0; column < columns; column++) {
                    // 计算当前帧的 UV 坐标
                    Rect frameRect = new Rect(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);

                    // 从 Sprite Sheet 创建 Sprite 帧并存储到数组中
                    Sprite frame = Sprite.Create(spriteSheet, frameRect, pivot);
                    frames[row * columns + column] = frame;
                }
            }
        }
    }
}
