Simple Footsteps Redux - by Quentin of Chisel Tools for Kai of CoB

- to add support for your own mod, include a soundentries.json in your mods config folder
Format is fairly straight forward:

example (note square brackets at start and end of the file and each entry wrapped in {} - should have a comma at the end for multiple lines)
[
  {"mobMatchCode": "drifter","soundTrigger": "wander","soundFile": "simplefootstepsredux:sounds/creature/steps/npc","soundTime": 0.55,"volume": 0.65,"changepitch": true},

]

mobMatchCode - this will pattern match to the mob, so below "drifter" means any mob that contains "drifter" in its id Code
soundTrigger - this is which sound you are referring to, there are 4:
   wander - footsteps played when the mob is wandering around
   fleeentity - footsteps played when the mob is running away
   seekentity - footsteps palyed when the mob is trying to reach a target
   getoutofwater - footsteps made when the mob is walking thru the water
   stayclosetoentity - footsteps made when following another entity
soundFile - this is the sound file, including the mod domain
soundTime - how often to play the sound (smaller is more often) - defaults to 0.5 if not specified
volume (float) - how load to play, 1 is 100% and is the default
changepitch (true/false) - if true, Vintage Story will vary the pitch slightly

Notes:
- if you specify an invalid file name, mod domain, or the mob doesn't match anything it just won't play
- you can include all 4, 1, or no entries for any given mob
- in the case a mob matches multiple entries it will basically use the first rules it finds, which you won't really have control over
