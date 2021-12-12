# Manos Unidas VR Experience
This project was made in collaboration Manos Unidas ONG. It consists of a virtual reality cinema/theatre made for short films provided to the short film contest made by Manos Unidas. For more information, you can visit the ["Clipmetrajes"](https://www.clipmetrajesmanosunidas.org/) section, where it explains more on the topic.

<p align="center">
  <img width="500" height="250" src="https://www.catolicos.red/wp-content/uploads/2017/04/large_campa_a_manos_unidas-corte_ingles.jpg" alt="Manos Unidas">
</p>

## Table Of Contents
- [Main Idea Behind The Project](#main-idea)
- [Use Cases](#use-cases)
- [Tech Stack And Comparison](#tech-stack)
- [Planning & Organising The Project](#project-planning)
- [Backend Structure](#backend-structure)
    - Data Model
    - ORM
    - How To Install And Run
- [Frontend Structure](#frontend-structure)
    - Mockups & Prototypes
    - How To Install
    - How To Use
    - User Requirements
    - Usability
- [System Requirements](#sys-requirements)
- [Final Thoughts](#conclusion)
- [External Links](#external-links)

<a name="main-idea"/>

## Main Idea Behind The Project
The main idea proposed for this project was to create a virtual reality environment where the user would join and watch short films. Here, the user would choose between all the short films submitted to the contest previously mentioned, and rate it based on their own opinion. 

Knowing what Manos Unidas was asking for, the idea transformed into making a cinema/theatre in virtual reality.

<a name="use-cases"/>

## Use Cases
Register Users are able to:
- Watch short films in the theatre
- Rate the short films
- View ratings for a specific short films

Managers are able to:
- Do all actions a registered user can
- Manage users
- Manage short film ratings

Administrators are able to:
- Do all actions a manager can
- Insert new short film videos and trailer images

What you can do with this app:
- Compile and run it as standalone for PC (for further information, please see [system requirements](#sys-requirements))

![Use Case Image](https://image-link)

<a name="tech-stack"/>

## Tech Stack And Comparison
The server side of this project was created with NodeJS.

<p align="center">
	<img width="350" height="175" src="https://cdn.pixabay.com/photo/2015/04/23/17/41/node-js-736399_960_720.png" alt="NodeJS Logo">
</p>

The reason behind the use of NodeJS comes from how extensive and powerful all frameworks within and external from NodeJS can be, delivering fast responses for real-time performance, as well as taking advantage of the extensive versatility that comes with the JavaScript language.

Within the use of NodeJS, frameworks like Express JS with Sequelize were used to provide a stable and fast server and server connection with the database.


The user experience on the other hand was made using Unity.  

<p align="center">
	<img width="600" height="140" src="https://unity3d.com/profiles/unity3d/themes/unity/images/pages/branding_trademarks/unity-mwu-black.png" alt="NodeJS Logo">
</p>

Unity was chosen for this project due to its simplicity and how powerful it can be. Knowing there are other game engines like Unreal Engine or Godot, Unity provides a simple and interactive user experience to develop the game, while providing powerful tools to exploit the users creativity.





<a name="project-planning">

## Planning & Organising The Project
hello

<a name="backend-structure"/>

## Backend Structure
### Data Model
hello

### ORM
Backend uses a combination of ExpressJS + MySQL and Sequelize as the ORM.  

### How To Install And Run
How to install:  
Having an open connection to a MySQL server, run the ```database-setup.sql``` file.  

After, run ```npm install``` in the *\*backend\** directory when cloning the project to install the required libraries.  

An npm package called ```nodemon``` is used to reload changes automatically when files change. To install, simply run ```npm i -g nodemon```  

The following commands must be run inside the backend directory:  
To start the server, run ```npm start``` to start the server with nodemon.  
To start the server manually, run ```node server.js``` within the */backend* directory, to launch the server and reload it with Ctrl + C.

<a name="frontend-structure"/>

## Frontend Structure
### Mockups & Prototypes
hello

### How To Install
This project uses free SNAPS Prototyping Assets from Unity Asset store, as well as the [SNAPS Tool](#external-links), ProBuilder and ProGrids plugins. The project also uses TextMesh Pro, make sure to previously import it if you haven't. 

To avoid uploading unused Prefabs, only the Materials folder is uploaded (if needed) to provide the changed materials for the Prefabs, which can be obtained in the Unity Asset Store:  
[Office Prototype](#external-links)  
[SciFi/Industrial Prototype](#external-links)  
[SciFi Construction Kit (Modular)](#external-links)  
[18 High Resolution Wall Textures](#external-links)  

Additional plugins installed from the asset store:  
[Oculus Integration (for standalone version only)](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022)
  
**These prototypes should be installed in the "AssetStoreOriginals/_SNAPS_PrototypingAssets" directory, except SciFi Warehouse Kit and High Resolution Wall textures, which should be placed inside "Assets" to avoid errors*  

### How To Use
hello

### User Requirements
hello

### Usability
hello

<a name="sys-requirements"/>

## System Requirements
hello

<a name="conclusion"/>

## Final Thoughts
Very good.

<a name="external-links"/>

## Related Links
### Backend
### Frontend
#### Tools
[SNAPS Tool](https://assetstore.unity.com/packages/tools/integration/asset-swap-tool-151202?aid=1101lPGj&utm_campaign=unity_affiliate&utm_medium=affiliate&utm_source=partnerize-linkmaker)

#### Assets
[Office Prototype](https://assetstore.unity.com/packages/3d/environments/snaps-prototype-office-137490)  
[SciFi/Industrial Prototype](https://assetstore.unity.com/packages/3d/environments/sci-fi/snaps-prototype-sci-fi-industrial-136759)  
[SciFi Construction Kit (Modular)](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-construction-kit-modular-159280)  
[18 High Resolution Wall Textures](https://assetstore.unity.com/packages/2d/textures-materials/brick/18-high-resolution-wall-textures-12567)

#### Other Unity Packages
[Oculus Integration (for standalone version only)](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022)


