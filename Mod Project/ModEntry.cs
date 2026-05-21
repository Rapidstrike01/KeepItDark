using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Framework;
using StardewValley;

namespace KeepItDark
{
    public class ModEntry : Mod
    {
        private ModConfig Config;
        private GenericModConfigMenuAPI? ConfigMenuApi;

        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();  // Loads your config.json!

            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.GameLaunched += OnGameLaunched;
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            // Optional GMCM slider
            ConfigMenuApi = helper.ModRegistry.GetApi<GenericModConfigMenuAPI>("spacechase0.GenericModConfigMenu");
            if (ConfigMenuApi is null) return;

            ConfigMenuApi.RegisterModConfig(
                ModManifest,
                () => Config = new ModConfig(),  // Reset button
                () => helper.WriteConfig(Config)  // Save button
            );

            // In-game slider: 400-800 ticks (4am-8am)
            ConfigMenuApi.RegisterClampedOption(
                ModManifest,
                "SunriseTime",
                "Ticks until dawn (600=6AM). Early rise? Go low! 🌅",
                () => Config.SunriseTime,
                (int val) => Config.SunriseTime = val,
                400,
                800
            );
        }

        private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
        {
            if (!e.IsMultipleOf(5)) return;  // Perf boost

            if (!Context.IsWorldReady || Game1.timeOfDay >= Config.SunriseTime) return;

            // FORCE PITCH BLACK 🖤
            Game1.lightGlow = 1f;
            Game1.lightFade = 0f;
            Game1.dawnId = -1;
            Game1.dawnAlpha = 0f;
            Game1.outdoorLight = Color.Black;
        }
    }
