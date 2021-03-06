﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CSharpCallBat :Editor {
    
    [MenuItem("GD/IO/CSharpCallOutExe")]
    public static void CSharpCallOutProgram()
    {
        //   C: \Users\Administrator\Documents\Visual Studio 2015\Projects\test1\test1\bin\Debug
        // System.Diagnostics.Process.Start("C:\\Windows\\system32\\calc.exe");
        string[] strs = new string[] { "gd", "123", "over" };
        System.Diagnostics.Process task=   System.Diagnostics.Process.Start(@"D:\Root\test1.exe", "1 2 3");
        task.WaitForExit();
        Debug.Log(task.ExitCode);
       // ExecuteProgram();
    }
    /// <summary>
    /// 调用外部应用程序
    /// </summary>
    public static  bool ExecuteProgram(string exeFilename, string workDir, string args)
    {
        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
        info.FileName = exeFilename;
        info.WorkingDirectory = workDir;
        info.RedirectStandardOutput = true;
        info.RedirectStandardError = true;
        info.Arguments = args;
        info.UseShellExecute = false;
        info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

        System.Diagnostics.Process task = null;

        bool rt = true;

        try
        {
             task = System.Diagnostics.Process.Start(info);
           // C:\Windows\system32
           // task = System.Diagnostics.Process.Start("C:\\Windows\\system32\\calc.exe");
            if (task != null)
            {
                task.WaitForExit(10000);
            }
            else
            {
                return false;
            }
        }
        catch (System. Exception e)
        {
            Debug.LogError("Error: " + e.ToString());
            return false;
        }
        finally
        {
            if (task != null && task.HasExited)
            {
                string output = task.StandardError.ReadToEnd();
                if (output.Length > 0)
                {
                    Debug.LogError(output);
                }

                output = task.StandardOutput.ReadToEnd();
                if (output.Length > 0)
                {
                    Debug.Log("Error: " + output);
                }

                rt = (task.ExitCode == 0);
            }
        }

        return rt;
    }
    /// <summary>
    /// 调用批处理文件
    /// </summary>
    static bool ExecuteProgram_Bat(string exeFilename, string workDir, string args)
  {  
      System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();  
      info.FileName = exeFilename;  
      info.WorkingDirectory = workDir;  
      info.UseShellExecute = true;  
      info.Arguments = args;  
      info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;  
    
     System.Diagnostics.Process task = null;  
     bool rt = true;  
     try  
     {  
         Debug.Log("ExecuteProgram:" + args);  
           
         task = System.Diagnostics.Process.Start(info);  
         if (task != null)  
         {  
             task.WaitForExit(100000);  
         }  
         else  
         {  
             return false;  
         }  
     }  
     catch (System. Exception e)  
     {  
         Debug.LogError("ExecuteProgram:" + e.ToString());  
         return false;  
     }   
     finally  
     {  
         if (task != null && task.HasExited)  
         {  
             rt = (task.ExitCode == 0);  
        }  
     }  
   
     return rt;  
 }  
}
