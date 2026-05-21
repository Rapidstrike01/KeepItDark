using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

public interface GenericModConfigMenuAPI
{
    void RegisterModConfig(IManifest mod, Action revertToDefault, Action saveToFile);
    void UnregisterModConfig(IManifest mod);
    void SetDefaultIngameOptinValue(IManifest mod, bool optedIn);
    void StartNewPage(IManifest mod, string pageName);
    void OverridePageDisplayName(IManifest mod, string pageName, string displayName);
    void RegisterLabel(IManifest mod, string labelName, string labelDesc);
    void RegisterPageLabel(IManifest mod, string labelName, string labelDesc, string newPage);
    void RegisterParagraph(IManifest mod, string paragraph);
    void RegisterImage(IManifest mod, string texPath, Rectangle? texRect = null, int scale = 4);
    void RegisterSimpleOption<T>(IManifest mod, string optionName, string optionDesc, Func<T> optionGet, Action<T> optionSet);
    void RegisterClampedOption(IManifest mod, string optionName, string optionDesc, Func<int> optionGet, Action<int> optionSet, int min, int max);
    void RegisterClampedOption(IManifest mod, string optionName, string optionDesc, Func<float> optionGet, Action<float> optionSet, float min, float max);
    void RegisterChoiceOption(IManifest mod, string optionName, string optionDesc, Func<string> optionGet, Action<string> optionSet, string[] choices);
    void SubscribeToChange(IManifest mod, Action<string, bool> changeHandler);
    void SubscribeToChange(IManifest mod, Action<string, int> changeHandler);
    void SubscribeToChange(IManifest mod, Action<string, float> changeHandler);
    void SubscribeToChange(IManifest mod, Action<string, string> changeHandler);
    void OpenModMenu(IManifest mod);
}