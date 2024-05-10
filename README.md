# Runaway
This is an archive of *Runaway*, a group project that was developed by me and my peers from Georgia Tech's Video Game Design class (CS 4455) during the Spring 2024 semester.

The game was built with Unity. My proper role was Character Designer, wherein I was responsible for gathering and implementing models, animations, and animator state machines for the player, zombies, and NPC.
However, I also directly implemented and helped implement various game mechanics, including (but not limited to) the objective system and the interaction system.

The following are scripts that I have either authored or worked on:
- IInteractable.cs
- InteractionPrompt.cs
- Interator.cs
- ItemInteractable.cs
- InventoryMenuToggle.cs
- ObjectiveManager.cs
- Objective.cs
- SubObjective.cs
- SaveObjective.cs
- ObtainItemInteractable.cs
- HoldInteractable.cs
- PlayerStats.cs
- PropExplosion.cs
- ExplosionEvent.cs

## Developer Guide
> [!NOTE]
> Note: This repository contains the game's source code and files.
> If you would like to play the game from an executable, either refer to the *Tip* section below or follow this link to a Google Drive folder for the project: https://drive.google.com/drive/folders/1dZogkED3QtsNrrQllzIEnDUWfFNm_GGg?usp=sharing.

1. Install [Unity Hub](https://unity.com/download)
2. In Unity Hub, navigate to the: "Installs" tab -> "Install Editor" button -> "Archive" tab -> "download archive" link. Then, find and download Unity version **2021.3.33** via the "Unity Hub" button.
3. When downloading Unity version 2021.3.33, select the following modules and press the "Continue" button:
    - Microsoft Visual Studio Community
    - Mac Build Support
      - Try to select both the **IL2CPP** and **Mono** variants, but if only one is present, select that
    - Windows Build Support
      - Try to select both the **IL2CPP** and **Mono** variants, but if only one is present, select that
    - Documentation
4. Place this repository in a desired directory (either through downloading the ZIP file or cloning it)
5. In Unity Hub, click the "Add" button and select the folder that contains the repository files (it should be titled "Runaway")
6. Open the newly added project. This should give you access to all assets and scripts used to develop the game.
> [!IMPORTANT]
> The Unity scene that contains a bulk of the game's assets is "Level 1" under the "Scenes" folder. The scene "Menu" is the main menu of the game. To open either of these scenes, simply double-click them in Unity.

> [!TIP]
> In order to build the game from this project, refer to this guide: https://www.youtube.com/watch?v=7nxKAtxGSn8&ab_channel=Brackeys.

## Instructions
You’re a survivor in a zombie-infested land, and it is up to you to escape. You will be given 3 objectives that you must complete in order to win the game:
- Gather Supplies: Find and collect food, water, and a medkit scattered throughout the map
- Repair Generator: Find and work on a generator for about 15 seconds to complete it
- Rescue Survivor: Find a survivor and get the zombies away from him

But, be careful as zombie chase and attack you!

Move with WASD or arrow keys, crouch with C, and sprint with Left or Right Shift. Interact with all objects with the "E" key. Press the "ESC" key for the pause menu.

## Known Issues
- There is currently no UI for the screen that appears when the player completes the game, so you will have to simply exit the game from the taskbar
- Walking into certain colliders at certain angles can cause the player to be launched into the air
- Objects and characters may spawn on top of or inside obstacles (if this puts an objective object in an unreachable place, simply pause and hit restart)
I plan to fix these issues and expand the game at some point in the near future.
