# MU-VR-Experience
A collaboration with *Manos Unidas* to create a VR experience for viewing short films in Virtual Reality.

## Backend
Backend uses a combination of ExpressJS + MySQL and Sequelize as the ORM.  

How to install:  
Having an open connection to a MySQL server, run the ```database-setup.sql``` file.  

After, run ```npm install``` in the *\*backend\** directory when cloning the project to install the required libraries.  

An npm package called ```nodemon``` is used to reload changes automatically when files change. To install, simply run ```npm i -g nodemon```  

The following commands must be run inside the backend directory:  
To start the server, run ```npm start``` to start the server with nodemon.  
To start the server manually, run ```node server.js``` within the */backend* directory, to launch the server and reload it with Ctrl + C.

## Frontend
This project uses free SNAPS Prototyping Assets from Unity Asset store, as well as the [SNAPS Tool](https://assetstore.unity.com/packages/tools/integration/asset-swap-tool-151202?aid=1101lPGj&utm_campaign=unity_affiliate&utm_medium=affiliate&utm_source=partnerize-linkmaker), ProBuilder and ProGrids plugins. The project also uses TextMesh Pro, make sure to previously import it if you haven't. 

To avoid uploading unused Prefabs, only the Materials folder is uploaded (if needed) to provide the changed materials for the Prefabs, which can be obtained in the Unity Asset Store:  
[Office Prototype](https://assetstore.unity.com/packages/3d/environments/snaps-prototype-office-137490)  
[SciFi/Industrial Prototype](https://assetstore.unity.com/packages/3d/environments/sci-fi/snaps-prototype-sci-fi-industrial-136759)  
[SciFi Construction Kit (Modular)](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-construction-kit-modular-159280)  
[18 High Resolution Wall Textures](https://assetstore.unity.com/packages/2d/textures-materials/brick/18-high-resolution-wall-textures-12567)  

Additional plugins installed from the asset store:  
[Oculus Integration (for standalone version only)](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022)
  
**These prototypes should be installed in the "AssetStoreOriginals/_SNAPS_PrototypingAssets" directory, except SciFi Warehouse Kit and High Resolution Wall textures, which should be placed inside "Assets" to avoid errors*  
