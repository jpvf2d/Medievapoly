using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardSpace : MonoBehaviour
{
	/*
	Need the following variables:
	- Category color
	- A bool or something to indicate if all properties in this category are owned (rent increases if all owned)
	*/
    public abstract void passing();
    public abstract void land();
    public abstract void stuck();
}
