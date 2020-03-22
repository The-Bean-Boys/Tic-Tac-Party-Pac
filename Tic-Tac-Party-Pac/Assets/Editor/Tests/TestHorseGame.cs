using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestHorseGame
    {
        Scene scene;
        private GameObject canvas;
        private GameObject horse;
        private GameObject horse2;
        private HorseHandler HH;
        private ButtonHandler BH;
        [SetUp]
        public void Setup()
        {
            scene = SceneManager.GetActiveScene();
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/HorseGame/Canvas"));
            horse = canvas.transform.GetChild(0).GetChild(0).gameObject;
            horse2 = canvas.transform.GetChild(1).GetChild(0).gameObject;
            HH = horse.GetComponent<HorseHandler>();
            BH = canvas.transform.GetChild(0).gameObject.GetComponent<ButtonHandler>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(horse);
            Object.Destroy(canvas);
            Object.Destroy(HH);
            Object.Destroy(BH);
        }
        // Tests that the horses move right
        [UnityTest]
        public IEnumerator TestHorseMovesRight()
        {
            float initialXPos = horse.transform.position.x;
            yield return new WaitForSeconds(0.3f);
            Assert.Greater(horse.transform.position.x, initialXPos);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestClickWorks()
        {
            BH.OnClick();
            yield return new WaitForSeconds(0.3f);
            Assert.Greater(horse.transform.position.x, horse2.transform.position.x);
        }
    }
}
