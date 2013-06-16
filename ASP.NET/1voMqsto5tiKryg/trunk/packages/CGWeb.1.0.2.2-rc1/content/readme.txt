Changes from last version:

IMPORTANT: You must now include a call to ServiceManager.Init on app start.
We used to run some initialization code in static constructors but since they are unreliable moved that to an extra method.

Changed the signature ot ServiceManager.RegisterGame. Firstly, the order of type params is now TGame, TGameArgs (it was the opposing way).
Added a third type param TGameHub that is the type of the hub that will serve the game. That way we can now raise game events on the provided
game hub instead of our GameroomHub.

Fixed a bug where js clients werent notified of users leaving/joining rooms.


