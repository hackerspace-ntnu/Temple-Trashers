This directory contains general UI-related scripts.

ResourceUI is responsible for handling the UI that shows the resource amount in game. It does this by instantiating an empty game object every time the game starts and updates the resource amount when prompted.

UIControllerWheel handles the inputs from the player when using the tower wheel UI. This script must be attached to the RadialUiPlayer prefab (Or similar radial segments selection menus, aka selection wheels) for it to work.  

The FlexibleUI contains scripts that define a general UI that could be used to give skins to all menu based UI (Like main menu or pause screen) using scriptable objects, much like how tower scriptable objects work. These are however unfinished and still unused.
