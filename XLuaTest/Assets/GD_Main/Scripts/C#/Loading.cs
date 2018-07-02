using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
/// <summary>
/// 加载场景信息资源
/// </summary>
public class Loading : MonoBehaviour {
    public AssetBundle bundle;

	IEnumerator  Start ()
    {
      //  WWW download = WWW.LoadFromCacheOrDownload("file://" + Application.dataPath + "/GD_Main/BundleAsset/prefab/cube.txt", 1);
        WWW download = WWW. LoadFromCacheOrDownload("http://localhost/HotfixAsset/cube.txt",1);
        yield return download;
        ///cube.modle
        bundle = download.assetBundle;
       // byte[] data = download.bytes;
      //  File.WriteAllBytes(Application.streamingAssetsPath+ "/Asset/cube.txt",data);

        // bundle = AssetBundle.LoadFromFile("file://"+Application.dataPath + "/GD_Main/BundleAsset/prefab");
        // file:///D:/Study/GD/XluaStudyDemo/XLuaTest/Assets/GD_Main/BundleAsset
        //  bundle = AssetBundle.LoadFromFile("file:///D:/Study/GD/XluaStudyDemo/XLuaTest/AssetBundles/prefab");
      GameObject obj =     bundle.LoadAsset("cube") as GameObject;

        GameObject instanceObj = Instantiate(obj);

       //StartCoroutine(loadasset("http://localhost/HotfixAsset/cube.txt")) ;
    }
    //写入模型到本地
    IEnumerator loadasset(string url)
    {
        Debug.Log("www");
        WWW w = new WWW(url);
        yield return w;
        if (w.isDone)
        {
            byte[] model = w.bytes;
            int length = model.Length;
            //写入模型到本地
            CreateModelFile(Application.streamingAssetsPath, "asset/cube.txt", model, length);
        }
    }

    void CreateModelFile(string path, string name, byte[] info, int length)
    {
        //文件流信息
        //StreamWriter sw;
        Stream sw;
        FileInfo t = new FileInfo(path + "/" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.Create();
        }
        else
        {
            //如果此文件存在则打开
           // sw = t.Open(FileMode.CreateNew);
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

    /**
    * path：文件创建目录
    * name：文件的名称
    *  info：写入的内容
    */
    void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.AppendText();
        }
        //以行的形式写入信息
        sw.WriteLine(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
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

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (System. Exception ex)
        {
            throw new System.Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }


}
