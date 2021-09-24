
This directory holds the logic for gameobjects that can be interacted with, which in practice means that it does something when you press interact. These objects, for example loot, inherit from the class "Interactable", which gives them the properties they need to, as yet another example, be carried by a player. You can find the logic that handles the "Interactable-logic" for players in the "PlayerStateController"-script in the "Player" directory.

Scripts tied to how different parts of the game be interacted with by players in-game can be placed here if they don't have any more obvious places to be.
