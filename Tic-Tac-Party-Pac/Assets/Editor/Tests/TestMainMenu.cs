using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestMainMenu
    {
        private GameObject MMGO;
        private MainMenu MM;
        [SetUp]
        public void Setup()
        {
            GameObject canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/MainMenu/Canvas"));
            MMGO = canvas.transform.GetChild(2).gameObject;
            MM = MMGO.GetComponent<MainMenu>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(MMGO);
            Object.Destroy(MM);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestNewScene()
        {
            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestHelp()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
