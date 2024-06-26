#!/bin/bash
set -e

MOD_DLL="pi_utils"
GAME_PATH="/c/Program Files (x86)/Steam/steamapps/common/Techtonica"

# Build the project
echo "Building the project..."
cd plugin
dotnet build
cd ..

# Cleanup
echo "Cleaning up..."
rm -rf "$GAME_PATH/BepInEx/plugins/$MOD_DLL"
mkdir "$GAME_PATH/BepInEx/plugins/$MOD_DLL"

# Copy the mod dll to the mods folder
echo "Copying the mod dll to the mods folder..."
cp ./plugin/bin/Debug/netstandard2.1/$MOD_DLL.* "$GAME_PATH/BepInEx/plugins/$MOD_DLL/"
