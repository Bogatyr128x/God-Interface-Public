using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigations : InterfaceListener
{

    internal static void ReturnFromThisMenuToThisOtherMenu(uint currentMenu, uint menuToGoTo)
    {
        GodInterfaceMenuPool[(int)menuToGoTo].AddAnimation(InterfaceAnimation.TransitionTo.LeftIn, Configurator.GetDefaultAnimDuration());
        GodInterfaceMenuPool[(int)currentMenu].AddAnimation(InterfaceAnimation.TransitionTo.RightOut, Configurator.GetDefaultAnimDuration());
        SetUIDOfCurrentlyActiveMenu(menuToGoTo);
    }
    internal static void AdvanceFromThisMenuToThisOtherMenu(uint currentMenu, uint menuToGoTo)
    {
        GodInterfaceMenuPool[(int)currentMenu].AddAnimation(InterfaceAnimation.TransitionTo.LeftOut, Configurator.GetDefaultAnimDuration());
        GodInterfaceMenuPool[(int)menuToGoTo].AddAnimation(InterfaceAnimation.TransitionTo.RightIn, Configurator.GetDefaultAnimDuration());
        SetUIDOfCurrentlyActiveMenu(menuToGoTo);
    }

    internal static void MainMenu(int interactionIndex)
    {
        uint mainMenu = MenuFabricator.GetMainMenuUID();

        switch (interactionIndex)
        {
            default:
                break;
            case 0:
                uint saveMgr = MenuFabricator.GetSaveManagerMenuUID();
                AdvanceFromThisMenuToThisOtherMenu(mainMenu, saveMgr);
                break;
            case 1:
                uint universeCreator = MenuFabricator.GetUniverseCreatorUID();
                AdvanceFromThisMenuToThisOtherMenu(mainMenu, universeCreator);
                break;
            case 2:
                uint gameOptionsMenu = MenuFabricator.GetOptionsMenuUID();
                AdvanceFromThisMenuToThisOtherMenu(mainMenu, gameOptionsMenu);
                break;
            case 3:
                uint modManager = MenuFabricator.GetModsManagerMenuUID();
                AdvanceFromThisMenuToThisOtherMenu(mainMenu, modManager);
                break;

        }
    }
    internal static void SaveManager(int interactionIndex)
    {
        uint saveManager = MenuFabricator.GetSaveManagerMenuUID();
        int interactionLimit = GodInterfaceMenuPool[(int)saveManager].GetInteractionLimit();
        if (interactionIndex >= interactionLimit)
        {
            uint mainMenu = MenuFabricator.GetMainMenuUID();
            //Debug.Log("trying to return");
            ReturnFromThisMenuToThisOtherMenu(saveManager, mainMenu);
        }
    }
    internal static void ModManager(int interactionIndex)
    {
        uint modManager = MenuFabricator.GetModsManagerMenuUID();
        int interactionLimit = GodInterfaceMenuPool[(int)modManager].GetInteractionLimit();
        if (interactionIndex >= interactionLimit)
        {
            uint mainMenu = MenuFabricator.GetMainMenuUID();
            ReturnFromThisMenuToThisOtherMenu(modManager, mainMenu);
        }
    }
    internal static void OptionsMenu(int interactionIndex)
    {
        uint optionsMenu = MenuFabricator.GetOptionsMenuUID();
        int interactionLimit = GodInterfaceMenuPool[(int)optionsMenu].GetInteractionLimit();
        if (interactionIndex >= interactionLimit)
        {
            uint mainMenu = MenuFabricator.GetMainMenuUID();
            ReturnFromThisMenuToThisOtherMenu(optionsMenu, mainMenu);
        }
    }
    internal static void UniverseCreatorSheet(int interactionIndex)
    {
        uint genSheet = MenuFabricator.GetUniverseGeneratorSheetUID();
        int interactionLimit = GodInterfaceMenuPool[(int)genSheet].GetInteractionLimit();
        if (interactionIndex >= interactionLimit)
        {
            InterfaceListener.BlockAllButtonPresses = true;
            uint currMenu = GetUIDOfVisibleMenuInThisFrame(InterfaceFraming.BaseUIFrames.Left);
            ViewportResizer.ChangeViewportSize(
                new Vector4(256f, 0f, Screen.width, Screen.height),
                // top bottom left right
                new bool[4] { true, true, false, true},
                Configurator.GetDefaultAnimDuration());
            SetUIDOfCurrentlyActiveMenu(currMenu);
        }
    }

    internal static void UniverseCreator(int interactionIndex)
    {
        uint universeCreator = MenuFabricator.GetUniverseCreatorUID();
        int interactionLimit = GodInterfaceMenuPool[(int)universeCreator].GetInteractionLimit();
        if (interactionIndex >= interactionLimit)
        {
            uint mainMenu = MenuFabricator.GetMainMenuUID();
            ReturnFromThisMenuToThisOtherMenu(universeCreator, mainMenu);
        }
        switch (interactionIndex)
        {
            case 0:
                InterfaceListener.BlockAllButtonPresses = true;
                ViewportResizer.UnhideThisCorner(InterfaceFraming.BaseUIFrames.Right);
                ViewportResizer.UnblockThisCorner(InterfaceFraming.BaseUIFrames.Right);
                SetUIDOfCurrentlyActiveMenu(MenuFabricator.GetUniverseGeneratorSheetUID(), true);
                UniverseSimulation.CreateUniverse("new-universe");
                //InterfaceFabricator.MakeNewMenu(InterfaceFraming.BaseUIFrames.Right, InterfaceFabricator.MenuCreations.universeGeneratorSheet, true);
                //ViewportResizer.ChangeSizeOfThisMenuFrame(InterfaceFraming.BaseUIFrames.Right, Screen.width / 1.618f);
                ViewportResizer.ChangeViewportSize(
                    new Vector4(256f, 0f, Screen.width / 2, Screen.height),
                    // top bottom left right
                    new bool[4] { true, true, false, false },
                    Configurator.GetDefaultAnimDuration());
                //ViewportResizer.ChangeSizeOfThisMenuFrame(InterfaceFraming.BaseUIFrames.Left, 256f);
                break;
        }
    }
}
