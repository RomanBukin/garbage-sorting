using UnityEngine;
using Zenject;

namespace Game.Garbage
{
    public class GarbageFactory
    {
        private readonly GameObject[][] _garbage = new GameObject[6][];
        private readonly DiContainer _container;
        private readonly Vector3 _spawnPosition;
        

        public GarbageFactory(DiContainer container, Vector3 spawnPosition)
        {
            _container = container;
            _spawnPosition = spawnPosition;
            
            for (int i = 0; i < _garbage.Length; i++)
            {
                _garbage[i] = Resources.LoadAll<GameObject>($"Prefabs/Garbage/{i + 1}");
            }
        }

        ~GarbageFactory()
        {
            Resources.UnloadUnusedAssets();
        }

        public void Create(int[] types)
        {
            int type = Random.Range(0, types.Length);
            int index = Random.Range(0, _garbage[type].Length);
            var prefab = _garbage[type][index];
            
            var rotationZ = Random.Range(0f, 360f);
            var rotation = Quaternion.Euler(0f, 0f, rotationZ);
            var level = Random.Range(1, 4);

            var go = _container.InstantiatePrefab(
                prefab,
                _spawnPosition + Vector3.forward * level, 
                rotation, 
                null);
            go.layer = 5 + level;
        }
    }
}