using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Text;
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
    /// <summary>
    /// 获取版本信息
    /// </summary>
    [MenuItem("GD/IO/CreatVersion")]
    public static void CreatVersionInfo()
    {
        string path = Application.dataPath + "/Server/Asset";
        int ignoreLength = path.Length+1;

        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/Server/Version/Version.txt"))
        {
            GetFile(sw, path, ignoreLength);
            sw.Flush();
            sw.Close();
        }
        Debug.Log("Creat Version Success!!!");
    }
    /// <summary>
    /// 忽略.meta文件
    /// </summary>
    public static string ignore = ".meta";
    /// <summary>
    /// 递归函数深度优先的遍历文件夹
    /// </summary>
    public static void GetFile(StreamWriter writer, string path, int ignoreLength)
    {
        string[] directors = Directory.GetDirectories(path);
        for (int i = 0; i < directors.Length; i++)
        {
            GetFile(writer, directors[i], ignoreLength);
        }
        string[] files = Directory.GetFiles(path);
        for (int i = 0; i < files.Length; i++)
        {
            if (!files[i].EndsWith(ignore))
            {
                string md5 = GetMD5HashFromFile(files[i]);
                writer.WriteLine(files[i].Remove(0, ignoreLength) + "|" + md5);
            }
        }
    }
    /// <summary>
    /// 获取文件的MD5
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string GetMD5HashFromFile(string fileName)
    {
        try
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

           System.Text. StringBuilder sb = new System.Text. StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }
    /// <summary>
    /// 根据比对版本信息更新文件内容
    /// </summary>
    [MenuItem("GD/IO/CreatFileByVersion")]
    public static void CreatFileByVersion()
    {
        string root = Application.dataPath + "/root/";
        if (!Directory.Exists(root))
        {
            Directory.CreateDirectory(root);
        }
        using (StreamReader sr = new StreamReader(Application.dataPath + "/Server/Version/Version.txt"))
        {
            string path="";
            while(sr.HaveMessage(out path))
            {
                string md5 = path.Split('|')[1];
                string file = path.Split('|')[0];
                string[] roots = file.Split('/', '\\');
                int count = file.Length;
                int removeCount = roots[roots.Length - 1].Length;
                string directory = file.Remove(count-removeCount,removeCount);
                Directory.CreateDirectory(root+directory);
            }
            sr.Dispose();
            sr.Close();
        }
        AssetDatabase.Refresh();
        Debug.Log("Download Success!!!!");
    }
     /// <summary>
     /// 读取文本信息
     /// </summary>
   public static bool HaveMessage(this StreamReader sr,out string message)
    {

        message = sr.ReadLine();
        if (message==null)
        {
            return false;
        }
            return true;
    }
    /// <summary>
    /// 从服务器下载资源
    /// </summary>
    [MenuItem("GD/IO/DownloadAssetByWWW")]
    public static void DownloadAssetByWWW()
    {

        //loadasset("http://localhost/HotfixAsset/cube.txt");
        EmptyMono mono = GameObject.FindObjectOfType<EmptyMono>();
        if(!mono)
        {
            GameObject obj = new GameObject("Mono");
             mono = obj.AddComponent<EmptyMono>();
        }
        //"http://localhost/HotfixAsset/Version.txt"
        mono.StartCoroutine(ParseVersion( "Version.txt"));
    }
   static IEnumerator loadasset(string url)
    {
        Debug.Log("www");
        WWW w = new WWW(url);
        yield return w;
        if (w.isDone)
        {
            byte[] model = w.bytes;
            int length = model.Length;
            //写入模型到本地
          Tools.  CreateModelFile(Application.streamingAssetsPath, "asset/cube.txt", model, length);
        }
    }
 
    /// <summary>
    /// 解析文本 获取资源的路径和MD5值
    /// </summary>
    static IEnumerator ParseVersion( string path ,System.Action action=null)
    {
        ///获取服务器上的版本信息
        string serverPath = "http://localhost/HotfixAsset/" + path;
        WWW www = new WWW(serverPath);
        yield return www;
        byte[] data=  www.bytes;
        string str = System.Text.Encoding.UTF8.GetString(data);
        Dictionary<string,string> ServerVersionInfo=Tools.ParseTextToDictionary(str);

        //解析本地的版本信息
        string localPath = "file://"+Application.dataPath+"/Server/Version/"+ path;
        www = new WWW(localPath);
        yield return www;
       data = www.bytes;
       str = System.Text.Encoding.UTF8.GetString(data);
        Dictionary<string, string> LocalVersionInfo = Tools.ParseTextToDictionary(str);

        //比较差异 找到需要更新的文件
        List<string> needUpdateAsset = new List<string>();
        foreach(string key in ServerVersionInfo.Keys)
        {
            //当本地资源不存在 或者本地资源和服务器的资源不相同时记录下资源路径
            if (!LocalVersionInfo.ContainsKey(key)|| !ServerVersionInfo[key].Contains(LocalVersionInfo[key]))
            {
                needUpdateAsset.Add(key);
            }
        }
        string serverRoot= "http://localhost/HotfixAsset/Asset/";
        string localRoot =  Application.dataPath + "/Server/Asset/";
      
        //更新资源
        for (int i=0;i<needUpdateAsset.Count;i++)
        {
            WWW w = new WWW(serverRoot+needUpdateAsset[i]);
            yield return w;
            if (w.isDone)
            {
                byte[] model = w.bytes;
                int length = model.Length;
                //写入模型到本地
              Tools.  CreateModelFile(localRoot+ needUpdateAsset[i], model, length);
            }
        }
        www = new WWW("http://localhost/HotfixAsset/" + path);
        yield return www;
        if (www.isDone)
        {
            byte[] model = www.bytes;
            int length = model.Length;
            //写入模型到本地
            Tools.CreateModelFile(Application.dataPath + "/Server/Version/" + path, model, length);
        }
        AssetDatabase.Refresh();
        Debug.Log("Update Asset Success!!!");
    }
  
 
}
