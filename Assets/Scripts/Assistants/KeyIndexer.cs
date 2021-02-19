using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIndexer
{
    private int KeyIndex = 0;

    public static string KeycodeToString(KeyCode key)
    {
        return key.ToString()[key.ToString().Length - 1].ToString();
    }

    public const int KeyCodeLimit = 36;

    public static int GetKeyGodInterfaceEnumeration(KeyCode inputKey)
    {
        switch (inputKey)
        {
            case KeyCode.Alpha1:
                return 1;
            case KeyCode.Alpha2:
                return 2;
            case KeyCode.Alpha3:
                return 3;
            case KeyCode.Alpha4:
                return 4;
            case KeyCode.Alpha5:
                return 5;
            case KeyCode.Alpha6:
                return 6;
            case KeyCode.Alpha7:
                return 7;
            case KeyCode.Alpha8:
                return 8;
            case KeyCode.Alpha9:
                return 9;
            case KeyCode.A:
                return 10;
            case KeyCode.B:
                return 11;
            case KeyCode.C:
                return 12;
            case KeyCode.D:
                return 13;
            case KeyCode.E:
                return 14;
            case KeyCode.F:
                return 15;
            case KeyCode.G:
                return 16;
            case KeyCode.H:
                return 17;
            case KeyCode.I:
                return 18;
            case KeyCode.J:
                return 19;
            case KeyCode.K:
                return 20;
            case KeyCode.L:
                return 21;
            case KeyCode.M:
                return 22;
            case KeyCode.N:
                return 23;
            case KeyCode.O:
                return 24;
            case KeyCode.P:
                return 25;
            case KeyCode.Q:
                return 26;
            case KeyCode.R:
                return 27;
            case KeyCode.S:
                return 28;
            case KeyCode.T:
                return 29;
            case KeyCode.U:
                return 30;
            case KeyCode.V:
                return 31;
            case KeyCode.W:
                return 32;
            case KeyCode.X:
                return 33;
            case KeyCode.Y:
                return 34;
            case KeyCode.Z:
                return 35;
            case KeyCode.Alpha0:
                return 36;
        }
        return 0;

    }

    public KeyCode GetNextKeyCode()
    {
        switch (KeyIndex)
        {
            case 0:
                KeyIndex++;
                return (KeyCode.Alpha1);
            case 1:
                KeyIndex++;
                return (KeyCode.Alpha2);
            case 2:
                KeyIndex++;
                return (KeyCode.Alpha3);
            case 3:
                KeyIndex++;
                return (KeyCode.Alpha4);
            case 4:
                KeyIndex++;
                return (KeyCode.Alpha5);
            case 5:
                KeyIndex++;
                return (KeyCode.Alpha6);
            case 6:
                KeyIndex++;
                return (KeyCode.Alpha7);
            case 7:
                KeyIndex++;
                return (KeyCode.Alpha8);
            case 8:
                KeyIndex++;
                return (KeyCode.Alpha9);
            case 9:
                KeyIndex++;
                return (KeyCode.A);
            case 10:
                KeyIndex++;
                return (KeyCode.B);
            case 11:
                KeyIndex++;
                return (KeyCode.C);
            case 12:
                KeyIndex++;
                return (KeyCode.D);
            case 13:
                KeyIndex++;
                return (KeyCode.E);
            case 14:
                KeyIndex++;
                return (KeyCode.F);
            case 15:
                KeyIndex++;
                return (KeyCode.G);
            case 16:
                KeyIndex++;
                return (KeyCode.H);
            case 17:
                KeyIndex++;
                return (KeyCode.I);
            case 18:
                KeyIndex++;
                return (KeyCode.J);
            case 19:
                KeyIndex++;
                return (KeyCode.K);
            case 22:
                KeyIndex++;
                return (KeyCode.L);
            case 23:
                KeyIndex++;
                return (KeyCode.M);
            case 24:
                KeyIndex++;
                return (KeyCode.N);
            case 25:
                KeyIndex++;
                return (KeyCode.O);
            case 26:
                KeyIndex++;
                return (KeyCode.P);
            case 27:
                KeyIndex++;
                return (KeyCode.R);
            case 28:
                KeyIndex++;
                return (KeyCode.S);
            case 29:
                KeyIndex++;
                return (KeyCode.T);
            case 30:
                KeyIndex++;
                return (KeyCode.U);
            case 31:
                KeyIndex++;
                return (KeyCode.V);
            case 32:
                KeyIndex++;
                return (KeyCode.W);
            case 33:
                KeyIndex++;
                return (KeyCode.X);
            case 34:
                KeyIndex++;
                return (KeyCode.Y);
            case 35:
                KeyIndex++;
                return (KeyCode.Z);
            case 36:
                KeyIndex++;
                return (KeyCode.Alpha0);
        }
        return KeyCode.None;
    }


    public KeyCode GetLastKey()
    {
        return KeyCode.Alpha0;
    }

    public string GetNextHotkeyString()
    {
        switch (KeyIndex)
        {
            case 0:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha1);
            case 1:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha2);
            case 2:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha3);
            case 3:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha4);
            case 4:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha5);
            case 5:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha6);
            case 6:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha7);
            case 7:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha8);
            case 8:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha9);
            case 9:
                KeyIndex++;
                return KeycodeToString(KeyCode.A);
            case 10:
                KeyIndex++;
                return KeycodeToString(KeyCode.B);
            case 11:
                KeyIndex++;
                return KeycodeToString(KeyCode.C);
            case 12:
                KeyIndex++;
                return KeycodeToString(KeyCode.D);
            case 13:
                KeyIndex++;
                return KeycodeToString(KeyCode.E);
            case 14:
                KeyIndex++;
                return KeycodeToString(KeyCode.F);
            case 15:
                KeyIndex++;
                return KeycodeToString(KeyCode.G);
            case 16:
                KeyIndex++;
                return KeycodeToString(KeyCode.H);
            case 17:
                KeyIndex++;
                return KeycodeToString(KeyCode.I);
            case 18:
                KeyIndex++;
                return KeycodeToString(KeyCode.J);
            case 19:
                KeyIndex++;
                return KeycodeToString(KeyCode.K);
            case 22:
                KeyIndex++;
                return KeycodeToString(KeyCode.L);
            case 23:
                KeyIndex++;
                return KeycodeToString(KeyCode.M);
            case 24:
                KeyIndex++;
                return KeycodeToString(KeyCode.N);
            case 25:
                KeyIndex++;
                return KeycodeToString(KeyCode.O);
            case 26:
                KeyIndex++;
                return KeycodeToString(KeyCode.P);
            case 27:
                KeyIndex++;
                return KeycodeToString(KeyCode.R);
            case 28:
                KeyIndex++;
                return KeycodeToString(KeyCode.S);
            case 29:
                KeyIndex++;
                return KeycodeToString(KeyCode.T);
            case 30:
                KeyIndex++;
                return KeycodeToString(KeyCode.U);
            case 31:
                KeyIndex++;
                return KeycodeToString(KeyCode.V);
            case 32:
                KeyIndex++;
                return KeycodeToString(KeyCode.W);
            case 33:
                KeyIndex++;
                return KeycodeToString(KeyCode.X);
            case 34:
                KeyIndex++;
                return KeycodeToString(KeyCode.Y);
            case 35:
                KeyIndex++;
                return KeycodeToString(KeyCode.Z);
            case 36:
                KeyIndex++;
                return KeycodeToString(KeyCode.Alpha0);
        }
        return "";
    }
}