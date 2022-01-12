This directory holds tower (or turret) related scripts.
Scripts usable by all towers are placed in this folder,
while scripts that define logic exclusive to a certain tower are to be placed in the [`Specific towers`](Specific towers) folder.

Following are simple descriptions of the different scripts usable by every tower.

[`BulletLogic`](BulletLogic.cs)
  - Defines the characteristics of the specific object a tower shoots, like velocity or damage.
    This script is placed on a bullet prefab you want to spawn.

[`ProjectileTowerLogic`](ProjectileTowerLogic.cs)
  - Contains logic for spawning projectiles fired by towers.
    It should be placed on tower prefabs that fire a single projectile at a time.

[`RotatableTowerLogic`](RotatableTowerLogic.cs)
  - Extends `TowerLogic` with logic for rotating towers,
    including displaying a pointer when the player is focusing a tower, which indicates which direction it's firing.
    It should be placed on tower prefabs that fire a single projectile at a time.

[`Tower_SpawnShot`](Tower_SpawnShot.cs)
  - A script to be placed on an empty game object inside a tower prefab to indicate where on the tower model you want the shot to spawn from.

[`TowerLogic`](TowerLogic.cs)
  - Contains basic logic for positioning and focusing towers.
    It should be placed on all tower prefabs.

[`TurretInput`](TurretInput.cs)
  - This script defines how the tower should act when interacted with,
    like being placed on the terrain grid or when receiving inputs from the `PlayerStateController`.
    It should be placed on tower prefabs.

[`TurretInterface`](TurretInterface.cs)
  - This script is a functional interface, and is called by an animation every time the tower should shoot.
    All towers should implement this interface so they can be called by the same function in the animation controller.

[`TurretPrefabConstruction`](TurretPrefabConstruction.cs)
  - A script to be attached to the see-through object that represents a tower when it's previewed in the "Building" state.
