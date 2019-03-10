using System;
using Assets._CodeBase.DemoFramework;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private const int TILE_SIDE_COUNT = 1;

        private Vector2 lastPatchLocation = Vector2.zero;

        private int assetSweepCounter = 0;

        public GameObject TP_Controller;

        public Vector2 CurrentPatchLocation
        {
            get
            {
                return new Vector2((int)(TP_Controller.transform.position.x / TerrainPatch.PatchSize),
                                    (int)(TP_Controller.transform.position.z / TerrainPatch.PatchSize));
            }
        }

        public void Awake()
        {
            TP_Controller = GameObject.FindGameObjectWithTag("Player");
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
            if (TerrainPatch.Patches != null)
            {
                HandleTerrainUpdates();
            }
        }

        private void HandleTerrainUpdates()
        {
            if (lastPatchLocation != CurrentPatchLocation)
            {
                if (CurrentPatchLocation.y > lastPatchLocation.y)
                {
                    ScrollUp();
                }

                if (CurrentPatchLocation.y < lastPatchLocation.y)
                {
                    ScrollDown();
                }

                if (CurrentPatchLocation.x > lastPatchLocation.x)
                {
                    ScrollRight();
                }

                if (CurrentPatchLocation.x < lastPatchLocation.x)
                {
                    ScrollLeft();
                }

                lastPatchLocation = CurrentPatchLocation;
            }

            for (int i = TerrainPatch.Patches.Count - 1; i >= 0; i--)
            {
                var terrainPatch = TerrainPatch.Patches[i];
                if (Mathf.Abs(CurrentPatchLocation.y - terrainPatch.OffSet.y) > TILE_SIDE_COUNT || Mathf.Abs(CurrentPatchLocation.x - terrainPatch.OffSet.x) > TILE_SIDE_COUNT)
                {
                    terrainPatch.Destroy();
                    assetSweepCounter ++;
                    if (assetSweepCounter >= TILE_SIDE_COUNT * 2 + 1)
                    {
                        Resources.UnloadUnusedAssets();
                        assetSweepCounter = 0;
                    }
                }
            }
        }

        private void ScrollUp()
        {
            for (int x = -TILE_SIDE_COUNT; x <= TILE_SIDE_COUNT; x++)
            {
                var z = CurrentPatchLocation.y + TILE_SIDE_COUNT;
                new TerrainPatch(new Vector3(x + CurrentPatchLocation.x, 0, z));
            }
        
        }

        private void ScrollDown()
        {
            for (int x = -TILE_SIDE_COUNT; x <= TILE_SIDE_COUNT; x++)
            {
                var z = CurrentPatchLocation.y - TILE_SIDE_COUNT;
                new TerrainPatch(new Vector3(x + CurrentPatchLocation.x, 0, z));
            }

        }

        private void ScrollRight()
        {
            for (int z = -TILE_SIDE_COUNT; z <= TILE_SIDE_COUNT; z++)
            {
                var x = CurrentPatchLocation.x + TILE_SIDE_COUNT;
                new TerrainPatch(new Vector3(x, 0, z + CurrentPatchLocation.y));
            }

        }

        private void ScrollLeft()
        {
            for (int z = -TILE_SIDE_COUNT; z <= TILE_SIDE_COUNT; z++)
            {
                var x = CurrentPatchLocation.x - TILE_SIDE_COUNT;
                new TerrainPatch(new Vector3(x, 0, z + CurrentPatchLocation.y));
            }

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
