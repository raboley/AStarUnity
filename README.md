# AStartUnity


## Introduction

This repo uses the Unity game engine to visualize path finding routes in 3D with a goal of creating a pathfinding algorithm implementation that can deal with 3D spaces with varying heights. Most implementations of pathfinding focus on 1 plane, but I want to expand to multiple. Eventually I want to add multiple planes depending on heights such as floors of a building, or mountains with cliffs and overhangs.

The Goals of this project are to create an implementation that works in Unity and can be translated for use by EasyFarm in FFXI boting to move pathfinding forward for my bots in that game. Features that I want to add to make this robust.

### Individual Features

1. Given a starting point and an end point travel from Start -> End
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