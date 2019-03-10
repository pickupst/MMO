using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._CodeBase.Demos
{
    public class TerrainPatch
    {

        private const int Size = 49;
        private const float Spacing = 3f;

        private const float MaxHeight = 1f;


        private List<Vector3> vertices = new List<Vector3>();
        private List<Vector2> uvs = new List<Vector2>();
        private List<int> triangles = new List<int>();

        private GameObject meshObject;

        private MeshFilter meshFilter;
        private Material material;
        private Mesh mesh;
        private MeshCollider meshCollider;


        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = new Vector3(value.x * (Size - 1) * Spacing, value.y, value.z * (Size - 1) * Spacing); }
        }

        public static List<TerrainPatch> Patches { get; set; }

        public TerrainPatch(Vector3 pos)
        {
            if (Patches == null)
            {
                Patches = new List<TerrainPatch>();
            }

            Position = pos;

            material = Resources.Load("m_UvTest") as Material;

            meshObject = new GameObject("Mesh");
            meshObject.AddComponent<MeshFilter>();
            meshObject.AddComponent<MeshRenderer>();
            meshObject.AddComponent<MeshCollider>();
            meshObject.transform.position = position;
            meshFilter = meshObject.GetComponent<MeshFilter>() as MeshFilter;
            meshCollider = meshObject.GetComponent<MeshCollider>() as MeshCollider;


            CreateGrid();
            CreateMesh();
            UpdateMesh(); 

            Patches.Add(this);
        }

        private void CreateGrid()
        {
            vertices.Clear();
            uvs.Clear();

            for (int z = 0; z < Size; z++)
            {
                if (z % 2 == 0)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        var height = 0;
                        vertices.Add(new Vector3(x * Spacing, height, z * Spacing));
                        var u = (x * Spacing) / ((Size - 1) * Spacing);
                        var v = (z * Spacing) / ((Size - 1) * Spacing);

                        uvs.Add(new Vector2(u, v));

                    }
                }
                else
                {
                    for (int x = 0; x < Size + 1; x++)
                    {
                        var posX = x * Spacing - Spacing / 2;

                        if (posX < 0)   posX = 0;
                        if (posX > (Size - 1) * Spacing)    posX = (Size - 1) * Spacing;

                        var height = 0;
                        vertices.Add(new Vector3(posX, height, z * Spacing));

                        var u = posX / ((Size - 1) * Spacing);
                        var v = (z * Spacing) / ((Size - 1) * Spacing);

                        uvs.Add(new Vector2(u, v));

                    }
                }
                
            }

        }

        private void CreateMesh()
        {
            triangles.Clear();

            var index = 0;

            for (int z = 0; z < Size - 1; z++)
            {
                if (z % 2 == 0)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        triangles.Add(index);
                        triangles.Add(index + Size);
                        triangles.Add(index + Size + 1);

                        if (x < Size -1)
                        {
                            triangles.Add(index);
                            triangles.Add(index + Size + 1);
                            triangles.Add(index + 1);
                        }

                        index++;
                    }
                }
                else
                {
                    for (int x = 0; x < Size; x++)
                    {
                        triangles.Add(index);
                        triangles.Add(index + Size + 1);
                        triangles.Add(index + 1);

                        if (x < Size - 1)
                        {
                            triangles.Add(index + 1);
                            triangles.Add(index + Size + 1);
                            triangles.Add(index + Size + 2);
                        }

                        index++;
                    }
                    index++;
                }
            }

           

        }

        private void UpdateMesh()
        {

            mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();

            mesh.RecalculateNormals();

            meshFilter.mesh = mesh;


            meshCollider.sharedMesh = mesh;

            meshObject.GetComponent<MeshRenderer>().material = material;
            meshObject.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(0.1f * (Size - 1), 0.1f * (Size - 1));



        }
    }
}
