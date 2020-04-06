using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


namespace Tests
{
    public class TestTugOfWarGame
    {
        private GameObject canvas;
        private GameObject rope;
        private RopeHandler RH;
        private Button_Handler BH;
        private GameObject winner;
        private RopeFinish RF;

        [SetUp]
        public void Setup()
        {
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TugOfWarGame/Canvas"));
            rope = canvas.transform.GetChild(0).gameObject;
            RH = rope.GetComponent<RopeHandler>();
            BH = canvas.transform.GetChild(2).gameObject.GetComponent<Button_Handler>();
            winner = canvas.transform.GetChild(4).gameObject;
            RF = canvas.transform.GetChild(1).gameObject.GetComponent<RopeFinish>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(rope);
            Object.Destroy(canvas);
            Object.Destroy(RH);
            Object.Destroy(BH);
            Object.Destroy(winner);
            Object.Destroy(RF);
        }
        [UnityTest]
        public IEnumerator TestRopeMovesRight()
        {
            float initialXPos = rope.transform.position.x;
            RH.move("RButton");
            yield return new WaitForSeconds(0.3f);
            Assert.Greater(rope.transform.position.x, initialXPos);
        }
        [UnityTest]
        public IEnumerator TestRopeMovesLeft()
        {
            float initialXPos = rope.transform.position.x;
            RH.move("LButton");
            yield return new WaitForSeconds(0.3f);
            Assert.Less(rope.transform.position.x, initialXPos);
        }
        [UnityTest]
        public IEnumerator TestRopeVelocityIncreases()
        {
            float X1 = rope.transform.position.x;
            RH.move("RButton");
            yield return new WaitForSeconds(0.3f);
            float X2 = rope.transform.position.x;
            RH.move("RButton");
            yield return new WaitForSeconds(0.3f);
            float X3 = rope.transform.position.x;
            Assert.Greater(X3 - X2, X2 - X1);
        }
        [UnityTest]
        public IEnumerator TestPLWins()
        {
            RH.move("LButton");
            RH.move("LButton");
            RH.move("LButton");
            RH.move("LButton");
            RH.move("LButton");
            Time.timeScale = 100.0f;
            yield return new WaitForSeconds(50.0f);
            Assert.IsTrue(winner.activeSelf);
            Assert.AreEqual("Winner: Player 1", winner.GetComponent<TMPro.TextMeshProUGUI>().text);
            Time.timeScale = 1.0f;
        }
        [UnityTest]
        public IEnumerator TestPRWins()
        {
            RH.move("RButton");
            RH.move("RButton");
            RH.move("RButton");
            RH.move("RButton");
            RH.move("RButton");
            Time.timeScale = 100.0f;
            yield return new WaitForSeconds(50.0f);
            Assert.IsTrue(winner.activeSelf);
            Assert.AreEqual("Winner: Player 2", winner.GetComponent<TMPro.TextMeshProUGUI>().text);
            Time.timeScale = 1.0f;
        }
    }
}
