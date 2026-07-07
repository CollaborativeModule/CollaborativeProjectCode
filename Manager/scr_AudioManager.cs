using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scr_AudioManager : MonoBehaviour
{
    public static scr_AudioManager instance;

    public Slider musicSlider, sFXSlider;
    public TMP_Text musicSliderText, sFXSliderText;

    // sound event references
    public FMOD.Studio.EventInstance music;
    public FMOD.Studio.EventInstance armouredSkeletonEnemy;
    public FMOD.Studio.EventInstance world_1_Boss_Enemy;
    public FMOD.Studio.EventInstance world_2_Boss_Enemy;
    public FMOD.Studio.EventInstance caveEnvironment;
    public FMOD.Studio.EventInstance desertEnvironment;
    public FMOD.Studio.EventInstance dogs_Enemy;
    public FMOD.Studio.EventInstance environmentBreakAbles;
    public FMOD.Studio.EventInstance fire_Mummy_Enemy;
    public FMOD.Studio.EventInstance golem_Mummy_Enemy;
    public FMOD.Studio.EventInstance griffen_Enemy;
    public FMOD.Studio.EventInstance mimic_Enemy;
    public FMOD.Studio.EventInstance pickup;
    public FMOD.Studio.EventInstance player_Attack;
    public FMOD.Studio.EventInstance player_Dash;
    public FMOD.Studio.EventInstance player_Dead;
    public FMOD.Studio.EventInstance player_Hit;
    public FMOD.Studio.EventInstance player_Jump;
    public FMOD.Studio.EventInstance player_Orb;
    public FMOD.Studio.EventInstance player_Rage;
    public FMOD.Studio.EventInstance player_Shield;
    public FMOD.Studio.EventInstance player_Walk;
    public FMOD.Studio.EventInstance skeleton_Enemy;
    public FMOD.Studio.EventInstance slime_Enemies;
    public FMOD.Studio.EventInstance ui;
    public FMOD.Studio.EventInstance zombie_Enemy;

    //volume control settings references
    public FMOD.Studio.VCA musicVolumeController;
    public FMOD.Studio.VCA sFXVolumeController;

    public void Awake()
    {
        instance = this;

        // setting all the music references and parameters
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        music.start();
        music.setParameterByName("Music_State", 0f);
        musicVolumeController = FMODUnity.RuntimeManager.GetVCA("vca:/Music_Volume_Control");
        sFXVolumeController = FMODUnity.RuntimeManager.GetVCA("vca:/SFX_Volume_Control");

        armouredSkeletonEnemy = FMODUnity.RuntimeManager.CreateInstance("event:/Armoured_Skeleton_Enemy");
        world_1_Boss_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Boss_World_1");
        world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 0f);
        world_2_Boss_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Boss_World_2");
        world_2_Boss_Enemy.setParameterByName("Boss_World_2_State", 0f);
        caveEnvironment = FMODUnity.RuntimeManager.CreateInstance("event:/Cave_Environment");
        desertEnvironment = FMODUnity.RuntimeManager.CreateInstance("event:/Desert_Environment");
        dogs_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Dogs_Enemy");
        environmentBreakAbles = FMODUnity.RuntimeManager.CreateInstance("event:/Enivronment_Breakables");
        scr_AudioManager.instance.environmentBreakAbles.setParameterByName("Breakable_Object", 0f);
        fire_Mummy_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Fire_Mummy_Enemy");
        golem_Mummy_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Golem_Enemy");
        griffen_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Griffin_Enemy");
        mimic_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Mimic_Enemy");
        pickup = FMODUnity.RuntimeManager.CreateInstance("event:/PickUp");
        player_Attack = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Attack");
        player_Attack.setParameterByName("isFireAttack", 0f);
        player_Attack.setParameterByName("isOrbHit", 0f);
        player_Dash = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Dash");
        player_Dead = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Dead");
        player_Hit = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Hit");
        player_Jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Jump");
        player_Orb = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Orb");
        player_Rage = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Rage");
        player_Rage.setParameterByName("Player_Rage_Status", 0f);
        player_Shield = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Shield");
        player_Walk = FMODUnity.RuntimeManager.CreateInstance("event:/Player_Walk");
        player_Walk.setParameterByName("Walk_Surface", 0f);
        skeleton_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Skeleton_Enemy");
        slime_Enemies = FMODUnity.RuntimeManager.CreateInstance("event:/Slime_Enemies");
        ui = FMODUnity.RuntimeManager.CreateInstance("event:/UI");
        zombie_Enemy = FMODUnity.RuntimeManager.CreateInstance("event:/Zombie_Enemy");
    }

    public void VolumeChange() // triggered when changing volume in options
    {
        musicVolumeController.setVolume(musicSlider.value); // set the music volume to the inputted variable
        musicSliderText.text = Mathf.Round(musicSlider.value * 100).ToString(); // convert text displayed to 0 d.p and then display that value as text
        sFXVolumeController.setVolume(sFXSlider.value); // set the sfx volume to the inputted variable
        sFXSliderText.text = Mathf.Round(sFXSlider.value * 100).ToString(); // convert text displayed to 0 d.p and then display that value as text
    }
}
