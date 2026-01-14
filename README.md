# DFT Games Localization Solution

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Quick Start](#quick-start)
- [Setup Guide](#setup-guide)
  - [Text Localization Files](#text-localization-files)
  - [UI Image Localization Files](#ui-image-localization-files)
- [Localization Scripts](#localization-scripts)
- [Language Selection System](#language-selection-system)
- [TextMeshPro Utility](#textmeshpro-utility)
- [Best Practices](#best-practices)
- [Troubleshooting](#troubleshooting)

## Introduction

The DFT Games Localization Solution is a comprehensive Unity package that enables multi-language support for your Unity projects. It provides an easy-to-use system for localizing both text and images in your game's user interface.

## Features

- **Text Localization**
  - Full support for Unity's UGUI Text components
  - Full support for TextMeshPro (TMP) components
  - Simple key-value pair format for translation files
  - Support for multi-line text
  
- **UI Image Localization**
  - Language-specific 2D sprite support
  - Automatic sprite swapping based on selected language
  
- **Language Management**
  - Automatic language detection based on system language
  - Persistent language preference storage using Unity's PlayerPrefs
  - Dynamic language switching at runtime
  - Fallback to English if translation is missing

## Quick Start

1. **Create language files** in `Resources/localization/` folder (e.g., `English.txt`, `Spanish.txt`)
2. **Add localization scripts** to your UI components:
   - `Localize` for UGUI Text
   - `LocalizeTMPro` for TextMeshPro
   - `LocalizeImage` for UI Images
3. **Set the localization key** in the component inspector
4. **Run your game** - the system will automatically load the appropriate language

## Setup Guide

### Text Localization Files

#### File Location

Create your language files in the following directory:

```
Resources/localization/
```

#### Naming Convention

Each language file must be named using Unity's `SystemLanguage` enum values followed by `.txt`:

- `English.txt`
- `Spanish.txt`
- `French.txt`
- `German.txt`
- `Italian.txt`
- `Japanese.txt`
- `ChineseSimplified.txt`
- etc.

> **Important:** Language names must match exactly with Unity's [SystemLanguage enum](https://docs.unity3d.com/ScriptReference/SystemLanguage.html).

#### File Format

Each line in the file contains a single key-value pair using one of these separators:
- Equal sign: `=`
- Tab character: `\t`

**Syntax:**
```
key=value
key	value
```

**Special Characters:**
- Use `\n` for line breaks within text
- Keys are case-sensitive
- Empty lines are ignored

#### Example Language File

**English.txt:**
```
welcome=Welcome to the Game!
start_game=Start Game
exit_game=Exit
settings=Settings
confirm_exit=Are you sure you want to exit?
multiline_text=This is line one\nThis is line two\nThis is line three
player_score=Score: {0}
```

**Spanish.txt:**
```
welcome=¡Bienvenido al Juego!
start_game=Iniciar Juego
exit_game=Salir
settings=Configuración
confirm_exit=¿Estás seguro de que quieres salir?
multiline_text=Esta es la línea uno\nEsta es la línea dos\nEsta es la línea tres
player_score=Puntuación: {0}
```

### UI Image Localization Files

#### Directory Structure

Create language-specific folders for your localized sprites:

```
Resources/localization/UI/<LanguageName>/
```

#### Example Structure

```
Resources/
??? localization/
    ??? UI/
        ??? English/
        ?   ??? logo.png
        ?   ??? button_start.png
        ?   ??? icon_flag.png
        ??? Spanish/
        ?   ??? logo.png
        ?   ??? button_start.png
        ?   ??? icon_flag.png
        ??? French/
            ??? logo.png
            ??? button_start.png
            ??? icon_flag.png
```

> **Important:** Sprite files must have identical names across all language folders.

## Localization Scripts

### LocalizeImage

**Purpose:** Localizes UI Image components with language-specific sprites.

**Usage:**
1. Add the `LocalizeImage` component to a GameObject with a `UI.Image` component
2. Set the `Image Name` field to match the sprite filename (without extension)
3. The component will automatically load the correct sprite based on the current language

**Inspector Properties:**
- `Image Name`: The name of the sprite file (without path or extension)

### Localize

**Purpose:** Localizes Unity's UGUI Text components.

**Usage:**
1. Add the `Localize` component to a GameObject with a `Text` component
2. Set the `Localization Key` field to match a key from your language files
3. The text will automatically update to the translated value

**Inspector Properties:**
- `Localization Key`: The key to look up in the language file

### LocalizeTMPro

**Purpose:** Localizes TextMeshPro UGUI components.

**Usage:**
1. Add the `LocalizeTMPro` component to a GameObject with a `TextMeshProUGUI` component
2. Set the `Localization Key` field to match a key from your language files
3. The text will automatically update to the translated value

**Inspector Properties:**
- `Localization Key`: The key to look up in the language file

## Language Selection System

### How Language is Determined

The localization system uses the following priority order:

1. **Saved Preference** (Highest Priority)
   - Checks Unity's PlayerPrefs for a saved language preference
   - Key: `Language` (stored via PlayerPrefs)

2. **System Language** (Medium Priority)
   - Detects the operating system language using `Application.systemLanguage`
   - Only used if a corresponding translation file exists

3. **English Fallback** (Lowest Priority)
   - Defaults to English if no saved preference exists and system language file is not found
   - English is the default fallback language

### Changing Language at Runtime

To change the language programmatically:

```csharp
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public void SetLanguage(SystemLanguage language)
    {
        // Change the language
        Locale.CurrentLanguage = language;
        
        // Language preference is automatically saved to PlayerPrefs
        // All localization components will update automatically
    }
    
    public void SetEnglish()
    {
        SetLanguage(SystemLanguage.English);
    }
    
    public void SetSpanish()
    {
        SetLanguage(SystemLanguage.Spanish);
    }
}
```

### Persistence

The selected language is automatically saved to Unity's PlayerPrefs system and will persist across game sessions. No additional code is required to save or load the language preference.

## TextMeshPro Utility

### mergeAllText.ps1 Script

**Location:**
```
Assets/DFTGames/Localization/Demo/Resources/localization/
```

**Purpose:**
- Merges all `.txt` language files into a single `AllText.txt` file
- Useful for generating TextMeshPro font atlases with only the required characters
- Reduces font atlas size and improves performance

**How to Use:**

1. Navigate to the script directory in Windows Explorer
2. Right-click on `mergeAllText.ps1`
3. Select "Run with PowerShell"
4. The script will create `AllText.txt` in the same directory

**Using with TMPro Font Asset Creator:**

1. Run the `mergeAllText.ps1` script to generate `AllText.txt`
2. Open Window ? TextMeshPro ? Font Asset Creator
3. In the "Character Set" dropdown, select "Characters from File"
4. Select the generated `AllText.txt` file
5. Generate the font atlas - it will only include characters used in your localized text

> **Note:** This script only works on Windows systems with PowerShell.

## Best Practices

### File Organization

- Keep all language files in `Resources/localization/`
- Use consistent key names across all language files
- Document your keys in a shared spreadsheet or document
- Use descriptive key names (e.g., `menu_start_game` instead of `msg1`)

### Key Naming Conventions

```
# Good examples
ui_button_start=Start
ui_button_exit=Exit
menu_title_main=Main Menu
dialog_confirm_quit=Are you sure you want to quit?
error_network_connection=Unable to connect to server

# Avoid
btn1=Start
txt2=Exit
msg=Main Menu
```

### Testing Multiple Languages

1. Create a debug menu with language selection buttons
2. Test all UI elements in each language
3. Check for text overflow or truncation issues
4. Verify that all images load correctly for each language

### Performance Tips

- Use the TextMeshPro utility to minimize font atlas size
- Avoid creating language files with duplicate keys
- Load only the necessary language files at runtime
- Cache frequently accessed localized strings

## Troubleshooting

### Text Not Updating

**Problem:** Text remains in English or doesn't change when language is switched.

**Solutions:**
- Verify that the localization key exists in all language files
- Check that the key name is spelled correctly (case-sensitive)
- Ensure the language file is named correctly (e.g., `Spanish.txt`, not `spain.txt`)
- Verify the file is in the `Resources/localization/` folder

### Images Not Loading

**Problem:** UI images don't change or show as missing sprites.

**Solutions:**
- Verify that sprite files have identical names across all language folders
- Check that the folder structure matches: `Resources/localization/UI/<LanguageName>/`
- Ensure the `Image Name` field matches the sprite filename exactly (without extension)
- Verify that the sprites are imported correctly in Unity

### Language File Not Found

**Problem:** Game defaults to English even though language files exist.

**Solutions:**
- Verify the language name matches Unity's `SystemLanguage` enum exactly
- Check for typos in the filename (e.g., `Englsh.txt` instead of `English.txt`)
- Ensure files are in the correct directory: `Resources/localization/`
- Check that files have the `.txt` extension

### Special Characters Not Displaying

**Problem:** Special characters (accents, umlauts, etc.) show as boxes or question marks.

**Solutions:**
- Save language files with UTF-8 encoding
- For TextMeshPro, ensure your font atlas includes the required characters
- Use the `mergeAllText.ps1` utility to generate a complete character set
- Consider using a font that supports international characters

### Language Resets After Restart

**Problem:** Selected language doesn't persist between game sessions.

**Solutions:**
- Verify that PlayerPrefs are being saved correctly
- Check that the game has write permissions for PlayerPrefs
- Ensure you're not clearing PlayerPrefs during initialization
- Test on the target platform (some platforms have PlayerPrefs restrictions)

### Performance Issues

**Problem:** Language switching causes lag or frame drops.

**Solutions:**
- Ensure localization files are not too large
- Consider lazy-loading language data
- Cache commonly used translations
- Optimize TextMeshPro font atlases using the merge utility

---

## Support

For issues, questions, or contributions, please visit the [DFT Games Unity Localization repository](https://github.com/DFTGames/UnityLocalization).

## License

Please refer to the LICENSE file in the project repository
