This is an experiment/research project on how to download Unity Addressables on demand.

- _Unity version: **2020.1.8f1** (should not matter much as it is such a small project)_
- _Addressables version: **1.8.5** (kinda matters, as docs can be outdated)_

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

## Other notes
Pretty much all basic things work out of the box and are configured. The main thing that is not working is the actual download on-demand and not on asset load. The code is commented out to show the pain points.

- All assets are marked as _"Addressable"_.
- There is only one group, named _"Hosted"_, marked _default_ already.
- All paths are set to remote 
- If you want to change ftp url or user, go to **Window > Asset Management > Addressables > Profiles** and change the _RemoteLoadPath_.

## Download on demand notes
- I've tried the local hosting feature that Unity provides, but it did not work: still had to set up a ftp server.
- Now the download size always returns 0, I assume that some kind of build profile/setting is set incorrectly, didn't manage to find out which one is it. 

### If there are any more questions, DM me in slack and I'll try to give more context!