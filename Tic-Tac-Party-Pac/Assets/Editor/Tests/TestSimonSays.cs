using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestSimonSays
    {
        private GameObject canvas;
        private GameObject Game;
        private GameObject Winner;
        private SimonGameState GS;
        private SimonFinish SF;
        private SimonHandler SH;

        [SetUp]
        public void Setup()
        {
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SimonSays/Canvas"));
            Game = canvas.transform.GetChild(1).gameObject;
            Winner = canvas.transform.GetChild(2).gameObject;
            GS = canvas.GetComponent<SimonGameState>();
            SF = canvas.GetComponent<SimonFinish>();
            SH = Game.transform.GetChild(1).gameObject.GetComponent<SimonHandler>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(Game);
            Object.Destroy(Winner);
            Object.Destroy(GS);
            Object.Destroy(SF);
            Object.Destroy(SH);
        }

        [UnityTest]
        public IEnumerator TestInit()
        {
            yield return null;
            Assert.False(Winner.activeSelf);
            Assert.True(Game.activeSelf);
            Assert.AreEqual("Player One", Game.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
            Assert.AreEqual(new Color(0,0,0), Game.transform.GetChild(3).gameObject.GetComponent<Image>().color);
            Assert.AreEqual("Round 1", Game.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

        [UnityTest]
        public IEnumerator TestSingle()
        {
            SH.Clicked("Red");
            yield return new WaitForFixedUpdate();
            Assert.AreEqual("Player Two", Game.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
            Assert.AreEqual(new Color(255 / 255f, 6 / 255f, 0), Game.transform.GetChild(3).gameObject.GetComponent<Image>().color);
            Assert.AreEqual("Round 2", Game.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

        [UnityTest]
        public IEnumerator TestWins()
        {
            SH.Clicked("Red");
            yield return new WaitForFixedUpdate();
            SH.Clicked("Blue");
            yield return new WaitForSeconds(0.3f);
            Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
        }

        [UnityTest]
        public IEnumerator TestDouble()
        {
            SH.Clicked("Red");
            yield return new WaitForFixedUpdate();
            SH.Clicked("Red");
            yield return new WaitForFixedUpdate();
            SH.Clicked("Blue");
            yield return new WaitForFixedUpdate();
            Assert.AreEqual("Player One", Game.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
            Assert.AreEqual(new Color(0, 60 / 255f, 255 / 255f), Game.transform.GetChild(3).gameObject.GetComponent<Image>().color);
            Assert.AreEqual("Round 3", Game.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

    }
}
