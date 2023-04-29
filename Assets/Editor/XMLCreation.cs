using System.IO;
using System.Xml;
using UnityEditor;

public class XMLCreation : Editor
{
    [MenuItem("Assets/XML Creation/Default One")]
    public static void CreatDefaultOne()
    {
        string tFile;

        if (Selection.activeObject != null)
        {
            tFile = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (Directory.Exists(tFile))
                tFile = Path.Combine(tFile, "DefaultOne.xml");
            else if (File.Exists(tFile))
                tFile = Path.Combine(Path.GetDirectoryName(tFile), "DefaultOne.xml");
        }
        else
            tFile = "Assets/DefaultOne.xml";

        tFile = AssetDatabase.GenerateUniqueAssetPath(tFile);

        XmlWriter tWriter = XmlWriter.Create(tFile);
        tWriter.WriteElementString("defaultone", "");
        tWriter.Close();

        AssetDatabase.ImportAsset(tFile);
    }
}
