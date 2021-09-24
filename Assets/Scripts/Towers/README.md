This directory hold tower(or turret) related scripts. Scripts usable by all towers are placed in this folder, while scripts that define logic exclusive to a certain tower are to be placed in the "Specific towers" folder.

Following are simple descriptions of the different scripts usable by every tower.

BulletLogic
  - Defines the charetristics of the specific object a tower shoots, like velocity or damage. This script is placed on a bullet prefab you want to spawn.
Tower_SpawnShot
  - A script to be placed on an empty game object inside a tower prefab to indicate where on the tower model you want the shot to spawn from.
TowerLogic
  - Holds logic related to how players can interact with the tower by rotating it and holds a lot of variables that, among many things, defines properties like what specific bullet prefab you want to shoot and how fast the tower shoots, etc. This script is to be placed on the tower prefab.
  - TowerLogic also supports having an invisible pointer inside the model to indicate which way a tower is looking towards by turning it visible when a player walks near.
TurretInput
  - This script defines how the tower should act when interacted with, like being placed on the terrain grid or when receiving inputs from the PlayerStateController. This placed should be placed on tower prefabs.
TurretInterface
  - This script is a functional interface, and is called by an animation every time the tower should shoot. All towers should implement this interface so they can be called by the same function in the animation controller.
TurretPrefabConstruction
  - A script to be attached to the see-through object that represents a tower when it's previewed in the "Building" state.
