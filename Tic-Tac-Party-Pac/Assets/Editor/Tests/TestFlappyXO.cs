using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestFlappyXO
    {
        GameObject canvas;
        GameObject start;
        GameObject GameOver;
        GameObject X;
        GameObject O;
        TapController XTap;
        TapController OTap;
        GameManager GM;
        GameObject environment;
        [SetUp]
        public void Setup()
        {
            GM = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/FlappyXO/Main Camera")).GetComponent<GameManager>();
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/FlappyXO/Canvas"));
            X = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/FlappyXO/x"));
            O = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/FlappyXO/o"));
            start = canvas.transform.GetChild(2).gameObject;
            GameOver = canvas.transform.GetChild(3).gameObject;
            XTap = X.GetComponent<TapController>();
            OTap = O.GetComponent<TapController>();
            GM.SetStartPage(start);
            GM.gameOverPage = GameOver;
            environment = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/FlappyXO/Environment"));
        }
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(start);
            Object.Destroy(GameOver);
            Object.Destroy(X);
            Object.Destroy(O);
            Object.Destroy(XTap);
            Object.Destroy(OTap);
            Object.Destroy(GM);
            Object.Destroy(environment);
        }
        [UnityTest]
        public IEnumerator TestFlappyXOInit()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.True(start.activeSelf);
            Assert.False(GameOver.activeSelf);
        }
        [UnityTest]
        public IEnumerator TestFlappyXOStarts()
        {
            GM.StartGame();
            yield return new WaitForSeconds(0.1f);
            Assert.False(start.activeSelf);
        }
        [UnityTest]
        public IEnumerator TestFlappyXOFalls()
        {
            Vector2 initPos = X.transform.position;
            GM.StartGame();
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(initPos.y, X.transform.position.y);
        }
        [UnityTest]
        public IEnumerator TestFlappyXOFlaps()
        {
            GM.StartGame();
            yield return new WaitForSeconds(0.1f);
            Vector2 initPos = X.transform.position;
            XTap.Tapped();
            yield return new WaitForSeconds(0.1f);
            Assert.Less(initPos.y, X.transform.position.y);
        }
        [UnityTest]
        public IEnumerator TestFlappyXODies()
        {
            GM.StartGame();
            Time.timeScale = 10.0f;
            yield return new WaitForSeconds(5.0f);
            Vector2 initPos = X.transform.position;
            yield return new WaitForSeconds(0.3f);
            Time.timeScale = 1.0f;
            Assert.AreEqual(initPos.y, X.transform.position.y);
        }
    }
}
