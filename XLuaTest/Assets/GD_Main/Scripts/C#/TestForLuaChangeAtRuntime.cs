using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class TestForLuaChangeAtRuntime : MonoBehaviour {

     string script="";
    LuaEnv luaenv = new LuaEnv();
    LuaTable luaScripts;
    private IEnumerator Start()
    {
        yield return null;
        //script=  Application.streamingAssetsPath
        //WWW load = new WWW("file://" + Application.streamingAssetsPath + "/TestLua.lua.txt");
        //yield return load;
        //script = Encoding.UTF8.GetString(load.bytes);
        //LuaTable meta = luaenv.NewTable();
        //meta.Set("__index", luaenv.Global);
        //luaScripts = luaenv.NewTable();
        //luaScripts.SetMetaTable(meta);
        //meta.Dispose();
        //luaScripts.Set("self", this);
       // luaScripts.Set("self", this);
    }
    public float speed;
    private void Update()
    {

        // luaenv.DoString(@script, "TestForLuaChangeAtRuntime", luaScripts);
        luaenv.DoString("print(1111)");
    }
}
