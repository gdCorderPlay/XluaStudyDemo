using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
/// <summary>
/// 文件操作控制类
/// </summary>
public static class FileControl  {
    /// <summary>
    /// 创建一个文件夹
    /// </summary>
    [MenuItem("GD/IO/CreatFolder")]
	public static void CreatFolder()
    {
        Directory.CreateDirectory(Application.dataPath+"/GD/gd");
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 创建一个文件 路径目录问价夹需要提前存在
    /// </summary>
    [MenuItem("GD/IO/CreatFile")]
    public static void CreatFile()
    {
     FileStream fs=   File.Create(Application.dataPath + "/GD/gd/info.txt");
        fs.Close();
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 移动文件夹 包含文件夹下的内容
    /// </summary>
    [MenuItem("GD/IO/MoveFolder")]
    public static void MoveFolder()
    {
        Directory.Move(Application.dataPath + "/GD/gd", Application.dataPath+ "/GD_Main/gd");
    }


}
