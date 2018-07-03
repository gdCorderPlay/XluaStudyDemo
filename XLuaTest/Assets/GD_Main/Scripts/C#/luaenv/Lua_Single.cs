using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lua_Single : Singleton<Lua_Single> {

	public void Show()
    {
        Debug.Log("lua single");
    }
    
}
