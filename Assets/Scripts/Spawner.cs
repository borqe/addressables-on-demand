using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    public AssetReference asset;
    public List<GameObject> loadedObjects = new List<GameObject>();

    public void Spawn()
    {
        Addressables.LoadAssetAsync<GameObject>(asset).Completed += OnLoaded;
    }

    public void Clear()
    {
        foreach (var obj in loadedObjects)
        {
            Destroy(obj.gameObject);
        }

        loadedObjects.Clear();
        asset.ReleaseAsset();
    }

    private void OnLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            var instantiatedObject = Instantiate(obj.Result);
            loadedObjects.Add(instantiatedObject);
        }
    }
}
