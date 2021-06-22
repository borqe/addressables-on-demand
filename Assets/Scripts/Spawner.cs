using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    public AssetReference asset;
    public List<GameObject> loadedObjects = new List<GameObject>();

    // Loads assets using the Addressable package.
    public void Spawn()
    {
        // Uses a callback as the function is asynchronous.
        Addressables.LoadAssetAsync<GameObject>(asset).Completed += OnLoaded;
    }

    public void Clear()
    {
        foreach (var obj in loadedObjects)
        {
            Destroy(obj.gameObject);
        }

        loadedObjects.Clear();
    }

    private void OnLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            // EZ instantiation, like any other GameObject
            var instantiatedObject = Instantiate(obj.Result);
            loadedObjects.Add(instantiatedObject);
        }
    }
}
