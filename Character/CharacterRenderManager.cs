using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TachGame {
    internal enum AnimateStage {
        IdleLR = 100,
        WalkLR = 101,
        IdleF = 200,
        WalkF = 201,
        IdleB = 300,
        WalkB = 301,
    }
    internal class CharacterRenderManager : UnitySingleton<CharacterRenderManager> {

        public SpriteRenderer spriteRenderer; // Sprite 渲染器组件
        
        public float frameRate = 3; // 动画帧率
        public Tilemap groundMap = null;
        public float scale = 1.2f;
        private bool startDelayed = true;

        private Sprite[] frames; // 存储分割后的 Sprite 帧
        private float timer; // 计时器
        private int currentFrameIndex; // 当前帧索引

        private CharacterRendererData chara = null;

        private void Start() {
            StartCoroutine(LateStart(0.5f));
        }

        IEnumerator LateStart(float waitTime) {
            // 等待一定时间
            yield return new WaitForSeconds(waitTime);
            groundMap = MapRendererManager.Instance.groundMap;


            // 从 Sprite Sheet 图片中分割帧
            

            // 执行 Start 方法的内容
            Debug.Log("Delayed Start");

            // 获取指定格子的世界坐标
            Vector3 worldPosition = groundMap.CellToWorld(new Vector3Int(0, 0, 0)) + new Vector3(0.5f, 0.5f, 1);


            // 设置 Sprite 的位置和尺寸
            spriteRenderer.transform.position = worldPosition;
            spriteRenderer.size = groundMap.cellSize;

            // 调整 Sprite 的缩放来匹配 Tilemap 的格子大小
            Vector3 spriteScale = new Vector3(scale, scale, 1f);
            spriteRenderer.transform.localScale = spriteScale;

            // 开始播放动画
            PlayAnimation();

            // 设置标志位，表示已经延迟执行完 Start
            startDelayed = false;
        }

        private Texture2D LoadTextureFromFile(string filePath) {
            // 从磁盘加载图片数据
            byte[] imageData = System.IO.File.ReadAllBytes(filePath);

            // 创建一个新的 Texture2D 对象，并加载图片数据
            Texture2D textureLoad = new Texture2D(2, 2);
            textureLoad.LoadImage(imageData);

            return textureLoad;
        }
        private void PlayAnimation() {
            // 设置初始帧索引和计时器
            currentFrameIndex = 0;
            timer = 0f;

            // 设置 Sprite 渲染器的初始帧
            spriteRenderer.sprite = frames[currentFrameIndex];
        }

        private void Animate() {
            // 增加计时器
            timer += Time.deltaTime;

            // 根据帧率计算帧切换时间
            float frameTime = 1f / frameRate;

            // 如果计时器超过帧切换时间，切换到下一帧
            if (timer >= frameTime) {
                // 增加帧索引
                currentFrameIndex++;

                // 如果帧索引超过帧数，重置为第一帧
                if (currentFrameIndex >= frames.Length)
                    currentFrameIndex = 0;

                // 更新 Sprite 渲染器的当前帧
                spriteRenderer.sprite = frames[currentFrameIndex];

                // 重置计时器
                timer = 0f;
            }
        }

        public override void UpdateMessage(Message currentMessage) {
            if (currentMessage.ID == MessagesCode.RENDER_NEW_CHARACTER) {
                string path = (string)currentMessage.Obj1;
                //"Assets/Resources/Characters/spritefull.png"
                chara = new CharacterRendererData(path);
                frames = chara.Frames;
            }
        }

        public override void UpdateElse() {
            if (!startDelayed) {
                // 播放动画
                Animate();
            }
        }
    }
}
