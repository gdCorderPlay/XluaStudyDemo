using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaStart : MonoBehaviour {

	
	void Start ()
    {
       

	}
	
public	void DoLuaString(int index)
    {

        GDLua.Lua lua = new GDLua.Lua();
        lua.DoString(GDLua.LuaScripts.scripts[index]);


    }
}
