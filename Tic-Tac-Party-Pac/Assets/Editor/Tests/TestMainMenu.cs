using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestMainMenu
    {
        Scene scene;
        private GameObject canvas;
        private GameObject MMGO;
        private MainMenu MM;
        [SetUp]
        public void Setup()
        {
            scene = SceneManager.GetActiveScene();
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));
            MMGO = canvas.transform.GetChild(2).gameObject;
            MM = MMGO.GetComponent<MainMenu>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(MMGO);
            Object.Destroy(MM);
        }

        // Tests if the playgame method opens the correct scene
        /*[UnityTest]
        public IEnumerator TestPlayGame()
        {
            MM.PlayGame();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
        }*/

    }
}
