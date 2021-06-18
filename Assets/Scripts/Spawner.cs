using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    public AssetReference asset;

    public List<GameObject> loadedObjects = new List<GameObject>();

    // private void Start() => StartCoroutine(Setup());

    private IEnumerator Setup()
    {
        string key = "assetKey";
        //Clear all cached AssetBundles
        Addressables.ClearDependencyCacheAsync(key);

        Debug.Log("clear cache");

        //Check the download size
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownloadSize;

        Debug.Log("get download size");

        //If the download size is greater than 0, download all the dependencies.
        if (getDownloadSize.Result > 0)
        {
            AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(key);
            yield return downloadDependencies;

            downloadDependencies.Completed += OnDownloadComplete;
        }
    }

    private void OnDownloadComplete(AsyncOperationHandle obj)
    {
        Debug.Log("download done");
    }

    public void Spawn()
    {
        Debug.Log(asset);
        Addressables.LoadAssetAsync<GameObject>(asset).Completed += OnLoaded;
    }

    private void OnLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            var instantiatedObject = Instantiate(obj.Result);
            loadedObjects.Add(instantiatedObject);
        }
    }

    public void Despawn()
    {
        asset.ReleaseAsset();
    }
}
