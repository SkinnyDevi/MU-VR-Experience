# Manos Unidas VR Experience
This project was made in collaboration Manos Unidas ONG. It consists of a virtual reality cinema/theatre made for short films provided to the short film contest made by Manos Unidas. For more information, you can visit the ["Clipmetrajes"](https://www.clipmetrajesmanosunidas.org/) section, where it explains more on the topic.

<p align="center">
  <img width="400" height="250" src="https://www.manosunidas.org/sites/default/files/logo_mu.png" alt="Manos Unidas">
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

![Use Case Image](https://raw.githubusercontent.com/SkinnyDevi/MU-VR-Experience/develop/readme-assets/images/use-cases.png)

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
The planning went as follows:  
1. Tackle how to structure the project and develop a main idea.  
* To start off the project, first we made a basic planning on what we needed:
	- A place where users register
	- A room where users choose a short film
	- A cinema room where the users watch the short films

2. Start from the base and build up with the backend.  
* The backend was the first to be implemented so that when the frontend started development, it could all be tested correctly without speculations.  
The process in which the backend would be develop was the following:
	1. Develop user model and controller with authentication
	2. Develop short film model and controller
	3. Develop rating model and controllers
3. The frontend was to be developed later, and it would be developed in the following order:
	1. Main hub / reception first where a user would register
	2. The billboard room where a user would choose what short film to watch
	3. The main cinema room to watch the short films


<a name="backend-structure"/>

## Backend Structure
### Data Model
The backend database used was developed using MySQL. The database is structured as follows:

1. A Users table for registered
2. A short film table to store information about the short films
3. A rating table to store ratings submitteed by users

![E-R DB Diagram](https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/e-r_diagram.png?raw=true)
![UML DB Diagram](uml-link)

With the following Relational DB structure:

USERS(ID, username, email, password, isAdmin)  
CLIPS(ID, name, duration, trailer_img)  
RATINGS(ID, user_id, clip_id, rating)  


**Restriction: only one rating is allowed per video per user, meaning if the user rates the same video, the existing rating will be updated.*

### ORM
As the ORM, Express JS uses Sequelize to communicate with our MySQL database. We first define our models inside of the ```./models``` directory, after we define the controllers inside ```./controllers``` directory.

### How To Install And Run 
After having cloned the project and an open connection to a MySQL server, run the ```database-setup.sql``` file.

To install all dependencias, run ```npm install``` inside the *\*backend\** directory. 

An npm package called ```nodemon``` is used to reload changes automatically when files change. To install, simply run ```npm i -g nodemon``` in the console.

Before running the server, make sure you have created a ```.env``` file with the following contents:
```
JWT_SECRET=V3RY#1MP0RT@NT$3CR3T#

MYSQL_DATABASE=ongMU_db
MYSQL_USER=root
MYSQL_PASSWORD=pwd
MYSQL_ROOT_PASSWORD=root_pwd

DB_HOST=$YOUR DATABASE CONNECTION$ (normally localhost)

NODE_ENV=development
```

The following commands must be run inside the backend directory:  
- To start the server, run ```npm start``` to run with nodemon to run with automatic refresh.  
- To start the server manually, run ```node server.js``` to launch the server and reload it with Ctrl + C.

<a name="frontend-structure"/>

## Frontend Structure
### Mockups & Prototypes
Here are the mockups used to serve as base for the modeling of the different rooms.

[View the mockup document here.](pdf-document-with-mockup)

### How To Install
Firstly, clone the project. The main unity project file is located in ```./frontend/MU-VR-Experience```

To install Unity, make sure to install Unity Hub, and install the version of the Unity Editor *2020.3.20f1* through the Unity Hub app.

This project uses free SNAPS Prototyping Assets from the Unity Asset Store, as well as the [SNAPS Tool](#external-links), ProBuilder, ProGrids and TextMeshPro. Make sure to install this through the Unity Package Manager under the Window section of Unity.

To avoid uploading unused Prefabs, only the Materials folder is uploaded (if needed) to provide the changed materials for the Prefabs, which can be obtained in the Unity Asset Store:  

[Office Prototype](#external-links)  
[SciFi/Industrial Prototype](#external-links)  
[SciFi Construction Kit (Modular)](#external-links)  
[18 High Resolution Wall Textures](#external-links)  

Additional plugins installed from the asset store:  
[Oculus Integration (for standalone version only)](#external-links)
  
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
#### Tech Stack
[About ExpressJS](https://expressjs.com)  
[About bcrypt.js](https://www.npmjs.com/package/bcrypt)
### Frontend
#### Tech Stack
[Unity Hub](https://unity.com/download)  
[Unity Editor 2020.3.20f1](https://unity3d.com/get-unity/download/archive)
#### Aditional Tools
[SNAPS Tool](https://assetstore.unity.com/packages/tools/integration/asset-swap-tool-151202?aid=1101lPGj&utm_campaign=unity_affiliate&utm_medium=affiliate&utm_source=partnerize-linkmaker)

#### Assets
[Office Prototype](https://assetstore.unity.com/packages/3d/environments/snaps-prototype-office-137490)  
[SciFi/Industrial Prototype](https://assetstore.unity.com/packages/3d/environments/sci-fi/snaps-prototype-sci-fi-industrial-136759)  
[SciFi Construction Kit (Modular)](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-construction-kit-modular-159280)  
[18 High Resolution Wall Textures](https://assetstore.unity.com/packages/2d/textures-materials/brick/18-high-resolution-wall-textures-12567)

#### Other Unity Packages
[Oculus Integration (for standalone version only)](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022)

### Other Documentation
[User App Manual](app-manual)  


