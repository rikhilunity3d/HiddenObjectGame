using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectHolder : MonoBehaviour
{

    [SerializeField]
    private List<HiddenObjectData> hiddenObjectsList;

    public  List<HiddenObjectData> HiddenObjectsList { get { return hiddenObjectsList; } }
   
}
