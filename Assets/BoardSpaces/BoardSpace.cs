using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardSpace : MonoBehaviour
{
    public abstract void passing();
    public abstract void land();
    public abstract void stuck();
}
