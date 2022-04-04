using OpenTK.Graphics.ES11;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LD50
{
    /// <summary>
    /// Enum containing the names of each texture
    /// </summary>
    public enum TexName
    {
        // Test
        TEST,
        PIXEL,
        PLAYER_IDLE,
        // UI
        BUTTON,
        EXIT_BUTTON,
        HUD_BUTTON,
        // Car and other random stuff
        CAR_BASE,
        ROADSIGN,
        // Loot
        FUEL,
        FOOD,
        // Rooms
        ROOM_FOODSTORAGE,
        ROOM_BEDROOM,
        ROOM_FUELSTORAGE,
        ROOM_WEAPONSTORAGE,
        ROOM_WORKSHOP,
        // Backgrounds
        DRIVING_BG,
        AMBUSH_BG1,
        LOOT_BG,
        DRAGON_BG,
        // Enemies
        DRAGON,
        FISHBOWL_IDLE,
        FISHBOWL_WALK,
        SLIME_IDLE,
        SHEEP_WALK,
        ROCK_WALK,
        FISHBOWL_WALK_FLIPPED,
        SLIME_IDLE_FLIPPED,
        SHEEP_WALK_FLIPPED,
        ROCK_WALK_FLIPPED,
        // Persons
        PERSON_PORTRAIT,
        PERSON_IDLE,
        PERSON_WALK,
        PERSON_WALK_FLIPPED,
        // Guns
        BULLET,
        BASE_GUN,
        BETTER_GUN,
        FASTER_GUN,
        SNIPER_GUN,
        ROCKET_GUN,
        BASE_GUN_FLIPPED,
        BETTER_GUN_FLIPPED,
        FASTER_GUN_FLIPPED,
        SNIPER_GUN_FLIPPED,
        ROCKET_GUN_FLIPPED,
    }
    public class TextureList
    {
        private Dictionary<TexName, Texture> _textures;

        /// <summary>
        /// Create a List to hold all loaded Textures
        /// </summary>
        public TextureList()
        {
            _textures = new Dictionary<TexName, Texture>();
            Init();
        }

        /// <summary>
        /// Load all the Textures
        /// </summary>
        public void Init()
        {
            LoadTexture("Sprites/Test/Test.png", TexName.TEST);
            LoadTexture("Sprites/Test/Pixel.png", TexName.PIXEL);
            LoadTexture("Sprites/Test/PlayerIdle.png", TexName.PLAYER_IDLE);
            LoadTexture("Sprites/Rooms/FoodStorage.png", TexName.ROOM_FOODSTORAGE);
            LoadTexture("Sprites/Rooms/WeaponStorage.png", TexName.ROOM_WEAPONSTORAGE);
            LoadTexture("Sprites/Rooms/Bedroom.png", TexName.ROOM_BEDROOM);
            LoadTexture("Sprites/Rooms/FuelStorage.png", TexName.ROOM_FUELSTORAGE);
            LoadTexture("Sprites/Rooms/Workshop.png", TexName.ROOM_WORKSHOP);
            LoadTexture("Sprites/CarBase.png", TexName.CAR_BASE);
            LoadTexture("Sprites/Backgrounds/DrivingSceneBG.png", TexName.DRIVING_BG);
            LoadTexture("Sprites/Enemies/DragonBW.png", TexName.DRAGON);
            LoadTexture("Sprites/Enemies/FishbowlIdleTwoFrames.png", TexName.FISHBOWL_IDLE);
            LoadTexture("Sprites/Enemies/FishbowlWalkTwoFrames.png", TexName.FISHBOWL_WALK_FLIPPED);
            LoadTexture("Sprites/Enemies/SheepWalkTwoFrames.png", TexName.SHEEP_WALK_FLIPPED);
            LoadTexture("Sprites/Enemies/RockWalkTwoFrames.png", TexName.ROCK_WALK_FLIPPED);
            LoadTexture("Sprites/Enemies/SlimeIdleTwoFrames.png", TexName.SLIME_IDLE_FLIPPED);
            LoadTexture("Sprites/Enemies/FishbowlWalkTwoFramesFlipped.png", TexName.FISHBOWL_WALK);
            LoadTexture("Sprites/Enemies/SheepWalkTwoFramesFlipped.png", TexName.SHEEP_WALK);
            LoadTexture("Sprites/Enemies/RockWalkTwoFramesFlipped.png", TexName.ROCK_WALK);
            LoadTexture("Sprites/Enemies/SlimeIdleTwoFramesFlipped.png", TexName.SLIME_IDLE);
            LoadTexture("Sprites/Loot/Fuel.png", TexName.FUEL);
            LoadTexture("Sprites/Loot/Food.png", TexName.FOOD);
            LoadTexture("Sprites/Persons/PersonIdleTwoFrames.png", TexName.PERSON_IDLE);
            LoadTexture("Sprites/Persons/PersonWalkTwoFrames.png", TexName.PERSON_WALK);
            LoadTexture("Sprites/Persons/PersonWalkTwoFramesFlipped.png", TexName.PERSON_WALK_FLIPPED);
            LoadTexture("Sprites/Persons/PersonPortrait.png", TexName.PERSON_PORTRAIT);
            LoadTexture("Sprites/Backgrounds/AmbushBG1.png", TexName.AMBUSH_BG1);
            LoadTexture("Sprites/Backgrounds/DragonFightBG.png", TexName.DRAGON_BG);
            LoadTexture("Sprites/Backgrounds/LootBG.png", TexName.LOOT_BG);
            LoadTexture("Sprites/Guns/BaseGun.png", TexName.BASE_GUN);
            LoadTexture("Sprites/Guns/BetterGun.png", TexName.BETTER_GUN);
            LoadTexture("Sprites/Guns/FasterGun.png", TexName.FASTER_GUN);
            LoadTexture("Sprites/Guns/SniperGun.png", TexName.SNIPER_GUN);
            LoadTexture("Sprites/Guns/RocketGun.png", TexName.ROCKET_GUN);
            LoadTexture("Sprites/Guns/BaseGunFlipped.png", TexName.BASE_GUN_FLIPPED);
            LoadTexture("Sprites/Guns/BetterGunFlipped.png", TexName.BETTER_GUN_FLIPPED);
            LoadTexture("Sprites/Guns/FasterGunFlipped.png", TexName.FASTER_GUN_FLIPPED);
            LoadTexture("Sprites/Guns/SniperGunFlipped.png", TexName.SNIPER_GUN_FLIPPED);
            LoadTexture("Sprites/Guns/RocketGunFlipped.png", TexName.ROCKET_GUN_FLIPPED);
            LoadTexture("Sprites/UI/Exitbutton.png", TexName.EXIT_BUTTON);
            LoadTexture("Sprites/UI/HUDButton.png", TexName.HUD_BUTTON);
            LoadTexture("Sprites/Guns/Bullet.png", TexName.BULLET);
            LoadTexture("Sprites/Roadsign.png", TexName.ROADSIGN);
        }

        /// <summary>
        /// Loads a texture from a filepath
        /// </summary>
        /// <param name="path">Path to the texture</param>
        /// <param name="name">Name of the texture</param>
        public void LoadTexture(string path, TexName name)
        {
            Texture texture = Texture.LoadFromFile(path);

            // If the texture already exists, unload it first
            if (_textures.ContainsKey(name))
            {
                GL.DeleteTexture(_textures[name].Handle);
            }
            _textures[name] = texture;
        }

        /// <summary>
        /// Loads a texture from a Bitmap
        /// </summary>
        /// <param name="image">Bitmap</param>
        public Texture LoadTexture(Bitmap image)
        {
            Texture texture = Texture.LoadFromBmp(image, false);

            return texture;
        }

        /// <summary>
        /// Get a loaded texture from a name
        /// </summary>
        /// <param name="name">Name of the texture</param>
        /// <returns><code>Texture</code></returns>
        public Texture GetTexture(TexName name)
        {
            return _textures[name];
        }

        /// <summary>
        /// Unload all textures from memory
        /// </summary>
        public void UnLoad()
        {
            foreach (Texture texture in _textures.Values)
            {
                Globals.Logger.Log($"Unloaded {texture}", utils.LogType.INFO);
                GL.DeleteTexture(texture.Handle);
                Globals.unloaded++;
            }
        }

        /// <summary>
        /// Check if a texture exists in the list
        /// </summary>
        /// <param name="name"><code>string</code> name of the texture</param>
        /// <returns><code>true</code> if the texture exists</returns>
        /// <returns><code>false</code> if the texture doesn't exist</returns>
        public bool CheckIfTextureExists(TexName name)
        {
            return _textures.ContainsKey(name);
        }
    }
}
