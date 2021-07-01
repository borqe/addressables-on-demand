using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Downloader : MonoBehaviour
{
    public Button updateButton;
    public Spawner spawner;

    private List<string> toUpdate = new List<string>();

    public void NeedsUpdate(List<string> _toUpdate)
    {
        toUpdate = _toUpdate;
        updateButton.interactable = true;
    }

    public void UpdateAdressables()
    {
        spawner.Clear();
        Addressables.UpdateCatalogs(toUpdate);

        toUpdate.Clear();
        updateButton.interactable = false;
    }
}
