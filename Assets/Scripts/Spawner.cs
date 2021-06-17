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

    private void Start() {
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        HostTopology topology = new HostTopology(config, 10);

        int hostId = NetworkTransport.AddHost(topology, 50655);

        byte error;

        var connectionId = NetworkTransport.Connect(hostId, "192.168.1.64", 50655, 0, out error);

        Debug.Log(connectionId);
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
