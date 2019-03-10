using Assets._CodeBase.Demos;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
   public class TerrainGenerator{

        private int maxHeight;
        private Texture2D baseMap;

        public TerrainGenerator(int maxHeight, Texture2D baseMap)
        {
            this.maxHeight = maxHeight;
            this.baseMap = baseMap;
        }

        public float GetHeight(float x, float z)
        {
            var scale = 1024 / (17 * 5 - 4);

            var height = baseMap.GetPixel((int)(scale * x) + baseMap.width / 2, (int)(scale * z) + baseMap.height / 2).r * maxHeight;
            return height;
        }


    }
}
