﻿/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;
using System.IO;
[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

//[LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour {
    /// <summary>
    /// lua 脚本文件
    /// </summary>
    public TextAsset luaScript;

    private string luaScriptContext;
    
    public Injection[] injections;
    /// <summary>
    /// lua 虚拟机
    /// </summary>
    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 
    /// <summary>
    /// 方法
    /// </summary>
    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;
    /// <summary>
    /// lua脚本对象
    /// </summary>
    private LuaTable scriptEnv;

   public void Test()
    {
        Debug.Log("测试lua脚本中调用c#脚本的方法");
    }
    void Awake()
    {

        //  File fs = new File(Application.dataPath+ "XLua/Examples/02_U3DScripting/LuaTestScript.lua.text");
       // luaScriptContext = File.ReadAllText(Application.dataPath + "/XLua/Examples/02_U3DScripting/LuaTestScript.lua.txt");
        //luaScript = ()fs.BaseStr;
         scriptEnv = luaEnv.NewTable();
        print("aa");
        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);
        }
        luaEnv.AddLoader((ref string filepath) =>
        {
            filepath = Application.dataPath + "/XLua/Examples/08_Hotfix/" + filepath.Replace('.', '/') + ".lua";
            if (File.Exists(filepath))
            {
                return File.ReadAllBytes(filepath);
            }
            else
            {
                return null;
            }
        });
        luaEnv.DoString(luaScript.text/*luaScriptContext*/, "LuaBehaviour", scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("update", out luaUpdate);
        scriptEnv.Get("ondestroy", out luaOnDestroy);

        if (luaAwake != null)
        {
            luaAwake();
        }
    }

	// Use this for initialization
	void Start ()
    {
        if (luaStart != null)
        {
            luaStart();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
	}

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv.Dispose();
        injections = null;
    }
}
