# Windows Keyboard Layout Input Switcher/Changer

A small Windows command-line tool that switches the active keyboard layout to the desired input language.

Use it from a shortcut, a hotkey, or a script when you want a specific layout applied immediately — for example before launching a game.

## System requirements

- Windows 10 or Windows 11
- .NET Framework 4.x (included with Windows)
- The target keyboard layout must already be installed in Windows Settings

This tool does not install languages or keyboard packs. It only activates a layout that is already available on the system.

## Installation

1. Download `KeyboardLayoutSwitcher.exe` from Releases, or build it from source.
2. Copy the exe to any folder you want, for example:

       D:\Tools\KeyboardLayoutSwitcher\KeyboardLayoutSwitcher.exe

3. Optional: create a shortcut and assign a hotkey.
4. Optional: call it from a `.bat` file before starting another program.

No installer is required. The program writes nothing to Program Files and does not need administrator rights.

## How to use
- If you run the Program without any arguments, it will try to switch currently Keyboard Input to English (United States). If layout is not installed, nothing will happen.
- Click the window that should receive the new layout, then run:

```bat
KeyboardLayoutSwitcher.exe 00000409
```

## How Find codes for layouts already installed on your PC

To list the codes:

1. Open PowerShell.
2. Run:

    Get-WinUserLanguageList

Example output:

    LanguageTag     : en-US
    Autonym         : English (United States)
    EnglishName     : English
    LocalizedName   : English (United States)
    ScriptName      : Latin
    InputMethodTips : {0409:00000409}
    Spellchecking   : True
    Handwriting     : False

Use the number after the colon in InputMethodTips.

- `{0409:00000409}` → `KeyboardLayoutSwitcher 00000409`

For US English, the code is `00000409`.

## Examples

    KeyboardLayoutSwitcher
    KeyboardLayoutSwitcher 00000409
    KeyboardLayoutSwitcher 00000809
    KeyboardLayoutSwitcher 0000040D
    KeyboardLayoutSwitcher 00000401

- `00000409` — English (United States)
- `00000809` — English (United Kingdom)
- `0000040D` — Hebrew
- `00000401` — Arabic
- `00000407` — German
- `0000040C` — French
- `00000419` — Russian

These codes work only if that keyboard is already installed.

These argument forms are also accepted:

    KeyboardLayoutSwitcher 0409
    KeyboardLayoutSwitcher 0x0409

## Notes

- If no arguments is passed, the program will try to switch to English (United States) by default.
- These codes work only if that keyboard is already installed.
- If nothing changes, the layout is not installed, or another window had focus when the program ran.

## Build from source

- The project is a single C# file compiled with the .NET Framework csc compiler that ships with Windows.
- launch command line tool and CD into the folder where the KeyboardLayoutSwitcher.cs file exists, then run the following command

```bat
"%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /target:winexe /out:KeyboardLayoutSwitcher.exe KeyboardLayoutSwitcher.cs
```
- you should find an .exe file generated
- make sure you have .NET Framework 4.x (should be included with your Windows 10/11)

