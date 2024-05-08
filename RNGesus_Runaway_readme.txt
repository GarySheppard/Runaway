Runaway
By RNGesusSoftware

Starting Scene: Menu.unity
—-------------------------------------------------------------------------------------------------
Instructions

You’re a survivor in a zombie-infested land, and it is up to you to escape. You will be given 3 objectives that you must complete in order to win the game:
- Gather Supplies: Find and collect food, water, and a medkit scattered throughout the map
- Repair Generator: Find and work on a generator for _ seconds to complete it
- Rescue Survivor: Find a survivor and get the zombies away from him
But, be careful as zombie chase and attack you!

Move with WASD or arrow keys, crouch with C, and sprint with Left or Right Shift. Interact with all objects with the "E" key.
Press the "ESC" key for the pause menu.

—-------------------------------------------------------------------------------------------------
Known Problem Areas
- Walking into certain colliders at certain angles can cause the player to be launched into the air
- Objects and characters may spawn on top of or inside obstacles (if this puts an objective object in an unreachable place, simply pause and hit restart)
—-------------------------------------------------------------------------------------------------

Lee Rist
Role: Environment Programming and UI Aesthetics/Functionality

For this, Lee created the Menu Scene, with the Play button, Controls button, Options button, 
and Exit button. The Controls menu lists the player controls and the Options button lets the player
change the volume.
Lee also create the pause menu, which can be accessed with Esc. It includes a Reset 
and Start Menu button to restart the level or go to the start menu. The Reset set script
is the same as the Start Script, with the Start Menu script being a changed version of it. Also helped
to implement the volume control on the Options menu.

For this, the Scripts for these were: 
- OptionsMenu.cs
- MainMenu.cs
- PauseMenu.cs
- ToStartMenu.cs

Lee also worked on the games Objective system and Inventory/Hotbar system. For this, Lee made 
the UI and made it display the current level's objective, the player's HP, the time left for the 
level and the Player's weapons/inventory. They also added the functionality of the Inventory/Hotbar 
system and the visual changing of weapons and HP.

For this, the Scripts for these were: 
- HUDManager.cs
- ObjectiveManager.cs
- GameOverPopup.cs
- MainObjective.cs
- Objective.cs
- SubObjective.cs
- ObjectiveComplete.cs
- Item.cs
- ItemController.cs
- ItemPickup.cs
- InventoryManager.cs
- InventoryItemController.cs



Sarah Mabrouk
Role: Landscape/Non-Interactable/Interactables Asset Design

Sarah worked on creating the landscape design and non-interactable assets portion. For the coding part, Sarah worked on the base code for the inventory. She implemented the functionality for the game to go to the GameOver screen when the player runs out of HP and time runs out. She also implemented the functionality where the game over screen pop up actually ends the game. She also implemented the timer functionality itself. She set up all the buildings and walls surrounding the scene so the player doesn't roam off the map. She added aspects of the map to make the gameplay more engaging by adding haunted props that move by themselves such as horror dolls, wheelchairs, and added drones for a futuristic post-apocalyptic feel. To make the gameplay more immersive, she added more lighting around the map. She implemented a self-driving animation for the malfunctioning car so the player uses it as a never-ending rolling cover.

The assets (All from Unity Asset Store) used were:
1) Countryside gas station by vgtixn.artstation.com/ (Assets under Gas Station and Old Rust Car)
2) Old Building by Rusik3DModels  (Asset: Old Building)
3) Survival Old House by Nikolay Fedorov (Asset: SurvivalOldHouse01_Prefab)
4) Destroyed Building Kit by Demo (Loknar Studio) (Asset: Destroyed Building, DBK_Concete_wall_Full)
5) Littered Ground Materials- Texture Pack 01 by Deep Field Development  (Asset: Ground Rubble)
6) Rubble and Debris - Modular Set -  Free Sample by Loknar Studio (Asset: Rubble and Debris)
7) Horror Assets by Lukas Bobor boborlukas.wixsite.com/portfolio (Assets: DeadBody, HorrorDoll, Painting, RockingChair, WheelChair) under Non-Interactable Props & Particles
8) Wasteland Cabin by Lukas Bobor (Assets: All of the assets with name "Wast_" including Wasteland Cabin itself) under Non-Interactable Props & Particles
9) Assets/Raw Models and Textures/Environment & System/Non-Interactable Props & Particles/Buildings/Destroyed Building/Models/Concrete/DBK_Concete_Wall_Full.fbx
10) 3D Tires by GamesAreLife www.artstation.com/n33k (Asset: Small Tires under Non-Interactable Props)
11) Pallets by MyNameIsVoo www.artstation.com/mynameisvoo (Asset: Non-Interactable Props)
12) Realistic Sandbags by Flaming Sands www.artstation.com/alaskari (Asset:Sandbags under Non-Interactable Props & Particles)
13) Mini Army Tent by Trextor15 www.artstation.com/racksuz_design (Asset: Mini Army Tent under Non-Interactable Props & Particles)
14) Rock and Boulders 2 by Manufactura K4 www.facebook.com/ManufacturaK4 (Asset: Assets "Rock.." Under Prefabs)
15) Sci-Fi Drone by leonidas10009 www.artstation.com/leonidas10009 (Assets: scifi-drone under Misc)
16) PBR Dirty Jack-In-The-Box by Jesper Molander www.artstation.com/jespermolander3d   (Asset: JackInTheBox under Misc)
17) Blue Sedan by Team XS www.teamxsgames.comm (Asset: Team_Xs under Misc)
  
