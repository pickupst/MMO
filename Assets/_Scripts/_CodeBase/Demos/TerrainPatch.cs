﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._CodeBase.Demos
{
    public class TerrainPatch
    {

        private const int Size = 17;
        private const float Spacing = 1f;
 
        public const int MaxHeight = 3;


        private List<Vector3> vertices = new List<Vector3>();
        private List<Vector2> uvs = new List<Vector2>();
        private List<int> triangles = new List<int>();

        private GameObject meshObject;

        private MeshFilter meshFilter;
        private Material material;
        private Mesh mesh;
        private MeshCollider meshCollider;


        private Vector3 position;

        public Vector2 OffSet { get; set; }

        public Vector3 Position
        {
            get { return position; }
            set { position = new Vector3(value.x * (Size - 1) * Spacing, value.y, value.z * (Size - 1) * Spacing); }
        }

        public static List<TerrainPatch> Patches { get; set; }

        public static float PatchSize
        {
            get
            {
                return (Size - 1) * Spacing;
            }
        }

        public TerrainPatch(Vector3 pos)
        {
            if (Patches == null)
            {
                Patches = new List<TerrainPatch>();
            }

            OffSet = new Vector2(pos.x, pos.z);

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

            var offSet = (Size - 1) * Spacing / 2f;

            for (int z = 0; z < Size; z++)
            {
                if (z % 2 == 0)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        var height = TerrainDemo.terrainGenerator.GetHeight(
                            x * Spacing - offSet + Position.x, 
                            z * Spacing - offSet + Position.z);

                        vertices.Add(new Vector3(x * Spacing - offSet, height, z * Spacing - offSet));
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

                        var height = 0f;
                        //var height = TerrainDemo.terrainGenerator.GetHeight(
                        //    x * Spacing - offSet + Position.x, 
                        //    z * Spacing - offSet + Position.z);

                        if (posX == 0 || posX == (Size - 1) * Spacing)
                        {
                            height = TerrainDemo.terrainGenerator.GetHeight(
                                            posX * Spacing - offSet + Position.x, 
                                            z * Spacing - offSet + Position.z);
                        }
                        else
                        {
                            var h1 = TerrainDemo.terrainGenerator.GetHeight(
                                            x * Spacing - Spacing - offSet + Position.x,
                                            z * Spacing - offSet + Position.z);

                            var h2 = TerrainDemo.terrainGenerator.GetHeight(
                                            x * Spacing - offSet + Position.x,
                                            z * Spacing - offSet + Position.z);

                            height = Mathf.Lerp(h1, h2, 0.5f);
                        }

                        vertices.Add(new Vector3(posX - offSet, height, z * Spacing - offSet));

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

            meshObject.GetComponent<MeshRenderer>().sharedMaterial = material;
            meshObject.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(0.1f * (Size - 1), 0.1f * (Size - 1));



        }

        public void Destroy()
        {
            vertices.Clear();
            uvs.Clear();
            triangles.Clear();
            GameObject.Destroy(meshFilter.mesh);
            GameObject.Destroy(meshCollider.sharedMesh);
            GameObject.Destroy(mesh);
            GameObject.Destroy(meshCollider);
            GameObject.Destroy(meshFilter);

            GameObject.Destroy(meshObject);

            Patches.Remove(this);
        }
    }
}
