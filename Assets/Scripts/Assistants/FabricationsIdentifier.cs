using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricationsIdentifier
{
    public string FabricatorName;
    private uint UniqueIdentifierCounter = 0;
    public uint GetCurrentIdentifier()
    {
        return UniqueIdentifierCounter;
    }
    public uint GetUniqueIdentifier()
    {
        uint UniqueIdentifier = UniqueIdentifierCounter;
        UniqueIdentifierCounter++;
        return UniqueIdentifier;
    }
}
