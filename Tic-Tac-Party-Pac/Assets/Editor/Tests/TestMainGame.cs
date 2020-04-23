using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestMainGame
    {
        GameObject canvas;
        GameObject GamePlayGO;
        GamePlay Game;
        [SetUp]
        public void Setup()
        {
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameScene/Canvas"));
            GamePlayGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameScene/GamePlay"));
            Game = GamePlayGO.GetComponent<GamePlay>();
        }
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(GamePlayGO);
            Object.Destroy(Game);
        }
        [UnityTest]
        public IEnumerator TestPlaceCell()
        {
            Game.GameSetup();
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(0);
            Assert.AreEqual(Game.playedCells[0], 1);
        }
        [UnityTest]
        public IEnumerator TestPlaceCellTwo()
        {
            Game.GameSetup();
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(0);
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(1);
            Assert.AreEqual(Game.playedCells[1], 2);
        }
        [UnityTest]
        public IEnumerator TestTTTMoveToMinigame()
        {
            Game.GameSetup();
            yield return new WaitForSeconds(0.3f);
            Scene sc = SceneManager.GetActiveScene();
            Game.TicTacToe(0);
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(1);
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(3);
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(2);
            yield return new WaitForSeconds(0.3f);
            Game.TicTacToe(6);
            yield return new WaitForSeconds(0.3f);
            Assert.AreNotEqual(SceneManager.GetActiveScene(), sc);

        }
        [UnityTest]
        public IEnumerator TestInit()
        {
            Game.GameSetup();
            yield return new WaitForSeconds(0.3f);
            
            for (int i = 0; i < Game.playedCells.Length; i++)
            {
                Assert.AreEqual(Game.playedCells[i], 0); 
            }
            for (int i = 0; i < Game.playedTiles.Length; i++)
            {
                Assert.AreEqual(Game.playedTiles[i], 0); 
            }
            for (int i = 0; i < Game.winningLine.Length; i++)
            {
                Assert.IsTrue(!Game.winningLine[i].activeSelf);
            }
            for (int i = 0; i < Game.winningLines.Length; i++)
            {
                Assert.AreEqual(Game.winningLines[i], 0);
            }
            for (int i = 0; i < Game.winningShades.Length; i++)
            {
                Assert.IsTrue(!Game.winningShades[i].activeSelf);
            }
            for (int i = 0; i < Game.bigWinLine.Length; i++)
            {
                Assert.IsTrue(!Game.bigWinLine[i].activeSelf);
            }
            Assert.IsTrue(!Game.gameOverPage.activeSelf);
            Assert.IsTrue(Game.gameBoard.activeSelf);
            Assert.AreEqual(Game.turn, 0);
            Assert.AreEqual(Game.turns, 0);
        }
        [UnityTest]
        public IEnumerator TestMoveToMM()
        {
            Game.GameSetup();
            yield return new WaitForSeconds(0.3f);
            Scene sc = SceneManager.GetActiveScene();
            Game.MainMenu();
            yield return new WaitForSeconds(0.3f);
            Assert.AreNotEqual(SceneManager.GetActiveScene(), sc);
        }
    }
}
