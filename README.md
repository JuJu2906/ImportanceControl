# Importance Control: Gaze-based adaptation objective
This repository contains
- AUIT, a toolkit to support the design of adaptive user interfaces in XR,
- VR Gaze Interaction System, a tool to obtain basic gaze-gameobject interaction information.
The base AUIT was extended by adding
- WindowGazeData, a data structure for storing and updating various metric data and hyperparameters,
- UpdateManager, a manager for continuously updating your gameobjects/windows, --- [DOUBLE CHECK PLEASE] ---
- ImportanceControl, an adaptation objective that evaluates and suggests opacity and resize adaptations,
- ImportanceTransition, a transition property for applying smooth opacity and resize transitions,
- and some minor changes to other base scripts.

## To run and use Importance Control
The extended AUIT can be imported from github --- [INSERT GITHUB LINK FOR OUR STUFF] ---. Warning: It is important to note that the base AUIT does not fully provide the necessary framework for operating Importance Control.
VR Gaze Interaction System can be imported from https://assetstore.unity.com/packages/tools/camera/vr-gaze-interaction-system-241337.

## Project & Importance Initialization
Note: We used Unity Editor Version 6000.3.8f1 and a Universal 3D project.
1. Follow the documentation in https://developers.meta.com/horizon/documentation/unity/move-unity-getting-started/ to set up the essential environment.
2. Add the following Building Blocks to your project:
  - Camera Rig (delete Main Camera to replace it)
  - Eye Gaze (will automatically attach to the Camera Rig in the scene)
3. Manage the access and permissions of the camera:
  3.1 In "Camera Rig", under the component "OVR Manager (Script)", do the following:
    - "Quest Features -> Passthrough Support" = Required
    - "Quest Features -> Eye Tracking Support" = Required
    - "Permission Requests On Startup -> Eye Tracking" = on
  3.2 Navigate to "Camera Rig -> Tracking Space -> LeftEyeAnchor -> Eye Gaze Left", and check if the following is true:
    - Apply Position is on
    - Apply Rotation is on
    - Tracking Mode is set to "Head Space"
4. Set up the Gaze Interactor:
  4.1 In Project files, navigate to "GazeInteraction -> Prefabs" which should contain "Gaze_Interactor.prefab"
  4.2 Under "Eye Gaze Left" in the hierarchy, Drag and drop the "Gaze_Interactor.prefab"
  4.3 In the inspector, configure your preferred Max and Min Detection Distance (Note: We use [0,100])
  4.4 Set the preferred "Time To Activate" (Note: We use 0.3)
  4.5 Set Layer Mask to "Everything"
  4.6 (optional) Enable reticle to test the focus accuracy
5. Set up the Importance Manager:
  5.1 Create an Empty Object (optionally name it "Importance Manager")
  5.2 In the inspector, add the component "Update Manager"

## Game Object / Window Set-up
1. Create a game object of your choice (e.g. "UI (Canvas) -> Image"), and set the Render Mode as "World Space"
2. Define the dimensions/geometric bounds for your entire game object (including its children)
3. In the game object, add the component "Canvas Group" (Note: This is required for setting the opacity of the game object and its children simultaneously)
4. Add another component called "Window Gaze Data"
5. Set up the Gaze Interactable:
  1. Once again in Project files, navigate to "GazeInteraction -> Prefabs" which should also contain "Gaze_Interactable.prefab"
  2. Under the game object, drag and drop the "Gaze_Interactable.prefab"
  3. In the inspector, under "Box Collider", do the following:
  - Set all Center values to 0
  - Set all Size values to 1
  - Set the scale to match the game object
  4. Under "Events -> On Gaze Enter()", click on the "+" button
  5. Drag and drop your game object into the empty field
  6. Click on "No Function" and select "WindowGazeData -> UpdateOnGazeEnter()"
  7. Do the same thing for "On Gaze Exit()" with the corresponding "UpdateOnGazeExit()"
6. Inside Update Manager, under "Windows", click on the "+" button, and drag your game object into the empty field
Once your game object is fully set in the scene, you can already interact with it and obtain Importance values.

## AUIT & Importance Adaptation Set-up
1. Drag and drop the "AUIT.prefab" into your scene
2. In the inspector, under "Game Objects To Optimize", click on the "+" button, and drag your game object(s) into the empty field
3. Add the component "Interval Optimization Trigger", and set the "Interval" to 0.5
4. Furthermore, add the components "Importance Control" and "Importance Transition"

## Experimental Section
If the calibration of your eye-tracking feels off, you may manually change the position of "Camera Rig" to your liking.
You may also experiment with the hyperparameters provided (including tooltips) under WindowGazeData. For further explanation on how the hyperparameters interact with and affect Importance Control, refer to the methodology section of the report by Chanwook and Jawad.