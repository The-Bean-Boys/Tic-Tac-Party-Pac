using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestBalloonMinigame
    {
        GameObject XScoreGO;
        GameObject OScoreGO;
        GameObject SpawnerGO;
        GameObject WinnerGO;
        Spawner Spawner;
        Score Xscore;
        Score Oscore;
        BalloonHandler BH;
        [SetUp]
        public void Setup()
        {
            SpawnerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/Spawner"));
            XScoreGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/X Score"));
            OScoreGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/O Score"));
            WinnerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/Winner"));
            Spawner = SpawnerGO.GetComponent<Spawner>();
            Xscore = XScoreGO.GetComponent<Score>();
            Oscore = OScoreGO.GetComponent<Score>();
            Spawner.winnerText = WinnerGO;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(XScoreGO);
            Object.Destroy(OScoreGO);
            Object.Destroy(Xscore);
            Object.Destroy(Oscore);
            Object.Destroy(SpawnerGO);
            Object.Destroy(Spawner);
            Object.Destroy(WinnerGO);
            Object.Destroy(BH);
        }
        [UnityTest]
        public IEnumerator TestBalloonRises()
        {
            GameObject Balloon = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/Blue Balloon"));
            Vector2 BT = Balloon.transform.position;
            yield return new WaitForSeconds(0.3f);
            Assert.Greater(Balloon.transform.position.y, BT.y);
        }
        [UnityTest]
        public IEnumerator TestBalloonPops()
        {
            BalloonManagerStatic.Otext = Oscore;
            BalloonManagerStatic.Xtext = Xscore;
            GameObject Balloon = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/Blue Balloon"));
            yield return new WaitForFixedUpdate();
            BH = Balloon.GetComponent<BalloonHandler>();
            BH.OnMouseDown();
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(1, Xscore.getScore());
        }

        [UnityTest]
        public IEnumerator TestPXWins()
        {
            BalloonManagerStatic.Otext = Oscore;
            BalloonManagerStatic.Xtext = Xscore;
            GameObject Balloon = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/Blue Balloon"));
            yield return new WaitForFixedUpdate();
            BH = Balloon.GetComponent<BalloonHandler>();
            BH.OnMouseDown();
            Time.timeScale = 100.0f;
            yield return new WaitForSeconds(20.1f);
            Assert.IsTrue(WinnerGO.activeSelf);
            Assert.AreEqual("X Wins!", WinnerGO.GetComponent<TMPro.TextMeshPro>().text);
            Time.timeScale = 1.0f;
        }
        [UnityTest]
        public IEnumerator TestPOWins()
        {
            BalloonManagerStatic.Otext = Oscore;
            BalloonManagerStatic.Xtext = Xscore;
            GameObject Balloon = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Balloon/Green Balloon"));
            yield return new WaitForFixedUpdate();
            BH = Balloon.GetComponent<BalloonHandler>();
            BH.OnMouseDown();
            Time.timeScale = 100.0f;
            yield return new WaitForSeconds(20.1f);
            Assert.IsTrue(WinnerGO.activeSelf);
            Assert.AreEqual("O Wins!", WinnerGO.GetComponent<TMPro.TextMeshPro>().text);
            Time.timeScale = 1.0f;
        }
    }
}
