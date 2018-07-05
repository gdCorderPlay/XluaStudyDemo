using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 工具类
/// </summary>
public class Tools  {

    /// <summary>
    /// 解析文本文档 并返回一个资源路径和资源MD5 的键值对
    /// </summary>
    public static Dictionary<string, string> ParseTextToDictionary(string context)
    {
        Dictionary<string, string> pathAndMD5 = new Dictionary<string, string>();
        string[] paths = context.Split('\n');
        for (int i = 0; i < paths.Length; i++)
        {
            string keyAndValue = paths[i].Trim();
            // Debug.Log(keyAndValue);
            if (!string.IsNullOrEmpty(keyAndValue))
            {


                string assetPath = keyAndValue.Split('|')[0];
                string MD5 = keyAndValue.Split('|')[1];
                pathAndMD5.Add(assetPath, MD5);

            }
        }
        // Debug.Log("Parse Success!!! You have get the key and value with assetpath and MD5!!!");
        return pathAndMD5;
    }
    /// <summary>
    /// 根据文件的路径名称 创建文件 如果路径不存在就创建路径
    /// </summary>
    public static void CreateModelFile(string path, byte[] info, int length)
    {
        //文件流信息
        //StreamWriter sw;
        Stream sw;
        string[] paths = path.Split('/', '\\');
        string remove = paths[paths.Length - 1];
        string directory = path.Remove(path.Length - remove.Length - 1, remove.Length + 1);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        FileInfo t = new FileInfo(path);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.Create();

        }
        else
        {
            //如果此文件存在则打开
            sw = t.Open(FileMode.Open);

        }
        //以行的形式写入信息
        //sw.WriteLine(info);
        sw.Write(info, 0, length);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
        // Debug.Log("成功创建文件：" + path);
    }
    public static void CreateModelFile(string path, string name, byte[] info, int length)
    {
        //文件流信息
        //StreamWriter sw;
        Stream sw;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        FileInfo t = new FileInfo(path + "/" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.Create();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.Open(FileMode.CreateNew);
            return;
        }
        //以行的形式写入信息
        //sw.WriteLine(info);
        sw.Write(info, 0, length);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }
}
