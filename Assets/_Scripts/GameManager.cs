using Assets._CodeBase.DemoFramework;
using Assets._CodeBase.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static IDemo Demo { get; private set; }

    private void Awake()
    {
        Demo = new TerrainDemo();
        Demo.Awake();
    }

    private void Start()
    {
        Demo.Start();
    }

    private void Update()
    {
        Demo.Update();
    }

    private void OnGUI()
    {
        Demo.OnGUI();
    }

    private void OnApplicationQuit()
    {
        Demo.OnApplicationQuit();
    }


}
