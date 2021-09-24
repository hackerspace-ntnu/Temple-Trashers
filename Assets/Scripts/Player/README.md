This directory holds the scripts that are either tied directly to the player character(like PlayerUi), or those that tie the player to the rest of the game (like CameraFocusController).


The PlayerStateController-script is one of the most important scripts to understand if you want the player to *anything* in the game, thus I'll give as concise an explanation I can muster. The Player can be set to be in different states. These states either enables a set of actions the player can do at a certain time, or handle how those actions unfold.

As an example, the player can be set to the state called "Building". In this state, the player must carry a model which, which then can be placed by performing the interact-action. The player can still walk around, but cannot control/rotate already placed towers. When the player places or cancels out of the "Building" state, they will be returned to the "Free" state, in which they can walk freely and do whatever is defined there.

If you have any further questions regarding the PlayerStateController, don't hesitate to ask Sondre, Fredrik or Thomas.
