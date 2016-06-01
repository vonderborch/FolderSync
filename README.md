# FolderSync
Quick little program to help keep two folders synced.

# How to Use
(Pre: Download source and build project, or download a copy of the executable directly from the [releases section]( https://github.com/vonderborch/FolderSync/releases) )

1. Copy and paste the executable to some destination
2. Rename the new copy to something unique and descriptive
3. Run the new copy
  1. Enter source folder in the **Source** field
  2. Enter the destination folder in the **Destination** field
  3. Enter the interval between the syncs (in seconds) in the **Interval (S)** field
  4. Select the file override mode in the **Override Mode** combobox. Available Modes:
    1. Never: *Never override files with the same full name in the destination.*
    2. Always: *Always override files with the same full name in the destination.*
    3. SourceNewer: *Override files with the same full name in the destination if the file in the source is newer than the file in the destination.*
  5. Press the **Sync Now / Start Sync** button to begin syncing. The program will now keep syncing as long as you let it run. If you want to sync early (or you have changed some setting), simply re-press this button.
