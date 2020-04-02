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
        Scene scene;
        private GameObject canvas;
        private GameObject rope;
        private RopeHandler RH;
        private Button_Handler BH;

        [SetUp]
        public void Setup()
        {
            scene = SceneManager.GetActiveScene();
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TugOfWarGame/Canvas"));
            rope = canvas.transform.GetChild(0).gameObject;
            RH = rope.GetComponent<RopeHandler>();
            BH = canvas.transform.GetChild(2).gameObject.GetComponent<Button_Handler>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(rope);
            Object.Destroy(canvas);
            Object.Destroy(RH);
            Object.Destroy(BH);
        }

        // // A Test behaves as an ordinary method
        // [Test]
        // public void TestTugOfWarGameSimplePasses()
        // {
        //     // Use the Assert class to test conditions

        // }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestRopeMovesRight()
        {
            // Use the Assert class to test conditions.
            float initialXPos = rope.transform.position.x;
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
