using System;
using Assets._CodeBase.DemoFramework;

namespace Assets._CodeBase.Demos
{
    public class TerrainDemo : IDemo
    {
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
            new TerrainPatch(5, 1f);
        }

        public void Update()
        {
            
        }
    }
}
