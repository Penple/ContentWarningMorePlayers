# MorePlayers

BepInEx mod to increase the player limit in [Content Warning](https://store.steampowered.com/app/2881650/Content_Warning/).

## Installation

Note: This will get quite a bit easier once this game shows up on a mod manager.

1. Download the latest release of [BepInEx 5](https://github.com/BepInEx/BepInEx/releases).
2. Extract all files from BepInEx into the game folder (such that `winhttp.dll` is alongside `Content Warning.exe`)
3. Run the game once to generate the plugins folder.
4. Place the `MorePlayers.dll` from the release into the `BepInEx/plugins` folder.
5. The lobby size should be increased to 32 players.

All players must have the mod installed otherwise they will not spawn in the lobby and video extraction will fail (it's complicated).

## Building from source

1. Make a `lib` folder at the root of this repository.
2. Put the game's `Assembly-CSharp.dll` in `lib` folder.
3. Build the `.csproj` using some flavor of Visual Studio.

## bruh?

i'll put this on a mod store when one exists but it was very important that my group could play this game together day one