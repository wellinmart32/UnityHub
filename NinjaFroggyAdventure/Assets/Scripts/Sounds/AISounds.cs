using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISounds : MonoBehaviour
{
    public bool canListen = true;
    public AISoundWatcher[] aISoundWatchers1;
    public AISoundWatcher[] aISoundWatchers2;
    public int selectedSoundNumber1 = -1;
    public int selectedSoundNumber2 = -1;
    public enum SoundType { EnemySound, TrapSound };
    public enum SubSoundType { EnemySound, SpearTrap, Explosion, WaterExplosion };
    public SoundType soundType;
    public SubSoundType subSoundType;
    public float waitTime = 0.1f;
    private float soundTime;
    private int soundPosition = 0;
    private int soundLenght;

    private void Awake()
    {
        if (soundType == SoundType.EnemySound)
        {
            RespawnEnemy.Enemy enemy = GetComponentInParent<RespawnEnemy>().type;
            GameObject enemySoundController = GameObject.Find("StandarInterface").GetComponent<Initialization>().enemySoundController;
            int enemySoundPos1 = 0;
            int enemySoundPos2 = 0;
            int numbSounds1 = 0;
            int numbSounds2 = 0;

            if (enemy == RespawnEnemy.Enemy.Bat)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                enemySoundPos1 = 0;
                numbSounds1 = 2;
                soundLenght = 1;
            }
            else if (enemy == RespawnEnemy.Enemy.Bee)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                enemySoundPos1 = 1;
                numbSounds1 = 2;
                soundLenght = 1;
            }
            else if (enemy == RespawnEnemy.Enemy.BlueBird)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                aISoundWatchers2 = new AISoundWatcher[2];
                enemySoundPos1 = 2;
                enemySoundPos2 = 3;
                numbSounds1 = 2;
                numbSounds2 = 2;
                soundLenght = 2;
            }
            else if (enemy == RespawnEnemy.Enemy.Gost)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                aISoundWatchers2 = new AISoundWatcher[2];
                enemySoundPos1 = 4;
                enemySoundPos2 = 5;
                numbSounds1 = 2;
                numbSounds2 = 2;
                soundLenght = 2;
            }

            for (int i = 0; i < numbSounds1; i++)
            {
                aISoundWatchers1[i] = enemySoundController.transform.GetChild(enemySoundPos1).GetChild(i).GetComponent<AISoundWatcher>();
            }

            if (aISoundWatchers2 != null)
            {
                for (int i = 0; i < numbSounds2; i++)
                {
                    aISoundWatchers2[i] = enemySoundController.transform.GetChild(enemySoundPos2).GetChild(i).GetComponent<AISoundWatcher>();
                }
            }
        }
        else if (soundType == SoundType.TrapSound)
        {
            GameObject trapSoundController = GameObject.Find("StandarInterface").GetComponent<Initialization>().trapSoundController;
            int trapSoundPos1 = 0;
            int trapSoundPos2 = 0;
            int numbSounds1 = 0;
            int numbSounds2 = 0;

            if (subSoundType == SubSoundType.SpearTrap)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                trapSoundPos1 = 0;
                numbSounds1 = 2;
                soundLenght = 1;
            }
            else if (subSoundType == SubSoundType.Explosion)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                trapSoundPos1 = 1;
                numbSounds1 = 2;
                soundLenght = 1;
            }
            else if (subSoundType == SubSoundType.WaterExplosion)
            {
                aISoundWatchers1 = new AISoundWatcher[2];
                trapSoundPos1 = 2;
                numbSounds1 = 2;
                soundLenght = 1;
            }

            for (int i = 0; i < numbSounds1; i++)
            {
                aISoundWatchers1[i] = trapSoundController.transform.GetChild(trapSoundPos1).GetChild(i).GetComponent<AISoundWatcher>();
            }

            if (aISoundWatchers2 != null)
            {
                for (int i = 0; i < numbSounds2; i++)
                {
                    aISoundWatchers2[i] = trapSoundController.transform.GetChild(trapSoundPos2).GetChild(i).GetComponent<AISoundWatcher>();
                }
            }
        }
    }

    private void Update()
    {
        if (canListen)
        {
            Listen();
        }
        else
        {
            FreeAllUsedSounds();
        }
    }

    private void Listen()
    {
        soundTime -= Time.deltaTime;

        if (soundTime <= 0)
        {
            if (soundPosition == 0)
            {
                if (selectedSoundNumber1 >= 0)
                {
                    FreeUsedSound(soundPosition, selectedSoundNumber1);
                }
            }
            else if (soundPosition == 1)
            {
                if (selectedSoundNumber2 >= 0)
                {
                    FreeUsedSound(soundPosition, selectedSoundNumber2);
                }
            }

            PlaySound();

            if (soundLenght == soundPosition + 1)
            {
                soundPosition = 0;
            }
            else
            {
                soundPosition++;
            }
        }
    }

    private void PlaySound()
    {
        if (soundPosition == 0)
        {
            for (int i = 0; i < aISoundWatchers1.Length; i++)
            {
                AISoundWatcher aISW = aISoundWatchers1[i];

                if (!aISW.isUsing)
                {
                    SetSoundParameters(aISW, i);
                    return;
                }
            }
        }
        else if (soundPosition == 1)
        {
            for (int i = 0; i < aISoundWatchers2.Length; i++)
            {
                AISoundWatcher aISW = aISoundWatchers2[i];

                if (!aISW.isUsing)
                {
                    SetSoundParameters(aISW, i);
                    return;
                }
            }
        }
    }

    private void SetSoundParameters(AISoundWatcher aISW, int i)
    {
        if (soundPosition == 0)
        {
            selectedSoundNumber1 = i;
        }
        else if (soundPosition == 1)
        {
            selectedSoundNumber2 = i;
        }      
        
        aISW.isUsing = true;
        SetSoundParent(i);
        soundTime = aISW.clip.clip.length + waitTime;
        aISW.clip.Play();
    }

    public void FreeUsedSound(int soundPosition, int selectedSoundNumber)
    {
        if (soundPosition == 0)
        {
            aISoundWatchers1[selectedSoundNumber].FreeSound();
            this.selectedSoundNumber1 = -1;
        }
        else if (soundPosition == 1)
        {
            aISoundWatchers2[selectedSoundNumber].FreeSound();
            this.selectedSoundNumber2 = -1;
        }
    }

    public void FreeAllUsedSounds()
    {   
        for (int i = 0; i < aISoundWatchers1.Length; i++)
        {
            if (selectedSoundNumber1 == i)
            {
                FreeUsedSound(0, i);
            }
        }

        if (aISoundWatchers2 != null)
        {
            for (int i = 0; i < aISoundWatchers2.Length; i++)
            {
                if (selectedSoundNumber2 == i)
                {
                    FreeUsedSound(1, i);
                }
            }
        }

        if (soundType == SoundType.EnemySound)
        {
            if (GetComponentInParent<RespawnEnemy>())
            {
                if (GetComponentInParent<RespawnEnemy>().type == RespawnEnemy.Enemy.Bat)
                {
                    canListen = false;
                }
            }
        }

        soundTime = 0;
    }

    private void SetSoundParent(int currentSoundNumber)
    {
        if (soundPosition == 0)
        {
            if (soundType == SoundType.EnemySound || subSoundType == SubSoundType.Explosion || subSoundType == SubSoundType.WaterExplosion)
            {
                aISoundWatchers1[currentSoundNumber].transform.parent = transform.parent;
                aISoundWatchers1[currentSoundNumber].transform.position = transform.parent.position;
            }
            else if (subSoundType == SubSoundType.SpearTrap)
            {
                aISoundWatchers1[currentSoundNumber].transform.parent = transform;
                aISoundWatchers1[currentSoundNumber].transform.position = transform.position;
            }
        }
        else if (soundPosition == 1)
        {
            if (soundType == SoundType.EnemySound || subSoundType == SubSoundType.Explosion || subSoundType == SubSoundType.WaterExplosion)
            {
                aISoundWatchers2[currentSoundNumber].transform.parent = transform.parent;
                aISoundWatchers2[currentSoundNumber].transform.position = transform.parent.position;
            }
            else if (subSoundType == SubSoundType.SpearTrap)
            {
                aISoundWatchers2[currentSoundNumber].transform.parent = transform;
                aISoundWatchers2[currentSoundNumber].transform.position = transform.position;
            }
        }
    }
}
