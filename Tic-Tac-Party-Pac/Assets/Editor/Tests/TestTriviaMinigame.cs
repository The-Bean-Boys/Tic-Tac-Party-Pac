using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestTriviaMinigame
    {
        GameObject canvas;
        Game_Script Game;
        [SetUp]
        public void Setup()
        {
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TriviaMinigame/Canvas"));
            Game = canvas.GetComponent<Game_Script>();
        }
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(Game);
        }
        [UnityTest]
        public IEnumerator TestTriviaMinigameInit()
        {
            yield return new WaitForSeconds(0.3f);
            Assert.IsFalse(Game.playerTwoStart.activeSelf);
            Assert.IsFalse(Game.winnerDisplay.activeSelf);
            Assert.IsFalse(Game.playAgainButton.activeSelf);
            
        }
    }
}
