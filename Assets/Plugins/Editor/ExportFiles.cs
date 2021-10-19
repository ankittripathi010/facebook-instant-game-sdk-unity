using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

namespace FBInstant
{
    public class ExportFiles
    {

        [PostProcessBuild()]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            List<string> filesPathSource = new List<string>();
            filesPathSource.Add(Application.dataPath + "/Plugins/TemplateFile/index.html");
            filesPathSource.Add(Application.dataPath + "/Plugins/TemplateFile/TemplateData/UnityProgress.js");
            filesPathSource.Add(Application.dataPath + "/Plugins/TemplateFile/fbapp-config.json");

            List<string> filesPathDes = new List<string>();
            filesPathDes.Add(pathToBuiltProject + "/index.html");
            filesPathDes.Add(pathToBuiltProject + "/TemplateData/UnityProgress.js");
            filesPathDes.Add(pathToBuiltProject + "/fbapp-config.json");

            string path = pathToBuiltProject;
            string[] subPath = path.Split('/');

            for (int i = 0; i < filesPathSource.Count; i++)
            {
                File.Copy(filesPathSource[i], filesPathDes[i], true);
            }


            string indexFileData = File.ReadAllText(pathToBuiltProject + "/index.html");
            if (indexFileData.Contains("Build/Build.json"))
            {
                indexFileData = indexFileData.Replace("Build/Build.json", "Build/" + subPath[subPath.Length - 1] + ".json");
                File.WriteAllText(pathToBuiltProject + "/index.html", indexFileData);
            }

            string compat = File.ReadAllText(pathToBuiltProject + "/Build/UnityLoader.js");

            string s = GetLine(pathToBuiltProject + "/Build/UnityLoader.js", 4);
            string r = s.Substring(1564, 527);

            if (compat.Contains(r))
            {
                r = compat.Replace(r, "t();");
                File.WriteAllText(pathToBuiltProject + "/Build/UnityLoader.js", r);
                Debug.Log("match found");
            }
            else
            {
                Debug.Log("match not found");
            }

        }

        static string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }
    }

}
