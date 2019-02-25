using UnityEngine;

namespace Assets._CodeBase.DemoFramework
{
    public interface IDemo
    {
        void Awake();
        void Start();
        void Update();
        void OnGUI();   
        void OnApplicationQuit();

    }
}
