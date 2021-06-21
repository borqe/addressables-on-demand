using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.UI;

public class Downloader : MonoBehaviour
{
    public Button spawnButton;
    public List<AssetLabelReference> labelReferences = new List<AssetLabelReference>();

    public object assetBundleKey;

    public void Download() => StartCoroutine(Setup());

    private IEnumerator Setup()
    {
        Addressables.InitializeAsync().Completed += OnInitialize;

        yield return assetBundleKey;

        // //If the download size is greater than 0, download all the dependencies.
        // if (getDownloadSize.Result > 0)
        // {
        //     AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(assetKey, true);
        //     downloadDependencies.Completed += OnDownloadComplete;
            
        //     yield return downloadDependencies;
        // }

        yield return null;
    }

    private void OnInitialize(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("initialization complete");

        assetBundleKey = obj.Result.Keys.First();

        //Clear all cached AssetBundles
        Caching.ClearCache();
        Addressables.ClearDependencyCacheAsync(assetBundleKey);

        StartCoroutine(GetDownloadSize());

        // foreach (var itemKey in obj.Result.Keys)
        // {
        //     Debug.Log(itemKey);
        // }
    }

    private IEnumerator GetDownloadSize()
    {
        // Check the download size
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(assetBundleKey);
        yield return getDownloadSize;

        Debug.Log(getDownloadSize.Result);
    }

    private void OnDownloadComplete(AsyncOperationHandle obj)
    {
        Debug.Log("download done");
        spawnButton.interactable = true;
    }
}
