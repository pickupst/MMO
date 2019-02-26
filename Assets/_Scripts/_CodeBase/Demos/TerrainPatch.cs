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
            Size = size + 1;
            Spacing = spacing;
            CreateGrid();
        }

        private void CreateGrid()
        {
            var index = 0;

            for (int z = 0; z < Size; z++)
            {
                if (z % 2 == 0)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        Vertices.Add(new Vector3(x * Spacing, 0, z * Spacing));
                        index++;
                    }
                }
                else
                {
                    for (int x = 0; x < Size + 1; x++)
                    {
                        var posX = x * Spacing - Spacing / 2;

                        if (posX < 0)   posX = 0;
                        if (posX > (Size - 1) * Spacing)    posX = (Size - 1) * Spacing;

                        Vertices.Add(new Vector3(posX, 0, z * Spacing));
                        index++;
                    }
                }
                
            }





            foreach (var vertex in Vertices)
            {
                GameObject tempGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                tempGameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                tempGameObject.transform.position = vertex;
            }

        }

        public void DrawGrid()
        {
            var index = 0;

            for (int z = 0; z < Size - 1; z++)
            {
                if (z % 2 == 0)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        Debug.DrawLine(Vertices[index], Vertices[index + Size]);
                        Debug.DrawLine(Vertices[index + Size], Vertices[index + Size + 1]);
                        Debug.DrawLine(Vertices[index + Size + 1], Vertices[index]);

                        if (x < Size -1)
                        {
                            Debug.DrawLine(Vertices[index], Vertices[index + Size + 1]);
                            Debug.DrawLine(Vertices[index + Size + 1], Vertices[index + 1]);
                            Debug.DrawLine(Vertices[index + 1], Vertices[index]);
                        }

                        index++;
                    }
                }
                else
                {
                    for (int x = 0; x < Size; x++)
                    {
                        Debug.DrawLine(Vertices[index], Vertices[index + Size + 1]);
                        Debug.DrawLine(Vertices[index + Size + 1], Vertices[index + 1]);
                        Debug.DrawLine(Vertices[index + 1], Vertices[index]);

                        if (x < Size - 1)
                        {
                            Debug.DrawLine(Vertices[index + 1], Vertices[index + Size + 1]);
                            Debug.DrawLine(Vertices[index + Size + 1], Vertices[index + Size + 2]);
                            Debug.DrawLine(Vertices[index + Size + 2], Vertices[index + 1]);
                        }

                        index++;
                    }
                    index++;
                }
            }
        }

    }
}
