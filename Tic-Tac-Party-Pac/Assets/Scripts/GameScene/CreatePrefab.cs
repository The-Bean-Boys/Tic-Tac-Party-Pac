using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreatePrefab : MonoBehaviour
{
    static int index = 0;
    [MenuItem("Extras/Create Prefab For All Children")]
    static void DoCreatePrefab()
    {
        Transform[] transforms = Selection.transforms;
        CPrefab(transforms[0]);
        
    }

    static void CPrefab(Transform t)
    {
        for(int i = 0; i < t.childCount; i++)
        {
            CPrefab(t.GetChild(i));
        }
        string str = t.gameObject.name;
        switch (str)
        {
            case "T":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "M":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "B":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "L":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "MV":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "R":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "HR":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                break;
            case "HL":
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + index + ".prefab", InteractionMode.UserAction);
                index++;
                break;
            default:
                PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/GameScene/" + t.gameObject.name + ".prefab", InteractionMode.UserAction);
                break;
        }

    }
    [MenuItem("Extras/Create Prefabs For Selection")]
    static void createLotsOfPrefabs()
    {
        Transform[] transforms = Selection.transforms;
        foreach(Transform t in transforms)
        {
            PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, "Assets/Resources/Prefabs/MentalMath/" + t.gameObject.name + ".prefab", InteractionMode.UserAction);
        }
    }
}