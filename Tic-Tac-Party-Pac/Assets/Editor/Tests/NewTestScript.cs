using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class NewTestScript
    {

        private Scene scene;

        [SetUp]
        public void LoadScene()
        {
            scene = EditorSceneManager.OpenScene("Assets/Scenes/TitleScene.unity",
                             OpenSceneMode.Single);
        }
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            GameObject obj = GameObject.Find("Begin Test");
            //ColorChangerTest script = obj.GetComponent<ColorChangerTest>();
            //Assert.IsNotNull(script);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        /*
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */
    }
}
