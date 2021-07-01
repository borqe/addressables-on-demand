This is an experiment/research project on how to download Unity Addressables on demand.

- _Unity version: **2020.2.7f1** (should not matter much as it is such a small project)_
- _Addressables version: **1.16.10** (kinda matters, as docs can be outdated)_

## FTP Server
This project uses an ftp server which serves the assets needed. Asset bundles should be built in HostedData folder, and the ftp server should point to it. The server itself is created using FileZilla Server utility.

Steps for server setup:
1. **Edit > Users > General**: Create a user using **Add** button: I used root. 
2. **Edit > Users > Shared folders**: Add a folder which points to the bundles created by Unity. Add.
3. Test the server using FileZilla by connecting to localhost using created credentials and seeing if there is a _StandaloneWindows_ folder.

Now that the server is working, let's build an asset bundle for Unity Addressables to use.

## Asset Setup
I assume that Addressables package is already in the project. If not, download it using the package manager.

Steps for asset bundle build:
1. **Window > Asset Management > Addressables > Groups**
2. In the opened window, **Build > New Build > Default Build Script** to build an asset bundle that will be served using the ftp server.

Steps for updating asset bundle build:
1. **Window > Asset Management > Addressables > Groups**
2. In the opened window, **Build > Update a Previous Build**.

## Updating content on demand
1. Build asset bundle (see _Steps for asset bundle build_).
2. **File > Build And Run**
3. Open built project (_Addressables.exe_) and press _Spawn_ button to spawn different color cubes.
4. Go back to the Unity Editor and change something about the _Cube_ or _Cube2_ (ex. its material).
5. Update asset bundle build (see _Steps for updating asset bundle build_).
6. Go back to the opened project and press _Check for updates_, _Update_ button should be activated.
7. Press the _Update_ button and then the _Spawn_ button.
8. The updated cubes should be spawned.

## Other notes
Pretty much all basic things work out of the box and are configured.

- All assets are marked as _"Addressable"_.
- There are 2 groups: _"Hosted"_ (default) - uses remote path's from the _"Hosted"_ profile, _"Local"_ - uses local path's.
- Only _Cube_ and _Cube2_ edits will be visible after updating, _Cube3_ edits will be visible only on a new build.
- In _AddressableAssetSettings_ object _Build Remote Catalog_ must be checked to allow content updates.
- In _AddressableAssetSettings_ object _Disable Catalog Update on Startup_ must be checked to prevent addressables auto updating.
- If you want to change ftp url or user, go to **Window > Asset Management > Addressables > Profiles** and change the _RemoteLoadPath_.

## Download on demand notes
- I've tried the local hosting feature that Unity provides, but it did not work: still had to set up a ftp server.

### If there are any more questions, DM me in slack and I'll try to give more context!