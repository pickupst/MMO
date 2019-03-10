using System;
using Assets._CodeBase.DemoFramework;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private const int TILE_SIDE_COUNT = 1;



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
            CreateInitialTerrain();
        }

        public void Update()
        {
           
        }

        private void CreateInitialTerrain()
        {
            for (int x = -TILE_SIDE_COUNT; x <= TILE_SIDE_COUNT; x++)
            {
                for (int z = -TILE_SIDE_COUNT; z <= TILE_SIDE_COUNT; z++)
                {
                    new TerrainPatch(new Vector3(x, 0, z));
                }
            }
        }
    }
}
