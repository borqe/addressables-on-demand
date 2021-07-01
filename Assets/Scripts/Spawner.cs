using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    public List<AssetReference> assets;
    public List<GameObject> loadedObjects = new List<GameObject>();

    public void Spawn()
    {
        foreach (var asset in assets)
        {
            Addressables.InstantiateAsync(asset).Completed += OnInstantiation;
        }
    }

    private void OnInstantiation(AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            loadedObjects.Add(obj.Result);
        }
        else
        {
            Debug.LogError("Instantiation failed!");
        }
    }

    public void Clear()
    {
        foreach (var loadedObject in loadedObjects)
        {
            Addressables.ReleaseInstance(loadedObject);
        }

        loadedObjects.Clear();
        Resources.UnloadUnusedAssets();
        Caching.ClearCache();
    }
}
