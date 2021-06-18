using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Downloader : MonoBehaviour
{
    public void Download() => StartCoroutine(Setup());

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

        // TODO: Enable spawn button
        Debug.Log("download done");
    }
}
