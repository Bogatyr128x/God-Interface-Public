using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuFabricator : InterfaceFabricator
{
    private static uint SaveManagerMenuUID;
    private static uint MainMenuUniqueID;
    private static uint UniverseCreatorMenuUID;
    private static uint UniverseGeneratorSheetUID;
    private static uint OptionsMenuUID;
    private static uint ModsManagerMenuUID;

    public static uint GetMainMenuUID()
    {
        return MainMenuUniqueID;
    }
    public static uint GetOptionsMenuUID()
    {
        return OptionsMenuUID;
    }
    public static uint GetModsManagerMenuUID()
    {
        return ModsManagerMenuUID;
    }
    public static uint GetSaveManagerMenuUID()
    {
        return SaveManagerMenuUID;
    }

    public static uint GetUniverseCreatorUID()
    {
        return UniverseCreatorMenuUID;
    }
    public static uint GetUniverseGeneratorSheetUID()
    {
        return UniverseGeneratorSheetUID;
    }
    public static GodInterfaceMenu CreateUniverseGeneratorSheet(GodInterfaceMenu menuRef, InterfaceFraming.BaseUIFrames frameToPlace)
    {
        KeyIndexer keyIndexer = new KeyIndexer();
        UniverseGeneratorSheetUID = menuRef.GetIdentifier();
        menuRef.CurrentFrame = frameToPlace;
        menuRef.gameObject.name = "UniverseGeneratorSheet";
        GodInterfaceButton universenameInputfield = GetUserInputField
            (
            UniverseGeneratorSheetUID, 
            "Universe name", 
            "Name your universe", 
            keyIndexer.GetNextKeyCode()
            );

        universenameInputfield.gameObject.GetComponentInChildren<InputField>().onEndEdit.AddListener(delegate
        {
            UniverseSimulation.SetCurrentUniverseName(universenameInputfield.gameObject.GetComponentInChildren<InputField>().text);
        });

        menuRef.AddMenuButton(universenameInputfield) ;

        menuRef.AddMenuButton(
            GetButton(
                UniverseGeneratorSheetUID, "Add black hole", keyIndexer.GetNextKeyCode())
            );
        menuRef.AddMenuButton(
            GetButton(
                UniverseGeneratorSheetUID, "Add star", keyIndexer.GetNextKeyCode())
            );
        menuRef.AddMenuButton(
            GetButton(
                UniverseGeneratorSheetUID, "Add planet", keyIndexer.GetNextKeyCode())
            );
        menuRef.AddMenuButton(
            GetButton(
                UniverseGeneratorSheetUID, "Save this universe", keyIndexer.GetNextKeyCode())
            );
        menuRef.AddMenuButton(
            GetButton(
                UniverseGeneratorSheetUID, "Close", keyIndexer.GetLastKey())
            );
        return menuRef;
    }
    public static GodInterfaceMenu CreateMainMenu(GodInterfaceMenu menuRef, InterfaceFraming.BaseUIFrames frameToPlace)
    {
        KeyIndexer keyIndexer = new KeyIndexer();
        MainMenuUniqueID = menuRef.GetIdentifier();
        menuRef.CurrentFrame = frameToPlace;
        menuRef.gameObject.name = "MainMenu";
        menuRef.AddMenuElement(GetTitle("God Interface"));
        GodInterfaceButton continueButton = GetButton(menuRef.menuIdentifier, "Continue", keyIndexer.GetNextKeyCode());
        GodInterfaceButton newGamebutton = GetButton(menuRef.menuIdentifier, "New game", keyIndexer.GetNextKeyCode());
        GodInterfaceButton optionsButton = GetButton(menuRef.menuIdentifier, "Options", keyIndexer.GetNextKeyCode());
        GodInterfaceButton modsButton = GetButton(menuRef.menuIdentifier, "Mods", keyIndexer.GetNextKeyCode());
        GodInterfaceButton exitButton = GetButton(menuRef.menuIdentifier, "Exit", keyIndexer.GetLastKey());
        menuRef.AddMenuButton(continueButton);
        menuRef.AddMenuButton(newGamebutton);
        menuRef.AddMenuButton(optionsButton);
        menuRef.AddMenuButton(modsButton);
        menuRef.AddMenuButton(exitButton);
        return menuRef;
    }
    public static GodInterfaceMenu CreateUniverseCreatorMenu(GodInterfaceMenu menuRef, InterfaceFraming.BaseUIFrames frameToPlace)
    {
        KeyIndexer keyIndexer = new KeyIndexer();
        UniverseCreatorMenuUID = menuRef.GetIdentifier();
        menuRef.CurrentFrame = frameToPlace;
        menuRef.gameObject.name = "UniverseCreator";
        menuRef.AddMenuElement(GetTitle("Creator"));
        GodInterfaceButton continueButton = GetButton(menuRef.menuIdentifier, "New universe", keyIndexer.GetNextKeyCode());
        GodInterfaceButton exitButton = GetButton(menuRef.menuIdentifier, "Return", keyIndexer.GetLastKey());
        menuRef.AddMenuButton(continueButton);
        menuRef.AddMenuButton(exitButton);
        return menuRef;
    }

    public static GodInterfaceMenu CreateSaveManagerMenu(GodInterfaceMenu menuRef, InterfaceFraming.BaseUIFrames frameToPlace)
    {
        KeyIndexer keyIndexer = new KeyIndexer();
        SaveManagerMenuUID = menuRef.GetIdentifier();
        menuRef.CurrentFrame = frameToPlace;
        menuRef.gameObject.name = "SaveManager";
        menuRef.AddMenuElement(GetTitle("Save Manager"));
        GodInterfaceButton continueButton = GetButton(menuRef.menuIdentifier, "Continue current timeline", keyIndexer.GetNextKeyCode());
        GodInterfaceButton exitButton = GetButton(menuRef.menuIdentifier, "Return", keyIndexer.GetLastKey());
        menuRef.AddMenuButton(continueButton);
        menuRef.AddMenuButton(exitButton);
        return menuRef;
    }
    public static GodInterfaceMenu CreateOptionsMenu(GodInterfaceMenu menuRef, InterfaceFraming.BaseUIFrames frameToPlace)
    {
        KeyIndexer keyIndexer = new KeyIndexer();
        OptionsMenuUID = menuRef.GetIdentifier();
        menuRef.CurrentFrame = frameToPlace;
        menuRef.gameObject.name = "OptionsMenu";
        menuRef.AddMenuElement(GetTitle("Game Options"));
        GodInterfaceButton continueButton = GetButton(menuRef.menuIdentifier, "Continue", keyIndexer.GetNextKeyCode());
        GodInterfaceButton exitButton = GetButton(menuRef.menuIdentifier, "Return", keyIndexer.GetLastKey());
        menuRef.AddMenuButton(continueButton);
        menuRef.AddMenuButton(exitButton);
        return menuRef;
    }
    public static GodInterfaceMenu CreateModManagerMenu(GodInterfaceMenu menuRef, InterfaceFraming.BaseUIFrames frameToPlace)
    {
        KeyIndexer keyIndexer = new KeyIndexer();
        ModsManagerMenuUID = menuRef.GetIdentifier();
        menuRef.CurrentFrame = frameToPlace;
        menuRef.gameObject.name = "ModManager";
        menuRef.AddMenuElement(GetTitle("Mod Manager"));
        GodInterfaceButton continueButton = GetButton(menuRef.menuIdentifier, "Continue", keyIndexer.GetNextKeyCode());
        GodInterfaceButton exitButton = GetButton(menuRef.menuIdentifier, "Return", keyIndexer.GetLastKey());
        menuRef.AddMenuButton(continueButton);
        menuRef.AddMenuButton(exitButton);
        return menuRef;
    }

}
