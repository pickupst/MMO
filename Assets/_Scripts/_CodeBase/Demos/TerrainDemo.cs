using System;
using Assets._CodeBase.DemoFramework;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private const int TILE_SIDE_COUNT = 1;

        private Vector2 lastPatchLocation = Vector2.zero;

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
            HandleTerrainUpdates();
        }

        private void HandleTerrainUpdates()
        {
            if (lastPatchLocation != CurrentPatchLocation)
            {
                if (CurrentPatchLocation.y > lastPatchLocation.y)
                {
                    ScrollUp();
                }

                lastPatchLocation = CurrentPatchLocation;
            }
        }

        private void ScrollUp()
        {
            var z = CurrentPatchLocation.y + 1;
            new TerrainPatch(new Vector3(CurrentPatchLocation.x, 0, z));
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
