# EyeTrackerDataVisualizer
 
This tool is intended to replay eye-tracker games and should be used on data gathered using [ObjectTracking](https://github.com/YellowEmerald5/ObjectTracking). The database manager gathers data from the database. Change the variables in the start of the script to change the database it looks for.

Storages should be reset except for images, videos and colors before the game is built to ensure propper behaviour. Images and videos for overlay must be added to the images and videos storage scriptable object to be prepared and loaded into the scroll view and dropdown menu respectivly. NB! Videos are not yet supported as it was not a priority for this Project.