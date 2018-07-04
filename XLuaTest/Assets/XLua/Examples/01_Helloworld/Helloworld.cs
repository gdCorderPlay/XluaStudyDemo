/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using XLua;
[LuaCallCSharp]
public class Helloworld : MonoBehaviour {
    // Use this for initialization
    private LuaTable scriptEnv;
    void Start () {
        LuaEnv luaenv = new LuaEnv();
        scriptEnv = luaenv.NewTable();
        //设置元表 独立出来
        LuaTable meta = luaenv.NewTable();
        meta.Set("__index", luaenv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        luaenv.DoString(@"
        local helloworld=nil
         function a() 
             helloworld=self:GetComponent(typeof(CS.Helloworld))
            helloworld:DOTest()
            -- print('111')
           end
       -- a()
", "Helloworld", scriptEnv);
       System. Action luaAwake = scriptEnv.Get<System. Action>("a");
        if (luaAwake != null)
        {
            luaAwake();
        }
        // luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
        // luaenv.DoString("CS.UnityEngine.Debug.Log('gd')");
        // luaenv.DoString(@"
        //    a=  function() 
        //       --self:DOTest()
        //        print('111')
        //      end
        //   a()
        //"

        //);
        //luaenv.DoString(@"

        //    xlua.hotfix(CS.Helloworld, 'Update', function(self)
        //       -- self.tick = self.tick + 1
        //       --if (self.tick % 50) == 0 then
        //        -- print('<<<<<<<<Update in lua, tick = ' .. self.tick )
        //        -- print('gd')
        //        self:DOTest()--调用方法
        //        print(self.num)--调用属性
        //        CS.Helloworld.DoStaticTest()--调用静态方法
        //       -- end

        //    end)
        //");
        // luaenv.Dispose();
        //Debug.Log('a');
    }

    /// <summary>
    /// 球
    /// </summary>
    Transform ball;
    /// <summary>
    /// 鼠标
    /// </summary>
    Transform qidian;
    /// <summary>
    /// 指向结束点
    /// </summary>
    Transform end;
  public  void DOTest()
    {
         Debug.Log("test function");

        //end.position = ball.position + (ball.position - qidian.position);
         

    }
    private int num=1;
    static void DoStaticTest()
    {
        Debug.Log("test Static function");
    }
	// Update is called once per frame
	void Update () {
	
	}
}
