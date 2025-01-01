**DFT Games Localization Solution**

[TOC]

# Introduction

This package provides two core features:

1. **Text Localization**: Supports both UGUI and TextMeshPro.
2. **UI Image Localization**: Handles 2D Sprites for different languages.

The localization process involves two steps:

1. Prepare the localized files.
2. Attach the appropriate localization script to the UI elements you want to localize.

# Localized Files Location

Localized files must be placed in the following folders:

### Text Localization

**Path:** `Resources/localization`

- Each language requires a file named in the format `LanguageName.txt`. For example:
  - English: `English.txt`
  - Italian: `Italian.txt`
- File Format:
  - Each line contains a key-value pair separated by either an equal sign (`=`) or a Tab character (`\t`).
  - To include a new line in the value, use `\n`.

**Example:**

```
welcome=Welcome
exit=Exit
```

### UI Image Localization

**Path:** `Resources/localization/UI/<LanguageName>`

- Each sprite should have the same name across all language-specific folders.
  - Example:
    - `Resources/localization/UI/English/mySprite.png`
    - `Resources/localization/UI/Italian/mySprite.png`

The language name **must match** one of the values listed in Unity's `SystemLanguage` enumeration. Refer to the documentation: [SystemLanguage Enum](https://docs.unity3d.com/ScriptReference/SystemLanguage.html).

# Utility for TextMeshPro

A PowerShell script, **mergeAllText.ps1**, is available in the folder:

```
Assets/DFTGames/Localization/Demo/Resources/localization
```

This script:

- Merges all `.txt` files in the folder into a single Unicode text file named `AllText.txt`.
- This file can be used with the TMPro Font Asset Creator to generate a character map restricted to the localized text.

**Usage:** Execute the script on Windows to automate the process.

# Scripts for Localization

Add the appropriate script component to the UI element to enable localization:

- **LocalizeImage**: For UI Images.
- **Localize**: For UGUI Text.
- **LocalizeTMPro**: For TextMeshPro UGUI.

# Language Preference Handling

The localization system determines the player's language preference as follows:

1. If a language preference is stored in Unity's PlayerPrefs, it uses that language.
2. If no PlayerPrefs preference exists, it defaults to the operating system language, provided the corresponding translation file is available.
3. If the operating system language file is not found, it defaults to English.

The system automatically saves the selected language to Unity's PlayerPrefs game saving system. This ensures the player's language preference persists across game sessions.

# Additional Notes

- All localization scripts handle language changes dynamically, updating the UI elements in real-time.
- Ensure all localization files and assets are properly organized within the designated folder structure to avoid runtime issues.
- Debugging Tips:
  - Missing language files or incorrect folder structures will result in fallback to the English localization.
