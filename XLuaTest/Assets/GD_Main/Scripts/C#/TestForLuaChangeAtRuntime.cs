using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XLua;
using UnityEngine.UI;
[LuaCallCSharp]
public class TestForLuaChangeAtRuntime : MonoBehaviour {

    public Text t;
     string script="";
    LuaEnv luaenv = new LuaEnv();
    LuaTable luaScripts;
    private IEnumerator Start()
    {
        yield return null;
        //script=  Application.streamingAssetsPath
        WWW load = new WWW("file://" + Application.streamingAssetsPath + "/TestLua.lua.txt");
        yield return load;
        script = Encoding.UTF8.GetString(load.bytes);
        LuaTable meta = luaenv.NewTable();
        meta.Set("__index", luaenv.Global);
        luaScripts = luaenv.NewTable();
        luaScripts.SetMetaTable(meta);
        meta.Dispose();
        luaScripts.Set("self", this);
       // luaScripts.Set("self", this);
    }
    public float speed;

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RuningLua();
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
           StartCoroutine( FixRuningLua());
        }else
        {
            RunningCSharp();
        }
        //luaenv.DoString("print(1111)");
    }
    void RunningCSharp()
    {
        Debug.Log("CSharp");
    }
    void RuningLua()
    {
        luaenv.DoString(@script, "TestForLuaChangeAtRuntime", luaScripts);
    }


    IEnumerator FixRuningLua()
    {
        WWW load = new WWW("file://" + Application.streamingAssetsPath + "/TestLua.lua.txt");
        yield return load;
        script = Encoding.UTF8.GetString(load.bytes);
        RuningLua();
    }
    public void Logger(string msg)
    {
        // Debug.Log(msg);
        t.text = msg;
    }

}
