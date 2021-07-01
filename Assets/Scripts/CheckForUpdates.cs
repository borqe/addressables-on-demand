using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CheckForUpdates : MonoBehaviour
{
    public void Check()
    {
        Addressables.CheckForCatalogUpdates().Completed += OnCheck;
    }

    private void OnCheck(AsyncOperationHandle<List<string>> updated)
    {
        if (updated.Status == AsyncOperationStatus.Succeeded)
        {
            if (updated.Result.Count > 0)
            {
                GetComponent<Downloader>().NeedsUpdate(updated.Result);
            }
            else
            {
                Debug.Log("No updates needed");
            }
        }
        else
        {
            Debug.LogError("CheckForCatalogUpdates failed!");
        }
    }
}
