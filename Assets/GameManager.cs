using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class GameManager : BaseGameObject
    {
        public static GameManager instance;

        private int currentLevelIndex;
        public string levelNames;

        public Menu mainMenu, gameOverMenu, nextLevelMenu;

        public void NextLevel()
        {
            nextLevelMenu.Show(() =>
            {
                LoadNextLevel();
                nextLevelMenu.Hide();
            });
        }

        public void GameOver()
        {
            gameOverMenu.Show(() =>
            {
                DestroyLevel();
                WaitForSeconds(() =>
                {
                    mainMenu.Show(() => { gameOverMenu.gameObject.SetActive(false); });
                    gameOverMenu.Hide();
                }, 1f);
            });
        }

        private void DestroyLevel()
        {
            GameObject currentLevel = GameObject.FindWithTag("Level");
            if (currentLevel != null)
                SceneManager.UnloadSceneAsync(currentLevel.scene);
        }

        private void LoadNextLevel()
        {
            DestroyLevel();

            string[] levels = levelNames.Split(',');
            if (currentLevelIndex >= 0 && currentLevelIndex < levels.Length)
            {
                SceneManager.LoadScene(string.Format("Scenes/Levels/{0}", levels[currentLevelIndex]), LoadSceneMode.Additive);
                currentLevelIndex++;
                PlayerController.instance.CheckGround();
            }
            else
            {
                StartMenu();
            }
        }

        void Awake()
        {
            instance = this;
            currentLevelIndex = 0;
        }

        public void StartGame()
        {
            currentLevelIndex = 0;
            LoadNextLevel();
            StartCoroutine(CoWaitForLoad());
        }

        private IEnumerator CoWaitForLoad()
        {
            yield return new WaitForFixedUpdate();
            PlayerController.instance.Respawn();
            FollowCamera.instance.SetOnTarget();
        }

        public void StartMenu()
        {
            mainMenu.Show();
        }

        private void Start()
        {
            mainMenu.OnClick = () =>
            {
                StartGame();
                mainMenu.Hide();
            };
            StartMenu();           
        }

        public void Exit()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}