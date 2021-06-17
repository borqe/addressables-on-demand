using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

public class Spawner : MonoBehaviour
{
    public AssetReference _asset;

    public void Spawn()
    {
        
        var asset = _asset.InstantiateAsync();
        Debug.Log(asset.Result);
    }

    public void Despawn()
    {
        _asset.ReleaseAsset();
    }
}
