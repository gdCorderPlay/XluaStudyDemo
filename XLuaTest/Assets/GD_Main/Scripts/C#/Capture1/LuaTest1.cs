using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
namespace GDLua
{


    public class LuaTest1 : MonoBehaviour
    {
        void Start()
        {
            Lua lua = new Lua();
            //lua.SetFileRootPath("G:");
            lua.SetFileRootPath(Application.streamingAssetsPath + "/Capture1/");
             lua.DoFile("Test");
           // Logger("GDDDDDDDDDDDD");
//            LuaEnv lua = new LuaEnv();

            //            lua.DoString(@"
            //function  SaveByte(path, msg)
            //file = io.open(path, 'a')
            //file: write(msg)
            //-- 关闭打开的文件
            //file: close()
            //end
            //--msg
            //SaveByte('G:\\outPut.txt', 'lua++++++')
            //                ");

        }
        void Logger(string msg)
        {

            FileStream fs = new FileStream(@"g:\Debug.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(msg + DateTime.Now.ToString() + "\n");
            sw.Flush();
            sw.Close();
            fs.Close();
        }

    }
}
