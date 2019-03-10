using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._CodeBase.Demos
{
    public class TerrainPatch
    {
        private const float MaxHeight = 3f;

        private List<Vector3> Vertices = new List<Vector3>();
        private List<Vector2> UVs = new List<Vector2>();
        private List<int> Triangles = new List<int>();

        private GameObject meshObject;

        private MeshFilter meshFilter;
        private Material material;
        private Mesh mesh;
        private MeshCollider meshCollider;


        public int Size { get; set; }
        public float Spacing { get; set; }

        public TerrainPatch(int size, float spacing)
        {
            Size = size + 1;
            Spacing = spacing;
            material = Resources.Load("m_UvTest") as Material;
            CreateGrid();
            CreateMesh();
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
                        var height = Random.Range(0, MaxHeight);
                        Vertices.Add(new Vector3(x * Spacing, height, z * Spacing));
                        var u = (x * Spacing) / ((Size - 1) * Spacing);
                        var v = (z * Spacing) / ((Size - 1) * Spacing);

                        UVs.Add(new Vector2(u, v));

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

                        var height = Random.Range(0, MaxHeight);
                        Vertices.Add(new Vector3(posX, height, z * Spacing));

                        var u = posX / ((Size - 1) * Spacing);
                        var v = (z * Spacing) / ((Size - 1) * Spacing);

                        UVs.Add(new Vector2(u, v));

                        index++;
                    }
                }
                
            }

        }

        private void CreateMesh()
        {
            var index = 0;

            for (int z = 0; z < Size - 1; z++)
            {
                if (z % 2 == 0)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        Triangles.Add(index);
                        Triangles.Add(index + Size);
                        Triangles.Add(index + Size + 1);

                        if (x < Size -1)
                        {
                            Triangles.Add(index);
                            Triangles.Add(index + Size + 1);
                            Triangles.Add(index + 1);
                        }

                        index++;
                    }
                }
                else
                {
                    for (int x = 0; x < Size; x++)
                    {
                        Triangles.Add(index);
                        Triangles.Add(index + Size + 1);
                        Triangles.Add(index + 1);

                        if (x < Size - 1)
                        {
                            Triangles.Add(index + 1);
                            Triangles.Add(index + Size + 1);
                            Triangles.Add(index + Size + 2);
                        }

                        index++;
                    }
                    index++;
                }
            }

            meshObject = new GameObject("Mesh");
            meshObject.AddComponent<MeshFilter>();
            meshObject.AddComponent<MeshRenderer>();
            meshObject.AddComponent<MeshCollider>();

            mesh = new Mesh();
            mesh.vertices = Vertices.ToArray();
            mesh.uv = UVs.ToArray();
            mesh.triangles = Triangles.ToArray();

            mesh.RecalculateNormals();
            meshFilter = meshObject.GetComponent<MeshFilter>() as MeshFilter;
            meshFilter.mesh = mesh;

            meshCollider = meshObject.GetComponent<MeshCollider>() as MeshCollider;
            meshCollider.sharedMesh = mesh;

            meshObject.GetComponent<MeshRenderer>().material = material;
            meshObject.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(0.1f * (Size-1), 0.1f * (Size - 1));


        }
    }
}
