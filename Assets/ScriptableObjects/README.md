This directory contains scripts that define scriptable objects, and folders that holds specific instances of the defined scriptable objects.

Scriptable objects are made to streamline the process of making prefabs, both for UI elements like icons and descriptions, and functional variables like cost.

An example of usage of this, is when making a new turret. Instead of having to manually define different costs, icons, models etc., for each system in the game that uses these (Like the in-game turret selection UI) whenever you make add a new turret, you can define a scriptableObject (In this example a TowerObject) to hold this info and automatically and procedurally give that info to all the systems that need it.

Furthermore, to make it even easier to use, you can simply make an instance of a scriptable object in the unity editor by: right-clicking -> create -> "name-of-thing(example:Tower)"ScriptableObject. You can then edit the variables of the object you just created in unity :)



If you want to create new scriptableObject defining scripts, please follow the conventions of the already made script. (Like adding a [CreateAssetMenu] tag to make it easier for others to find by right-clicking). If you this , please follow the naming convention of "<name-of-thing(example:Tower)>ScriptableObject".
