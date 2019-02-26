using System;
using Assets._CodeBase.DemoFramework;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private TerrainPatch patch;
        private const int PathSize = 128;
        private const float PathSpacing = 4f;

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
           
        }
    }
}
