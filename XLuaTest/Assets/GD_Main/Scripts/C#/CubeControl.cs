using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
[Hotfix]
public class CubeControl : MonoBehaviour {
    /// <summary>
    /// 当前lua 脚本的MD5值
    /// </summary>
   string localMD5="0000";
	
	void Start () {
        //StartCoroutine(CheckAsset());
        Debug.Log(2.5f%2 );
    }
	
	
	void Update () {
        HotfixFunction();
    }

    /// <summary>
    /// 可能需要热更的代码
    /// </summary>
    void HotfixFunction()
    {
        transform.Rotate(Vector3.left);
    }
    /// <summary>
    /// 不断的检测是否需要更新
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckAsset()
    {
        while (true)
        {
            //十秒检测一次
            yield return new WaitForSeconds(10);

            Debug.Log("开始比对信息");
            ///获取服务器上的版本信息
            string serverPath = "http://localhost/HotfixAsset/";
            WWW www = new WWW(serverPath+"test.txt");
            yield return www;
            byte[] data = www.bytes;
            string str = System.Text.Encoding.UTF8.GetString(data);
            Dictionary<string, string> ServerVersionInfo =Tools. ParseTextToDictionary(str);
            foreach( string key in ServerVersionInfo.Keys)
            {
                Debug.Log(key);
                if (!ServerVersionInfo[key].Equals(localMD5))
                {
                     www = new WWW(serverPath+key);
                    yield return www;
                    byte[] luadata = www.bytes;
                    string context = System.Text.Encoding.UTF8.GetString(luadata);
                   
                    FixFunction(context);
                    localMD5 = ServerVersionInfo[key];
                    Debug.Log("更新逻辑"+localMD5);
                }
               // transform.Translate
            }


        }
    }
    void FixFunction(string context )
    {
        LuaEnv luaenv = new LuaEnv();
        luaenv.DoString(@context);
        //luaenv.Dispose();
    }
}