C# Scripts worked on are the ones under the Inventory Scripts folder: 
1) InventoryItemController.cs
2) InventoryManager.cs
3) Item.cs
4) Item Controller.cs
5) Item Pickup.cs
6) Player.cs
7) HauntedController.cs
8) CarController.cs
9) CountDownTimer.cs




Gary Sheppard
Role: Character Design

Gary worked on the models and animation state machines for the player, zombie enemies, and NPC. He also assisted in implementing the inventory, objective, and interaction systems.

The assets used were:
1) Adventure Character by Maksim Bugrimov from the Unity Asset Store (file directory: Assets/Models/Adventure_Character)
2) Zombie by Pxltiger from the Unity Asset Store (file directory: Assets/Models/Zombies)
3) Basic Motions FREE by Kevin Iglesias from the Unity Asset Store (file directory: Assets/Animations/PlayerAnimations/Basic Motion Pack)
4) Crouched To Standing, Crouched Walking, Crouching Idle, Pick Fruit Low, Pick Fruit Mid, Climbing, Standing Melee Attack Horizontal, Crouch Rapid Fire, Rummaging, Frisbee Throw, Pistol Idle, Shooting animations from Mixamo (file directory: Assets/Animations/PlayerAnimations)
5) Not So Scary Zombie from Mixamo (file directory: Assets/Animations/EnemyAnimations)
6) Injured Wave Idle animation from Mixamo (file directory: Assets/Animations/NPCAnimations)
7) Dying animation from Mixamo (file directory: Assets/Animations/PlayerAnimations/Death)
8) Particle Pack by Unity Technologies (file directory: Assets/Misc/Particle Pack)
9) Zombie Death animation from Mixamo (file directory: Assets/Animations/EnemyAnimations)

The C# scripts authored/edited were:
1) IInteractable.cs
2) InteractionPrompt.cs
3) Interator.cs
4) ItemInteractable.cs
5) InventoryMenuToggle.cs
6) ObjectiveManager.cs
7) Objective.cs
8) SubObjective.cs
9) SaveObjective.cs
10) ObtainItemInteractable.cs
11) HoldInteractable.cs
12) PlayerStats.cs
13) PropExplosion.cs
14) ExplosionEvent.cs



Gadaadhar Chennupati
Role: Player and Enemy Programming

Gadaadhar Worked on Player and enemy character control scripts. 

Game object design:
1) Added attributes to the player and combie game object to work wth the player controller.
2) Added tags to game objects that were used for initiating necessary animations.
3) Added the pointer for shooting along with the bullet.
4) Added hitboxes for the gun pointer to work.
5) Set up aimcamera along with changing the camera positions and sensitivty to improve game experience.

C# scripts worked on
1) PlayerController.cs
2) ZombieController.cs
3) ZombieStats.cs
4) PlayerStats.cs
5) Bullet.cs
6) ThirdPersonShooter.cs

Work done in C#:
1) Designed the combat functionality with different weapons for player character.
2) Implemented the zombie chasing mechanics 
3) Created scripts to ZombieStats.cs and PlayerStats.cs to store the health variables and to handle damage taken from hits.
4) Placed the player object, the enemies and the collectibles in the level 1 scene and tested them.
5) Added Bullet.cs which took care of checking whether the bullet hits the target and defining the speed of the bullet.
6) Implemented the functionality to generate the bullet and to aim the pistol.
7) Improved combat by making sure input is given at fixed intervals.
8) Removed bugs related combat animation calls in PlayerController.cs.


Seyeon Lee
Role: Interactable Asset Design and Sound

Gather and Import all the interactable items and sounds.
Designed and implemented sounds and mixer for each animation, ui, and item interaction.
Implemented collapsing animations for items.

The assets used were:
(file directory: Assets/Raw Models and Textures AND Assets/Ready-to-Use Prefabs)
1) Bat by CGUNWALE from the Unity Asset Store 
2) Food by COOKIEPOPWORKS.COM from the Unity Asset Store 
3) Medkit & Clipboard by COOKIEPOPWORKS.COM from the Unity Asset Store 
4) Pistol by FUN ASSETS from the Unity Asset Store 
5) Water by COOKIEPOPWORKS.COM from the Unity Asset Store 
6) Industrial Props Kit by UNIVERSAL ASSETS from the Unity Asset Store
7) Shuriken pack by 3DIZ-ART from the Unity Asset Store
8) Free Laboratory Pack by STORMBRINGER STUDIOS from the Unity Asset Store
9) Crate and Barrels from KOBRA GAME STUDIOS from the Unity Asset Store
10) PBR Barrels and Crates by INTEGRITY SOFTWARE & GAMES from the Unity Asset Store
11) Flare Gun by ROKAY3D from the Unity Asset Store
12) M26 Grenade by RENOWNED GAMES from the Unity Asset Store
(file directory: Assets/Sounds)
13) background music, crates_collapse, crouching_wood, documents_reading, explosion, gunshot, melee_bat, melee_punch, pickup_bat, pickup_cannedfood, pickup_medkit, pickkup_radio, pickup_waterbottle, player_hurt, repair_generator, scaffolding_collapse, walking_rubble, walking_wood, zomebie_grunt, zombie_hurt Sounds by Zapsplat 

Helped with:
PlayerController.cs
ZombieController.cs
PlayerSound.cs
ZombieSound.cs
HUDManager.cs
IInteractable.cs
ObtainItemInteractable.cs
Interactor.cs
AudioManager.cs

