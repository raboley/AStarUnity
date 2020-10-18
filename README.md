# AStartUnity


## Introduction

This repo uses the Unity game engine to visualize path finding routes in 3D with a goal of creating a pathfinding algorithm implementation that can deal with 3D spaces with varying heights. Most implementations of pathfinding focus on 1 plane, but I want to expand to multiple. Eventually I want to add multiple planes depending on heights such as floors of a building, or mountains with cliffs and overhangs.

The Goals of this project are to create an implementation that works in Unity and can be translated for use by EasyFarm in FFXI boting to move pathfinding forward for my bots in that game. Features that I want to add to make this robust.

### Individual Features

1. Given a starting point and an end point travel from Start -> End
1. If the Map Is unknown, Create a map the size of the points between start and end.
1. If obstacles are discovered on the way, Record Obstacles and re-calculate path.
1. Create Explore mode that finds the Edges of a map recording them somewhere that can be drawn in unity later.
1. Add understanding of Zones, so that a character can record when it has traveled to a different zone and mark zone boundaries on the map.
1. Add the understanding of world map, so given a destination is in a given zone multiple zones away, it can path find to the next closest zone until it reaches the correct zone, then path find to the destination.
1. Add teleportation to AI understanding
    1. Warp home
    1. Warp to Field using NPC?
    1. Warp to Place using Item?
    1. Get On a boat (super stretch goal???)
1. Add ability to locate and Open Doors
1. Add ability to understand elevators (might have to be manually noted, idk how to do this programatically)
1. Add ability to avoid monsters that will aggro when in the field.

### Party Features

1. Follow Party Leader
1. Entire Party Rests when 1 Party member rests.

## How to Run

To run this locally download Unity and open this folder in it.
After that click play and you should see multiple units called Seekers moving toward a Target.

Done! 

## Implementation

The implementation is A* Algorithm using a Heap to store the openset, and a PathfindingRequestManager to handle multiple requests at a time. The Grid is generated based on the A* Object in unity, that is a plane the size of the map, and then the unwalkable layer is added to objects that are unwalkable. Then as Pathfinding goes through the list of Nodes it will generate a list of lowest cost moves to get to the destination, and when the path is found, it reverses that path to get the route the unit should take. Units then move toward the target until it is reached.