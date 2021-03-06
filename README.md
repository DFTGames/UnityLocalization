Unity Localization
==================

This project is meant to provide a no-nonsense,
[KISS](https://en.wikipedia.org/wiki/KISS_principle) compliant implementation of
a Localization System for Unity. The main requirements we try to satisfy here
are:

-   Use simple text files, easy to send to translators, no Unity required for
    them

-   Allow full UI localization (including sprites)

The code is published “as is”, exactly the way we use it. Feel free to use it
the way you see fit.

DFT Games Localization Solution
===============================

This package delivers 2 independent features: *) Text localization (both UGUI
and Text Mesh Pro)* ) UI Images localization (2D Sprites)

Two are the steps to implement localization: 1) Prepare the localized files 2)
Add the script to the UI elements you want to localize

### Localized Files

The necessary files must be located in the following folders:

**Resources\\localization**

Here you add the language files. Each file must be named following the scheme
\<languageName\>.txt, so for English you'll create a file named English.txt, for
Italian the file will be Italian.txt and so on.

The file content is:

one key/value per line. valid key/value separator are the equal sign (=) and the
Tab character (0x09). To add a newline simply use the sequence **\\n**

**Resources\\localization\\UI\\\<LanguageName\>**  
Be sure to use the same sprite name for all its version and store each sprite in
its language specific folder.

The **language name** must be one of those listed here:
https://docs.unity3d.com/ScriptReference/SystemLanguage.html

Once you have prepared the files all you have to do is to add the correct script
component to the UI element you want to localize. LocalizeImage for the UI
Images Localize for the UGUI Text LocalizeTM for Text Mesh Pro UGUI
