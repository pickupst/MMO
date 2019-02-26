using System;
using Assets._CodeBase.DemoFramework;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private TerrainPatch patch;
        private const int PathSize = 10;
        private const float PathSpacing = 0.5f;

        public void Awake()
        {
            
        }

        public void OnApplicationQuit()
        {
           
        }

        public void OnGUI()
        {
            
        }

        public void Start()
        {
            patch = new TerrainPatch(PathSize, PathSpacing);
        }

        public void Update()
        {
            patch.DrawGrid();
        }
    }
}
