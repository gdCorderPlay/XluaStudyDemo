using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// assetBundle 打包方法
/// </summary>
public  class CreatAssetBundle :Editor {

    [MenuItem("GD/CreatBundle")]
    public static void CreatBundle()
    {


        if (!System.IO.Directory.Exists("AssetBundles"))
        {
            System.IO.Directory.CreateDirectory("AssetBundles");
        }
        //BuildPipeline.BuildAssetBundles("AssetBundles/" + EditorUserBuildSettings.activeBuildTarget.ToString(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions
            .ForceRebuildAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        Debug.Log(" bundle build success!!!! ");

    }
    [MenuItem("GD/CleanCache")]
    public static void CleanAssetBundle()
    {
        Caching.ClearCache();
    }
	
}
