using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceListener : MonoBehaviour
{
    internal static List<InterfaceFabricator.GodInterfaceMenu> GodInterfaceMenuPool = new List<InterfaceFabricator.GodInterfaceMenu>();
    private static KeyCode LastKeyPress = KeyCode.None;
    private static uint CurrentActiveMenu = 0;
    private static uint ButtonThatWasTriggeredLastFrame = uint.MaxValue;
    internal static bool BlockAllButtonPresses = false;

    public static uint GetCurrentActiveMenuUID()
    {
        return CurrentActiveMenu;
    }

    public static uint GetUIDOfVisibleMenuInThisFrame(InterfaceFraming.BaseUIFrames frame)
    {
        for(int i = 0; i < GodInterfaceMenuPool.Count; i++)
        {
            if(GodInterfaceMenuPool[i].CurrentFrame == frame && GodInterfaceMenuPool[i].gameObject.activeSelf)
            {
                return GodInterfaceMenuPool[i].GetIdentifier();
            }
        }
        return uint.MaxValue;
    }

    public static void SetUIDOfCurrentlyActiveMenu(uint inputMenuUint, bool activateMenuToo = false)
    {
        CurrentActiveMenu = inputMenuUint;
        if (activateMenuToo)
        {
            GodInterfaceMenuPool[(int)CurrentActiveMenu].gameObject.SetActive(true);
        }
    }

    public static void TriggerThisButtonInTheNextFrame(uint buttonIdentifier)
    {
        
        ButtonThatWasTriggeredLastFrame = buttonIdentifier;
    } 


    public static void AddGodInterfaceMenuToMenuPool(InterfaceFabricator.GodInterfaceMenu inputMenu)
    {
        GodInterfaceMenuPool.Add(inputMenu);
    }

    public static void SetLastKeypress()
    {
        KeyCode[] keys = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)keys.GetValue(i)))
            {
                LastKeyPress = (KeyCode)keys.GetValue(i);
                return;
            }
        }
        LastKeyPress = KeyCode.None;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LoopThroughEveryMenu()
    {
        for (int i = 0; i < GodInterfaceMenuPool.Count; i++)
        {
            if (GodInterfaceMenuPool[i].menuAnimation != null)
            {
                GodInterfaceMenuPool[i].menuAnimation.Animate(GodInterfaceMenuPool[i]);
            }
            if(GodInterfaceMenuPool[i].GetIdentifier() != CurrentActiveMenu)
            {
                BlockThisMenusButtons((int)GodInterfaceMenuPool[i].GetIdentifier());
            }
            else
            {
                UnblockThisMenusButtons((int)GodInterfaceMenuPool[i].GetIdentifier());
            }
        }
    }
    private static void UnblockThisMenusButtons(int inputMenuIndex)
    {
        InterfaceFabricator.GodInterfaceButton[] buttons = GodInterfaceMenuPool[(int)inputMenuIndex].GetButtonsArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponent<Button>().interactable = true;
        }
    }
    private static void BlockThisMenusButtons(int inputMenuIndex)
    {
        InterfaceFabricator.GodInterfaceButton[] buttons = GodInterfaceMenuPool[(int)inputMenuIndex].GetButtonsArray();
        for (int i = 0; i < buttons.Length; i++)
        {
             buttons[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }
    private static void LoopThroughThisMenusButtons(int inputMenuIndex)
    {
        if (BlockAllButtonPresses)
        {
            return;
        }
        InterfaceFabricator.GodInterfaceButton[] buttons = GodInterfaceMenuPool[(int)inputMenuIndex].GetButtonsArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (LastKeyPress != KeyCode.None && 
                LastKeyPress == buttons[i].buttonKeyCode && 
                CurrentActiveMenu == buttons[i].belongsToMenuID &&
                !BlockAllInteractionsInThisFrameDueToAnimations)
            {
                buttons[i].TriggerThisButton();
            }
        }

    }



    public static void InterpretButtonPress(uint buttonIdentifier)
    {
        //int mainMenuIdx = (int)MenuFabricator.GetMainMenuUniqueID();
        int buttonIndexInMenu = GodInterfaceMenuPool[(int)CurrentActiveMenu].GetButtonIndexByUID(buttonIdentifier);

        //Debug.Log(CurrentActiveMenu);
        //Debug.Log(universeCreatorIdx);
        if(CurrentActiveMenu == MenuFabricator.GetMainMenuUID())
        {
            MenuNavigations.MainMenu(buttonIndexInMenu);
        }
        else if (CurrentActiveMenu == MenuFabricator.GetUniverseCreatorUID())
        {
            MenuNavigations.UniverseCreator(buttonIndexInMenu);
            //Debug.Log("universecreatormenu");
        }
        else if (CurrentActiveMenu == MenuFabricator.GetModsManagerMenuUID())
        {
            MenuNavigations.ModManager(buttonIndexInMenu);
        }
        else if (CurrentActiveMenu == MenuFabricator.GetSaveManagerMenuUID())
        {
            MenuNavigations.SaveManager(buttonIndexInMenu);
        }
        else if (CurrentActiveMenu == MenuFabricator.GetOptionsMenuUID())
        {
            MenuNavigations.OptionsMenu(buttonIndexInMenu);
        }
        else if (CurrentActiveMenu == MenuFabricator.GetUniverseGeneratorSheetUID())
        {
            MenuNavigations.UniverseCreatorSheet(buttonIndexInMenu);
        }


        /*if (CurrentActiveMenu == mainMenuIdx && buttonIndexInMenu == 1)
        {
            GodInterfaceMenuPool[mainMenuIdx].AddAnimation(InterfaceAnimation.TransitionTo.LeftOut, 1.0f);
            GodInterfaceMenuPool[universeCreatorIdx].AddAnimation(InterfaceAnimation.TransitionTo.RightIn, 1.0f);
            SetUIDOfCurrentlyActiveMenu((uint)universeCreatorIdx);
        }
        else if (CurrentActiveMenu == universeCreatorIdx && buttonIndexInMenu == 1)
        {
            //Debug.Log("Create new universe");
            GodInterfaceMenuPool[mainMenuIdx].AddAnimation(InterfaceAnimation.TransitionTo.LeftIn, 1.0f);
            GodInterfaceMenuPool[universeCreatorIdx].AddAnimation(InterfaceAnimation.TransitionTo.RightOut, 1.0f);
            SetUIDOfCurrentlyActiveMenu((uint)mainMenuIdx);
        }*/
    }

    internal static bool BlockAllInteractionsInThisFrameDueToAnimations = false;

    // Update is called once per frame
    void Update()
    {
        SetLastKeypress();
        LoopThroughEveryMenu();
        LoopThroughThisMenusButtons((int)CurrentActiveMenu);
        if(ButtonThatWasTriggeredLastFrame != uint.MaxValue)
        {
            if (BlockAllInteractionsInThisFrameDueToAnimations == false)
            {
                InterpretButtonPress(ButtonThatWasTriggeredLastFrame);
            }
        }
        BlockAllInteractionsInThisFrameDueToAnimations = false;
        ButtonThatWasTriggeredLastFrame = uint.MaxValue;
    }
}
