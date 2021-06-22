using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
    }

    private void OnInitialize(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("initialization complete");

        // First is usually the bundle key, can check if others work, too !
        assetBundleKey = obj.Result.Keys.First();

        // This is to log all the available keys stored in IResourceLocator instance
        // foreach (var itemKey in obj.Result.Keys)
        // {
        //     Debug.Log(itemKey);
        // }

        // Clear all cached AssetBundles!
        Caching.ClearCache();
        Addressables.ClearDependencyCacheAsync(assetBundleKey);

        StartCoroutine(GetDownloadSize());
    }

    private IEnumerator GetDownloadSize()
    {
        // Check the download size
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(assetBundleKey);
        yield return getDownloadSize;

        Debug.Log(getDownloadSize.Result);

        // Ideally, now you would call DownloadDependenciesAsync()
        // and everything would download with no problem
        // calling OnDownloadComplete action.

        // If the download size is greater than 0, download all the dependencies.
        if (getDownloadSize.Result > 0)
        {
            AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(assetBundleKey, true);
            downloadDependencies.Completed += OnDownloadComplete;

            yield return downloadDependencies;
        }
    }

    // This callback is here to indicate that the bundles were downloaded / are ready to use.
    // Also to prevent premature asset download in Spawner class (LoadAssetAsyc)
    private void OnDownloadComplete(AsyncOperationHandle obj)
    {
        Debug.Log("download done");
        spawnButton.interactable = true;
    }
}
