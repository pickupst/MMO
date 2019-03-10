using System;
using Assets._CodeBase.DemoFramework;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
        private const int PATCH_COUNT = 12;



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
            new TerrainPatch(new Vector3(0, 0, 0));

        }

        public void Update()
        {
           
        }
    }
}
