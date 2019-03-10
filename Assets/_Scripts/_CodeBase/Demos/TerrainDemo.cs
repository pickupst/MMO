using System;
using Assets._CodeBase.DemoFramework;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private const int PATCH_COUNT = 12;

        private TerrainPatch patch;
        private const int PathSize = 25;
        private const float PathSpacing = 1f;

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
            for (int z = 0; z < PATCH_COUNT; z++)
            {
                for (int x = 0; x < PATCH_COUNT; x++)
                {
                    patch = new TerrainPatch(new Vector3(x, 0, z), PathSize, PathSpacing);
                }
            }

           
        }

        public void Update()
        {
           
        }
    }
}
