**DFT Games Localization Solution**

[TOC]

# Introduction

This package delivers 2 independent features:

- Text localization (both UGUI and Text Mesh Pro)
- UI Image localization (2D Sprites)

Two are the steps to implement localization:
 	1) Prepare the localized files
 	2) Add the script to the UI elements you want to localize 

# Localized Files Location

The necessary files must be located in the following folders:

**Resources\localization**

Here you add the language files. Each file must be named following the scheme **LanguageName.txt**, so for English, you'll create a file named **English.txt**; for Italian, the file will be **Italian.txt** and so on.

The file content is structured this way: **one key/value per line**. 

Valid key/value separators are:

- the equal sign
- the Tab character

To add a new line simply use the sequence **\n** in the text

Example: 

	Resources\localization\UI\<LanguageName>
	
	Be sure to use the same sprite name for all its version and store each sprite in its language specific folder.

The language name **must be** one of those listed here: https://docs.unity3d.com/ScriptReference/SystemLanguage.html

# Scripts to use

Once you have prepared the files all you have to do is to add the correct script component to the UI element you want to localize.
	**LocalizeImage** for the UI Images
	**Localize** for the UGUI Text
	**LocalizeTM** for Text Mesh Pro UGUI
