using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InterfaceFabricator : InterfaceListener
{
    private static FabricationsIdentifier MenuIdentificator = new FabricationsIdentifier();
    private static FabricationsIdentifier ButtonIdentificator = new FabricationsIdentifier();


    public class GodInterfaceMenu
    {
        internal GameObject gameObject;
        internal GameObject contentGObject;
        internal uint menuIdentifier;
        private float addedElementsHeightsCounter = 0.0f;
        private List<GodInterfaceButton> buttons = new List<GodInterfaceButton>();
        public InterfaceAnimation.MenuAnimation menuAnimation = null;
        internal InterfaceFraming.BaseUIFrames CurrentFrame;

        public int GetButtonIndexByUID(uint uniqueId)
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                if(buttons[i].buttonIdentifier == uniqueId)
                {
                    return i;
                }
            }
            return -1;
        }

        public uint GetIdentifier()
        {
            return menuIdentifier;
        }
        public int GetInteractionLimit()
        {
            return buttons.Count - 1;
        }
        public GodInterfaceButton[] GetButtonsArray()
        {
            return buttons.ToArray();
        }

        public void AddAnimation(InterfaceAnimation.TransitionTo transition, float duration)
        {
            InterfaceAnimation.MenuAnimation newMenuAnimation = new InterfaceAnimation.MenuAnimation();
            Vector2 currentDimensions = Unity_Static.GetWidthAndHeightOfUIObject(gameObject);
            if (transition == InterfaceAnimation.TransitionTo.RightOut)
            {
                newMenuAnimation.startPos = gameObject.transform.localPosition;
                newMenuAnimation.endPos = newMenuAnimation.startPos;
                newMenuAnimation.setActiveAfterEnding = false;
                newMenuAnimation.endPos.x += currentDimensions.x;
            }
            else if (transition == InterfaceAnimation.TransitionTo.RightIn)
            {
                newMenuAnimation.startPos = new Vector3(0, 0, 0);
                newMenuAnimation.endPos = new Vector3(0, 0, 0);
                newMenuAnimation.startPos.x = currentDimensions.x;
                newMenuAnimation.setActiveAfterEnding = true;
            }
            else if (transition == InterfaceAnimation.TransitionTo.LeftOut)
            {
                newMenuAnimation.startPos = new Vector3(0,0,0);
                newMenuAnimation.endPos = new Vector3(0, 0, 0);
                newMenuAnimation.endPos.x = -currentDimensions.x;
                newMenuAnimation.setActiveAfterEnding = false;
            }
            else if (transition == InterfaceAnimation.TransitionTo.LeftIn)
            {
                newMenuAnimation.startPos = new Vector3(0, 0, 0);
                newMenuAnimation.startPos.x = -currentDimensions.x;
                newMenuAnimation.endPos = new Vector3(0, 0, 0);
                newMenuAnimation.setActiveAfterEnding = true;
            }
            newMenuAnimation.startTime = Time.time;
            newMenuAnimation.endTime = Time.time + duration;
            menuAnimation = newMenuAnimation;
        }

        internal void AddMenuElement(GameObject menuElementGO)
        {
            menuElementGO.transform.SetParent(contentGObject.transform, false);
            menuElementGO.transform.localPosition = new Vector3(menuElementGO.transform.localPosition.x, addedElementsHeightsCounter,0);
            addedElementsHeightsCounter -= Unity_Static.GetWidthAndHeightOfUIObject(menuElementGO).y;
        }
        internal void AddMenuButton(GodInterfaceButton button)
        {
            buttons.Add(button);
            button.gameObject.transform.SetParent(contentGObject.transform, false);
            button.gameObject.transform.localPosition = new Vector3(button.gameObject.transform.localPosition.x, addedElementsHeightsCounter, 0);
            addedElementsHeightsCounter -= Unity_Static.GetWidthAndHeightOfUIObject(button.gameObject).y - 1;
        }
    }

    public class GodInterfaceButton
    {
        internal GameObject gameObject;
        internal string buttonHotkeyStr;
        internal KeyCode buttonKeyCode;
        internal uint belongsToMenuID;
        internal uint buttonIdentifier;

        public void TriggerThisButton()
        {
            ExecuteEvents.Execute(gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    public static GameObject GetTitle(string titleText)
    {
        GameObject newTitle = Instantiate(TitlePrefab);
        newTitle.GetComponentInChildren<Text>().text = titleText;
        return newTitle;
    }

    public static GodInterfaceButton GetButton(uint belongsToThisMenu, string buttonDesc, KeyCode buttonHotk)
    {
        GameObject newButton = Instantiate(ButtonPrefab);

        Text[] texts = newButton.GetComponentsInChildren<Text>();
        texts[0].text = buttonDesc;
        texts[1].text = KeyIndexer.KeycodeToString(buttonHotk);
        GodInterfaceButton godInterfaceButton = new GodInterfaceButton();
        godInterfaceButton.belongsToMenuID = belongsToThisMenu;
        godInterfaceButton.buttonIdentifier = ButtonIdentificator.GetUniqueIdentifier();
        godInterfaceButton.gameObject = newButton;
        godInterfaceButton.buttonHotkeyStr = texts[1].text;
        godInterfaceButton.buttonKeyCode = buttonHotk;

        newButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            TriggerThisButtonInTheNextFrame(godInterfaceButton.buttonIdentifier);
            //Debug.Log(godInterfaceButton.belongsToMenuID + " " + godInterfaceButton.buttonHotkeyStr);
        });

        return godInterfaceButton;
    }

    public static GodInterfaceButton GetUserInputField
        (uint belongsToThisMenu, 
        string buttonDesc, 
        string defaultEmptyText,
        KeyCode buttonHotk)
    {
        GameObject newInputfield = Instantiate(InputfieldPrefab);

        Text[] texts = newInputfield.GetComponentsInChildren<Text>();
        texts[0].text = buttonDesc;
        texts[1].text = KeyIndexer.KeycodeToString(buttonHotk);
        texts[2].text = defaultEmptyText;
        GodInterfaceButton godInterfaceButton = new GodInterfaceButton();
        godInterfaceButton.belongsToMenuID = belongsToThisMenu;
        godInterfaceButton.buttonIdentifier = ButtonIdentificator.GetUniqueIdentifier();
        godInterfaceButton.gameObject = newInputfield;
        godInterfaceButton.buttonHotkeyStr = texts[1].text;
        godInterfaceButton.buttonKeyCode = buttonHotk;
        InputField thisInputfield = newInputfield.GetComponentInChildren<InputField>();
        //InputField inputField = newInputfield.GetComponentInChildren<InputField>();
        thisInputfield.onValueChanged.AddListener(delegate 
        {
            BlockAllButtonPresses = true;
        });

        thisInputfield.onEndEdit.AddListener(delegate
        {
            BlockAllButtonPresses = false;
        });

        newInputfield.GetComponent<Button>().onClick.AddListener(delegate
        {
            newInputfield.GetComponentInChildren<InputField>().ActivateInputField();
            /*if (newInputfield.GetComponentInChildren<InputField>().isFocused == false)
            {
                TriggerThisButtonInTheNextFrame(godInterfaceButton.buttonIdentifier);
            }*/
            //Debug.Log(godInterfaceButton.belongsToMenuID + " " + godInterfaceButton.buttonHotkeyStr);
        });

        return godInterfaceButton;
    }

    public enum MenuCreations
    {
        mainMenu,
        universeCreator,
        saveManager,
        optionsMenu,
        modManager,
        universeGeneratorSheet,
    }

    public static GodInterfaceMenu MakeMenu(GameObject instantiatedMenuGO)
    {
        GodInterfaceMenu godMenu = new GodInterfaceMenu();
        godMenu.gameObject = instantiatedMenuGO;
        godMenu.menuIdentifier = MenuIdentificator.GetUniqueIdentifier();
        godMenu.contentGObject = Unity_Static.GetChildWithGameObjectName(godMenu.gameObject, "ContentHeightController");
        return godMenu;

    }

    public static void MakeNewMenu(InterfaceFraming.BaseUIFrames frame, MenuCreations menu, bool startActivated = false)
    {
        GameObject newMenu = Instantiate(MenuPrefab);
        GodInterfaceMenu godMenu = MakeMenu(newMenu);

        switch (menu)
        {
            case MenuCreations.mainMenu:
                //Debug.Log("Making mainmenu");
                godMenu = MenuFabricator.CreateMainMenu(godMenu, frame);
                break;
            case MenuCreations.modManager:
                //Debug.Log("Making mod manager");
                godMenu = MenuFabricator.CreateModManagerMenu(godMenu, frame);
                break;
            case MenuCreations.optionsMenu:
                //Debug.Log("Making optionsmenu");
                godMenu = MenuFabricator.CreateOptionsMenu(godMenu, frame);
                break;
            case MenuCreations.saveManager:
                //Debug.Log("Making savemanager");
                godMenu = MenuFabricator.CreateSaveManagerMenu(godMenu, frame);
                break;
            case MenuCreations.universeCreator:
                //Debug.Log("Making universecreator");
                godMenu = MenuFabricator.CreateUniverseCreatorMenu(godMenu, frame);
                break;
            case MenuCreations.universeGeneratorSheet:
                //Debug.Log("Making universecreator");
                godMenu = MenuFabricator.CreateUniverseGeneratorSheet(godMenu, frame);
                //godMenu.gameObject.transform.localPosition = new Vector3(1000, 1000, 0);
                /*KeyIndexer keyIndexer = new KeyIndexer();
                uint UniqueID = godMenu.GetIdentifier();
                godMenu.CurrentFrame = frame;
                godMenu.gameObject.name = "UniverseSheet";
                godMenu.AddMenuElement(GetTitle("God Interface"));
                GodInterfaceButton continueButton = GetButton(godMenu.menuIdentifier, "Continue", keyIndexer.GetNextKeyCode());
                GodInterfaceButton newGamebutton = GetButton(godMenu.menuIdentifier, "New game", keyIndexer.GetNextKeyCode());
                GodInterfaceButton optionsButton = GetButton(godMenu.menuIdentifier, "Options", keyIndexer.GetNextKeyCode());
                GodInterfaceButton modsButton = GetButton(godMenu.menuIdentifier, "Mods", keyIndexer.GetNextKeyCode());
                GodInterfaceButton exitButton = GetButton(godMenu.menuIdentifier, "Exit", keyIndexer.GetLastKey());
                godMenu.AddMenuButton(continueButton);
                godMenu.AddMenuButton(newGamebutton);
                godMenu.AddMenuButton(optionsButton);
                godMenu.AddMenuButton(modsButton);
                godMenu.AddMenuButton(exitButton);
                godMenu.gameObject.SetActive(true);
                InterfaceFraming.PlaceObjectInUIFrame(frame, godMenu);
                
                return;*/

                break;
        }

        ActivateMenuWhenHovering menuHoverRef = godMenu.gameObject.AddComponent<ActivateMenuWhenHovering>();
        menuHoverRef.MenuToActivate = godMenu.GetIdentifier();

        InterfaceFraming.PlaceObjectInUIFrame(frame, godMenu);
        InterfaceListener.AddGodInterfaceMenuToMenuPool(godMenu);

        if (startActivated)
        {
            newMenu.SetActive(true);
        }
        else
        {
            newMenu.SetActive(false);
        }
    }

    // Start is called before the first frame update

    private static GameObject MenuPrefab;
    private static GameObject TitlePrefab;
    private static GameObject ButtonPrefab;
    private static GameObject InputfieldPrefab;

    void Start()
    {
        MenuPrefab = GameObject.Find("ui-Prefab-Menu");
        TitlePrefab = GameObject.Find("ui-Prefab-Title");
        ButtonPrefab = GameObject.Find("ui-Prefab-Button");
        InputfieldPrefab = GameObject.Find("ui-Prefab-InputField");
        MakeNewMenu(InterfaceFraming.BaseUIFrames.Left, MenuCreations.mainMenu, true);
        MakeNewMenu(InterfaceFraming.BaseUIFrames.Left, MenuCreations.universeCreator);
        MakeNewMenu(InterfaceFraming.BaseUIFrames.Left, MenuCreations.modManager);
        MakeNewMenu(InterfaceFraming.BaseUIFrames.Left, MenuCreations.optionsMenu);
        MakeNewMenu(InterfaceFraming.BaseUIFrames.Left, MenuCreations.saveManager);
        MakeNewMenu(InterfaceFraming.BaseUIFrames.Right, MenuCreations.universeGeneratorSheet, true);
        gameObject.SetActive(false);
    }
}
