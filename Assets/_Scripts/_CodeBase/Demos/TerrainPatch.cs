using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._CodeBase.Demos
{
    public class TerrainPatch
    {

        private List<Vector3> Vertices = new List<Vector3>();
        public int Size { get; set; }
        public float Spacing { get; set; }

        public TerrainPatch(int size, float spacing)
        {
            Size = size;
            Spacing = spacing;
            CreateGrid();
        }

        private void CreateGrid()
        {
            var index = 0;

            for (int z = 0; z < Size; z++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Vertices.Add(new Vector3(x * Spacing, 0, z * Spacing));
                    index++;
                }
            }

            foreach (var vertex in Vertices)
            {
                GameObject tempGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                tempGameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                tempGameObject.transform.position = vertex;
            }

        }
    }
}
