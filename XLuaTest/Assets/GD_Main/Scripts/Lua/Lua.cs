using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
namespace GDLua
{

    public class Lua : LuaEnv
    {

        /// <summary>
        /// 设置lua加载的路径
        /// </summary>
        /// <param name="_filePath"></param>
        public void SetFileRootPath(string _filePath)
        {
            base.AddLoader((ref string filepath) =>
            {
                filepath = _filePath+"/" + filepath.Replace('.', '/') + ".lua";
                if (File.Exists(filepath))
                {
                    return File.ReadAllBytes(filepath);
                }
                else
                {
                    return null;
                }
            });
        }
        public void DoFile(string fileName)
        {

            base.DoString("require"+ "'"+fileName+"'");
        }

       
    }
}



