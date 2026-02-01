# Project **Hazard**

## Description

* **Concept**: Online multiplayer, party based, dungeon crawler roguelike.

### Hub
* Every player has their own unique Hub Town that they can use for shopping, online services, character customization, and initiating dungeons.
  * Shopping: There are a series of shops that sell weapons, armors, character cosmetics, and items for use in the dungeons.
  * Online Services: By default, the online services are turned off, allowing for solo play.  There are four modes for online mode.
    * Host Public: This mode opens the player's hub for any three additional players to join them.
    * Host Private: This mode opens the player's hub for friends only to join them.
    * Join Public: This mode searches for any public server for players to join.  The player is then spawned into the Host's hub.
    * Join Private: This mode searches for any friend servers for players to join.  The player is then spawned into the Host's hub.
    * Other online services include: Friend Code/ List, Item Sharing, Party Chat, and Dungeon Library (replaying favorite dungeons).
  * Character Customization: Every player has their own custom character.  They can change the hair color/ style, eye color, skin color, clothes, and in game voice.  The player also makes four presets for Knight, Mage, Ranger, and Barbarian builds.  When entering a dungeon, the character dons the equipment saved in the preset the player chooses to play as.
  * Initiating Dungeons: A Magic Portal or Gate that is interacted with to start a new dungeon run.

### Dungeons
* Dungeons are generated using a seed system, allowing players to replay their favorite levels or share levels with friends.
* All dungeons require four characters to complete.
  * Knight: Sword and Shield
  * Mage: Magic Bolt and Light Burst
  * Ranger: Bow and Arrow
  * Barbarian: Crush and Lift
* Dying and Game Over:
  * If a player dies, they aren't removed from the game, but must instead receive penalties.
    * First, the player drops all of the items and loot they've collected up to that point.  This loot is claimable by any player that comes across it.
    * Second, the player must wait for a short time before being returned to the starting room of the dungeon.  They must then make their way through the dungeon again.
  * If all four players die before any of them could respawn into the starting room, the party experiences a game over and are removed from the dungeon.  Don't worry, the ID seed of the dungeon is still remembered in case the players want to try it again.
* Characters act as keys to specific obstacles, forcing players to work together to complete the dungeon.
  * Enemies
    * Goblin: Use the Knight to parry its attack and then counter attack when it's stunned.
    * Ghost: Only takes damage from Magic bolt.  Use the Mage to defeat it.
    * Ninja: Teleports away from players if they get too close.  Use the Ranger to attack from a distance.
    * Tortoise: Has an armored shell that protects it from all damage.  Use the Barbarian's crushing attack to reveal its weak spot.
  * Doors
    * Overgrowth Door: Use the Knight's sword to cut the vines blocking the door.
    * Warp Door: The door warps players to a random point in the room, preventing them from moving on to the next room.  Use the Mage to "cleanse" the door, allowing players to continue.
    * Watching Door: A massive eye blocks the door, watching the players' every move.  If the players get too close, the eye closes.  Use the Ranger to shoot the eye from a distance, clearing the way.
    * Blocked Door: A simple door with a massive boulder blocking it.  Use the Barbarian to lift and move the boulder to clear the way.

### AI Players
* In the event that there aren't enough players to fill out the ranks of the party, AI bots will join the players.
* AI bots don't just follow the players, but are capable of helping explore the dungeon using a finite state machine.
  * **Regroup**: The default state.  When there is nothing for the AI's character to do, it returns to the Host Player's location.
  * **Explore**: If there is an unexplored room adjacent to the Host Player's location, the AI will explore that room to help expand the dungeon.  If there's nothing for the AI to do in this new room, it returns to the REGROUP state.
  * **Solve**: If there is a door that can only be opened by the AI's character, it will target that location and solve the door's puzzle.  If all door puzzles assigned to the AI's character is complete, it returns to the REGROUP state.
  * **Hunt**: If the Mini Boss's location is discovered, all AIs will target the Mini Boss until it is defeated.  After its defeat, the AIs will target the Boss Key that is dropped by the Mini Boss.
  * **Rally**: If the Boss Key is in the possession of a hero character, the AIs will remain will the Host Player.  This way, when the Host Player is ready to fight the Boss, the AIs are also present.  This also ensures that if the Host Player continues to explore the dungeon to collect all the treasures, the AIs will be present to unlock doors that are assigned to their characters.

## Getting Started

### Requirements

### Installation

1. Clone the repository: 'https://github.com/3Dever96/ProjectHazard.git'
2. Open the project in Unity Editor.

## Contributing

## License

This project is licensed under the MIT License- see the LICENSE file for details.

## Acknowledgments

