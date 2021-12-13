# Manos Unidas VR Experience - User Manual

## Table Of Contents
- [User gameplay](#user-gameplay)
	- Settings Menu
- [The Reception Room](#reception-room)
	- Login/Register Interface
- [The Billboard Room](#billboard-room)
- [The Cinema Room](#cinema-room)
	- Ratings
	- Short Film Player

## User Gameplay
Once inside the application, you are greeted with a first person view with a character which you can interact using the default controls with keyboard and mouse. To see Virtual Reality controls, [click here](#virtual-reality-movement).

You move the camera around using your mouse, by moving it left, right, up and down.

You move the player around by using the WASD keys to accordingly move up, left, down and right.

You have a small crosshair in the middle of the screen which indicates where you will be clicking. To click, use the corresponding left click action within your mouse to interact with interactable objects.

<p align="center">
	<img src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/user_manual/crosshair.png?raw=true" alt="Crosshair">
</p>

Interactable objects include 3D button like objects with text within them. Interactable objects also include doors with a simple like design within any room which lead on to another room upon clicked.

<p align="center">
	<img src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/register-buttons.png?raw=true" alt="Register Buttons">
</p>

<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/user_manual/interactable-door.png?raw=true" alt="Interactable Door">
</p>

As an easy to follow guide, the crosshair shows arrows around it when an interactable comes within range of the crosshair and when it points towards it.

<p align="center">
	<img src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/user_manual/selected-crosshair.png?raw=true" alt="Selected Crosshair">
</p>

### Settings Menu
To open the settings menu, press the Escape key.

<p align="center">
	<img src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/user_manual/settings-menu.png?raw=true" alt="Settings Menu">
</p>

In this menu, you can customize your player controls:
- Choose between WASD or Arrow key movement
- To interact with objects, choose between Left Click or E Key
- Choose your sensitivty to change how fast the camera goes with every mouse movement. Values range from 5 to 20
- Customize the game volume at need. Values range from 0 to 100.
- Enable VR by clicking the checkbox
	- *WARNING*: An alert will appear to inform you that once VR is activated, you cannot revert back to mouse and keyboard movement. If you wish to do so, you will need to restart the app.

At the top right hand side, when logged in, you will see a *"User: "* text, followed by the current logged in username.

Lastly, when not inside the reception room, a button with text *"Return To Reception"* to logout and return to the reception area.

<a name="virtual-reality-movement"/>

### Virtual Reality Movement

```
*WARNING*:
Before proceding on how to move around in the virtual reality environment, 
please login/register first, as you will not be able to do so once you have entered the virtual reality stage.
```

Once your headset is on, move your head around to look at the environment around you.

Here's a basic schematic on the controllers of the Oculus Rift. If you are using another type of Oculus Headset, controls should be very similar. If not, please refer to your headset's controller manual and how to use it.

<p align="center">
	<img width="500" height="300" alt="Rift Controller Schematic" src="https://preview.redd.it/osfxjlt9ak911.png?width=2871&format=png&auto=webp&s=098c9d63dfd983b99adc649fc49938ace3b276ae">
</p>

To interact with objects, you grab them by pressing the inner controller buttons, the top joystick and resting your thumb on the joystick, as to be simulating grabing a real object.

Interactable objects tend to be 3D button like objects with text inside or door handles. Although the same, in virtual reality, you cannot grab the login and register buttons to login and register.

Interactable objects may include the following:

- Door handles: once grabbed, it will lead you to the next room.
<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/user_manual/interactable-door.png?raw=true" alt="Interactable Door">
</p>

- Enter button within the Billboard Room
<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/enter-button.png?raw=true" alt="Enter Button">
</p>

- Rating buttons within the Cinema Room
<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/rating-buttons.png?raw=true" alt="Enter Button">
</p>

To move around, use the joystick from the left controller.  
To move the camera within the horizontal axis a bit faster, use the joystick from the right controller.

## The Reception Room

This is the first and main room you encounter once you open the application.

Here, you will encounter a table with two buttons:
- Login: Authenticates an existing user
- Register: Authenticates a new user

After authenticating, a door will appear to the right of the room where the user is lead to the next room.

<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/reception-room.png?raw=true" alt="Reception Room">
</p>

## The Billboard Room

Inside this room you will find a wall containing picture frames with a trailer image and ratings. These correspond to the short films stored inside the databse.

<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/billboard-room.png?raw=true" alt="Billboard Room">
</p>

Each short film rating is updated every time the user enters the room.

To access the Cinema room with the desired short film, click the enter button under the picture frame of your short film of choice. This will transport you to the cinema room.

<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/picture-frames.png?raw=true" alt="Picture Fraims">
</p>

## The Cinema Room

Inside the Cinema room, you spawn directly infront of the video player widescreen. Upon enter, the video will start to play automatically.

<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/user_manual/cinema-room.png?raw=true" alt="Cinema Room">
</p>

You can press (or grab in VR) the rating buttons at any time during your stay in the Cinema room to rate the short film you're watching.

<p align="center">
	<img width="500" src="https://github.com/SkinnyDevi/MU-VR-Experience/blob/develop/readme-assets/images/rating-buttons.png?raw=true" alt="Rating Buttons">
</p>
To exit the Cinema room, first rate the short film watched, and after, a door will appear to confirm your exit. After exiting, you will return back to the Billboard room to choose another short film to watch.
