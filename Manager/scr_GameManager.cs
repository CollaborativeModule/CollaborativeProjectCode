using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class scr_GameManager : MonoBehaviour
{
    public GameObject[] level1Worlds, level2Worlds;
    public GameObject level1Boss, level2Boss, currentLevel;
    public bool[] isLevel1Played, isLevel2Played;
    public float levelOn, numOfLevelsCanPlayInWorld, numOfLevelsInWorldPlayed;
    public bool nextLevelSelected;

    public GameObject mainMenu, loseMenu, winMenu, pauseMenu, optionsMenu, controlsMenu, previousMenu, InGameMenu;
    public bool canPause, isPaused, playerCanDoStuff;

    public GameObject pauseFirstButtonSelected, optionsFirstButtonSelected, controlsFirstButtonSelected, loseFirstButtonSelected, winFirstButtonSeleced, previousFirstButtonSelected, mainMenuFirstButtonSelected;

    public static scr_GameManager instance;

    public GameObject bossHealthBar, player;

    public RectTransform UIParticleEffectRectTransform;
    public GameObject UIButtonClickParticle;
    public bool useUnscaledTime = true;
    public ParticleSystem ps;

    public GameObject dash01, dash02, dash03;
    public GameObject jumpUp, dashUp, damageUp, rageItem, speedUp, orbIcon;
    public TMP_Text jumpUpText, dashUpText, damageUpText, rageItemText, speedUpText, orbIconText;

    private float levelMusic;

    public void Awake()
    {
        // setting values for particle system
        var main = ps.main;
        main.useUnscaledTime = useUnscaledTime;

        instance = this;

        previousFirstButtonSelected = mainMenuFirstButtonSelected;
        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButtonSelected);
    }

    public void NextLevel()
    {
        if (nextLevelSelected == false) // if the next level isn't already chosen then do this
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (levelOn == 1 && numOfLevelsInWorldPlayed < numOfLevelsCanPlayInWorld) // if on the first world but not done enough levels to do the boss fight
            {
                levelMusic = 1;
                scr_AudioManager.instance.music.setParameterByName("Music_State", levelMusic);
                scr_AudioManager.instance.caveEnvironment.start();
                scr_AudioManager.instance.player_Walk.setParameterByName("Walk_Surface", 0f);
                print("level 1 noraml world");
                int randomLevel = Random.Range(0, 5); // get random number between 0 and 5
                while (isLevel1Played[randomLevel] == true) // if the level is already selected then increase the num selected up to 5 where it resets back to 0 untill a level is found which hasn't been selected yet
                {
                    randomLevel++;
                    if (randomLevel == 5)
                    {
                        randomLevel = 0;
                    }
                }
                if (currentLevel != null) // if there was a previous level then trigger an event which deletes all the objects spawned and set the level as deactived
                {
                    currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
                    currentLevel.SetActive(false);
                }
                isLevel1Played[randomLevel] = true; // set this level as being selected
                level1Worlds[randomLevel].SetActive(true); // set this level as active 
                currentLevel = level1Worlds[randomLevel]; // set the level that has been selected as the current level
                level1Worlds[randomLevel].GetComponent<scr_LevelSpawnSystem>().LevelStart(); // spawns the objects for the level in
                scr_PlayerMovement.instance.StartOfLevel(); // allows the player to move and sets them to 0,0,0
                numOfLevelsInWorldPlayed++; // increases variable by 1
            }

            else if (levelOn == 1 && numOfLevelsInWorldPlayed >= numOfLevelsCanPlayInWorld) // if on level 1 but enough levels done for the boss fight
            {
                levelMusic = 2;
                scr_AudioManager.instance.music.setParameterByName("Music_State", levelMusic);
                print("level 1 boss world");
                level1Boss.SetActive(true); // set boss fight as active
                if (currentLevel != null) // if previous level exists, end previous level and deactivate
                {
                    currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
                    currentLevel.SetActive(false);
                }
                level1Boss.GetComponent<scr_LevelSpawnSystem>().LevelStart(); // spaws objects for current level
                bossHealthBar.SetActive(true); // actives boss HP bar
                scr_EnemyHealthBar.instance.bossEnemySetHealthBar(); // triggers event which sets the boss's HP Bar variables
                scr_PlayerMovement.instance.StartOfLevel(); // allows player to move
                numOfLevelsInWorldPlayed = 0f; // resets variable back to 0
                levelOn++; // increases levels played by 1
            }

            else if (levelOn == 2 && numOfLevelsInWorldPlayed < numOfLevelsCanPlayInWorld) // works same as previous comments just for lvl 2
            {
                scr_AudioManager.instance.caveEnvironment.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                scr_AudioManager.instance.desertEnvironment.start();
                levelMusic = 1;
                scr_AudioManager.instance.music.setParameterByName("Music_State", levelMusic);
                scr_AudioManager.instance.player_Walk.setParameterByName("Walk_Surface", 1f);
                int randomLevel = Random.Range(0, 5);
                while (isLevel2Played[randomLevel] == true)
                {
                    randomLevel++;
                    if (randomLevel == 5)
                    {
                        randomLevel = 0;
                    }
                }
                isLevel2Played[randomLevel] = true;
                level2Worlds[randomLevel].SetActive(true);
                if (currentLevel != null)
                {
                    currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
                    currentLevel.SetActive(false);
                }
                currentLevel = level2Worlds[randomLevel];
                level2Worlds[randomLevel].GetComponent<scr_LevelSpawnSystem>().LevelStart();
                scr_PlayerMovement.instance.StartOfLevel();
                numOfLevelsInWorldPlayed++;
            }

            else if (levelOn == 2 && numOfLevelsInWorldPlayed >= numOfLevelsCanPlayInWorld) // works same as previous comments just for level 2
            {
                levelMusic = 2;
                scr_AudioManager.instance.music.setParameterByName("Music_State", levelMusic);
                level1Boss.SetActive(true);
                if (currentLevel != null)
                {
                    currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
                    currentLevel.SetActive(false);
                }
                level1Boss.GetComponent<scr_LevelSpawnSystem>().LevelStart();
                bossHealthBar.SetActive(true);
                scr_EnemyHealthBar.instance.bossEnemySetHealthBar();
                scr_PlayerMovement.instance.StartOfLevel();
                numOfLevelsInWorldPlayed = 0f;
                levelOn++;
            }


            else if (levelOn == 3) // if player has beaten the 2 worlds
            {
                scr_AudioManager.instance.desertEnvironment.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                levelMusic = 0;
                Win(); // triggers win event
            }
        }

        scr_PlayerCombat.instance.OnHealed(); // triggers the on healed event which sets the player HP value's
    }

    public void PlayGame()
    {
        // plays UI button sound and particle effect
        scr_AudioManager.instance.ui.start();
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);

        // ends current level and sets it as false
        if (currentLevel != null)
        {
            currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
            currentLevel.SetActive(false);
        }

        for (int i = 0; i < isLevel1Played.Length; i++) // resets all the bools for knowing if a level is played back to default values
        {
            isLevel1Played[i] = false;
        }

        for (int i = 0; i < isLevel2Played.Length; i++)
        {
            isLevel2Played[i] = false;
        }

        levelOn = 1; // resets variables back to default
        numOfLevelsInWorldPlayed = 0;
        nextLevelSelected = false;
        canPause = true;
        playerCanDoStuff = true;
        isPaused = false;
        Time.timeScale = 1f; 

        InGameMenu.SetActive(true); // sets UI to In Game UI setting where only in game is enabled
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        scr_PlayerCombat.instance.GameStart();
        scr_PlayerMovement.instance.GameStart();
        scr_PlayerJump.instance.GameStart();

        NextLevel(); // spawns the level and objects in
    }

    public void LevelComplete()
    {
        // triggers next level after delay
        Invoke("NextLevel", 3f);
    }

    public void Update()
    {
        // unpauses the game
        if (canPause == true && isPaused == true && Input.GetButtonDown("Pause"))
        {
            isPaused = false;
            playerCanDoStuff = true;
            previousMenu = pauseMenu;
            previousMenu.SetActive(false);
            Time.timeScale = 1f;
            scr_AudioManager.instance.ui.start();
            scr_AudioManager.instance.music.setParameterByName("Music_State", levelMusic);
        }

        // pauses the game
        else if (canPause == true && isPaused == false && Input.GetButtonDown("Pause"))
        {
            isPaused = true;
            playerCanDoStuff = false;
            previousMenu = pauseMenu;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            scr_AudioManager.instance.ui.start();

            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set a new selected object
            EventSystem.current.SetSelectedGameObject(pauseFirstButtonSelected);
            previousFirstButtonSelected = pauseFirstButtonSelected;

            scr_AudioManager.instance.music.setParameterByName("Music_State", 0f);
        }

        // triggers selected button on controller when button presses
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject currentButton = EventSystem.current.currentSelectedGameObject;
            if (currentButton != null && currentButton.GetComponent<Slider>() == null)
            {
                currentButton.GetComponent<Button>().onClick.Invoke();
            }
        }

        // if in game
        if (playerCanDoStuff == true)
        {
            // sets number of dash UI visible
            float numOfDashes = scr_PlayerMovement.instance.numOfDashes;
            if (numOfDashes == 0)
            {
                dash01.SetActive(false);
                dash02.SetActive(false);                
                dash03.SetActive(false);
            }
            else if (numOfDashes == 1)
            {
                dash01.SetActive(true);
                dash02.SetActive(false);                
                dash03.SetActive(false);
            }
            else if (numOfDashes == 2)
            {
                dash01.SetActive(true);
                dash02.SetActive(true);                
                dash03.SetActive(false);
            }
            else if(numOfDashes == 3)
            {
                dash01.SetActive(true);
                dash02.SetActive(true);                
                dash03.SetActive(true);
            }
        }
    }

    public void Options()
    {
        scr_AudioManager.instance.ui.start();
        // activated UI particle effect, sets the particle effect position to the button pressed position then invokes a function which disables the particle effect
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButtonSelected);
        canPause = false;
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void ReturnFromOptions()
    {
        scr_AudioManager.instance.ui.start();
        // activated UI particle effect, sets the particle effect position to the button pressed position then invokes a function which disables the particle effect
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(previousFirstButtonSelected);
        previousMenu.SetActive(true);
        optionsMenu.SetActive(false);
        if (previousMenu == pauseMenu)
        {
            canPause = true;
        }
    }

    public void Lose()
    {

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(loseFirstButtonSelected);
        previousFirstButtonSelected = loseFirstButtonSelected;

        canPause = false;
        playerCanDoStuff = false;
        previousMenu = loseMenu;
        loseMenu.SetActive(true);
        pauseMenu.SetActive(false);
        InGameMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        winMenu.SetActive(false);
        controlsMenu.SetActive(false);

        currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
        currentLevel.SetActive(false);

        for (int i = 0; i < isLevel1Played.Length; i++) // resets all the bools for knowing if a level is played back to default values
        {
            isLevel1Played[i] = false;
        }
        for (int i = 0; i < isLevel2Played.Length; i++)
        {
            isLevel2Played[i] = false;
        }
        levelOn = 1; // resets variables back to default
        numOfLevelsInWorldPlayed = 0;
        nextLevelSelected = false;
        canPause = false;
        playerCanDoStuff = false;
        isPaused = false;
        Time.timeScale = 1f; 
        scr_PlayerCombat.instance.GameEnd();
        scr_PlayerMovement.instance.GameEnd();
        scr_PlayerJump.instance.GameEnd();

        scr_AudioManager.instance.caveEnvironment.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        scr_AudioManager.instance.desertEnvironment.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        scr_AudioManager.instance.music.setParameterByName("Music_State", 0f);
        scr_AudioManager.instance.player_Walk.setParameterByName("Walk_Surface", 0f);
    }

    public void Win()
    {

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(winFirstButtonSeleced);
        previousFirstButtonSelected = winFirstButtonSeleced;

        canPause = false;
        playerCanDoStuff = false;
        previousMenu = winMenu;
        winMenu.SetActive(true);
        pauseMenu.SetActive(false);
        InGameMenu.SetActive(false);
        loseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        winMenu.SetActive(false);
        controlsMenu.SetActive(false);

        currentLevel.GetComponent<scr_LevelSpawnSystem>().EndLevel();
        currentLevel.SetActive(false);

        for (int i = 0; i < isLevel1Played.Length; i++) // resets all the bools for knowing if a level is played back to default values
        {
            isLevel1Played[i] = false;
        }
        for (int i = 0; i < isLevel2Played.Length; i++)
        {
            isLevel2Played[i] = false;
        }
        levelOn = 1; // resets variables back to default
        numOfLevelsInWorldPlayed = 0;
        nextLevelSelected = false;
        canPause = false;
        playerCanDoStuff = false;
        isPaused = false;
        Time.timeScale = 1f; 
        scr_PlayerCombat.instance.GameEnd();
        scr_PlayerMovement.instance.GameEnd();
        scr_PlayerJump.instance.GameEnd();

        scr_AudioManager.instance.caveEnvironment.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        scr_AudioManager.instance.desertEnvironment.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        scr_AudioManager.instance.music.setParameterByName("Music_State", 0f);
        scr_AudioManager.instance.player_Walk.setParameterByName("Walk_Surface", 0f);
    }

    public void Controls()
    {
        scr_AudioManager.instance.ui.start();
        // activated UI particle effect, sets the particle effect position to the button pressed position then invokes a function which disables the particle effect
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(controlsFirstButtonSelected);

        canPause = false;
        controlsMenu.SetActive(true);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ReturnFromControls()
    {
        scr_AudioManager.instance.ui.start();
        // activated UI particle effect, sets the particle effect position to the button pressed position then invokes a function which disables the particle effect
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(previousFirstButtonSelected);

        previousMenu.SetActive(true);
        controlsMenu.SetActive(false);
        if (previousMenu == pauseMenu)
        {
            canPause = true;
        }
    }

    public void Quit()
    {
        scr_AudioManager.instance.ui.start();
        // activated UI particle effect, sets the particle effect position to the button pressed position then invokes a function which disables the particle effect
        UIButtonClickParticle.SetActive(true);
        UIParticleEffectRectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
        Invoke("DisabledUIParticleEffects", 1f);

        Application.Quit();
    }

    public void DisabledUIParticleEffects()
    {
        // disables particle effect for UI button click
        UIButtonClickParticle.SetActive(false);
    }
}
