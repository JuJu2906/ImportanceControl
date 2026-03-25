# Importance Control: Gaze-based adaptation objective
This repository contains:
- [AUIT](https://github.com/joaobelo92/auit), a toolkit to support the design of adaptive user interfaces in XR
- [VR Gaze Interaction System](https://assetstore.unity.com/packages/tools/camera/vr-gaze-interaction-system-241337), a tool to obtain basic gaze-gameobject interaction information

The base AUIT was extended by adding
- WindowGazeData, a data structure for storing and updating various metric data and hyperparameters,
- UpdateManager, a manager for continuously updating your gameobjects/windows,
- ImportanceControl, an adaptation objective that evaluates and suggests opacity and resize adaptations,
- ImportanceTransition, a transition property for applying smooth opacity and resize transitions,
- and some minor changes to other base scripts in AUIT.

## To run and use Importance Control
Note: We used Unity Editor Version 6000.3.8f1 and a Universal 3D project.

To use our tool, first clone this repository from GitHub to your local machine.
After cloning the project, open it in Unity and navigate to the Assets/Scenes folder. There, you will find a prepared Base Scene that already includes the essential setup for getting started. You can duplicate this scene and use it as the foundation for your own project.
Please make sure to follow the next steps in this guide for adding UI elements correctly.
You may also find additional prepared scenes in the same folder. These serve as examples and can help you understand how to use the tool and structure your own project.

## Game Object / Window Set-up
1. Create a game object of your choice (currently limited to "UI (Canvas)"), and set the Render Mode as "World Space"
2. Define the dimensions/geometric bounds for your entire game object (including its children)
3. In the game object, add the component "Canvas Group" (Note: This is required for setting the opacity of the game object and its children simultaneously)
4. Add another component called "Window Gaze Data" under Assets/Importance
5. Set up the Gaze Interactable:
   5.1 Once again in Project files, navigate to Assets/GazeInteraction/Prefabs which should also contain "Gaze_Interactable.prefab"
   5.2 Under the game object, drag and drop the "Gaze_Interactable.prefab"
   5.3 In the inspector of Gaze_interactable, under "Transform"
   - set the scale to match the width and height of the game object
   - set the position to 0,0,0
   5.4 In the inspector of Gaze_interactable, under "Events -> On Gaze Enter()", click on the "+" button
   5.5 Drag and drop your game object into the empty field
   5.6 Click on "No Function" and select "WindowGazeData -> UpdateGazeEnter()"
   5.7 Do the same thing for "On Gaze Exit()" with the corresponding "UpdateGazeExit()"
Now your game object is fully set in the scene, you can already interact with it and obtain Importance values.

To use the adaptation objective, follow the remaining steps:
1. Add both components "Importance Control" and "Importance Transition" to the game objects that you want to adapt
2. Inside Update Manager inspector, under "Windows", click on the "+" button, and drag your game object into the empty field
3. In the AUIT inspector, under "Game Objects To Optimize", click on the "+" button, and drag your game object(s) into the empty field


## Experimental Section
If the calibration of your eye-tracking feels off, you may manually change the position of "Camera Rig" to your liking.
You may also experiment with the hyperparameters provided (including tooltips) under UpdateManager. For further explanation on how the hyperparameters interact with and affect Importance Control, refer to the methodology section of the report by Chanwook and Jawad.
