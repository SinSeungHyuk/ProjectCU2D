using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon.StructWrapping;

public class MonsterSpawn : MonoBehaviour
{
    private MonsterSpawnEvent monsterSpawnEvent;
    private List<WaveSpawnParameter> waveSpawnParameterList;
    private WaveSpawnParameter currentWaveSpawnParameter;
    private Vector2 spawnPosition;


    private void Awake()
    {
        monsterSpawnEvent = GetComponent<MonsterSpawnEvent>();
    }
    private void OnEnable()
    {
        monsterSpawnEvent.OnMonsterSpawn += MonsterSpawnEvent_OnMonsterSpawn;
        monsterSpawnEvent.OnWaveStart += MonsterSpawnEvent_OnWaveStart;
    }                                       
    private void OnDisable()                
    {                                       
        monsterSpawnEvent.OnMonsterSpawn -= MonsterSpawnEvent_OnMonsterSpawn;
        monsterSpawnEvent.OnWaveStart -= MonsterSpawnEvent_OnWaveStart;
    }


    private void MonsterSpawnEvent_OnMonsterSpawn(MonsterSpawnEvent @event, MonsterSpawnEventArgs args)
    {
        waveSpawnParameterList = args.stage.WaveSpawnParameter;

        monsterSpawnEvent.CallWaveStart(0); // 첫 웨이브부터 시작
    }

    private void MonsterSpawnEvent_OnWaveStart(MonsterSpawnEvent @event, int waveCnt)
    {
        // 현재 웨이브에 해당하는 웨이브 스폰 파라미터 받아오기
        currentWaveSpawnParameter = waveSpawnParameterList[waveCnt];

        if (currentWaveSpawnParameter.isBossWave == true) return; // 보스생성 추후에 구현

        WaveMonsterSpawn().Forget(); // UniTask 호출
    }

    private async UniTaskVoid WaveMonsterSpawn()
    {
        try
        {
            // 첫 1초 대기
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            float elapsedTime = 1f;

            // 60초 동안 반복
            while (elapsedTime <= Settings.waveTimer)
            {
                RandomSpawn();

                // 1초 대기
                await UniTask.Delay(TimeSpan.FromSeconds(Settings.spawnInterval));

                elapsedTime += Settings.spawnInterval; // 스폰 간격만큼 시간 더해주기
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("WaveMonsterSpawn - Spawn Canceled!!!");
        }
    }

    private void RandomSpawn()
    {
        List<MonsterSpawnParameter> monsterParameters = currentWaveSpawnParameter.monsterSpawnParameters;

        // totalRatio : 몬스터의 스폰확률 전부 더한 값
        int totalRatio = monsterParameters.Sum(x => x.Ratio);
        // batchSpawnCount : 동시에 스폰할 몬스터의 수
        int batchSpawnCount = currentWaveSpawnParameter.batchSpawnCount;

        // 총 batchSpawnCount개의 몬스터 스폰
        for (int i = 0; i < batchSpawnCount; i++)
        {
            // 난수, 현재 몬스터의 스폰확률 누적값
            int randomNumber = UnityEngine.Random.Range(0, totalRatio);
            int ratioSum = 0;

            foreach (var monsterInfo in monsterParameters)
            {
                // 현재 순회중인 몬스터가 난수에 포함되면 스폰당첨
                ratioSum += monsterInfo.Ratio;
                if (randomNumber < ratioSum)
                {                   
                    var monster = ObjectPoolManager.Instance.Get("Monster", RandomSpawnPosition(), Quaternion.identity);
                    monster.GetComponent<Monster>().InitializeEnemy(monsterInfo.monsterDetailsSO);
                    break;
                }
            }
        }
    }

    private Vector2 RandomSpawnPosition()
    {
        // new 키워드로 계속 새 vector2를 생성하는 것보다 지역변수를 두고 대입하는게 낫다고 판단
        spawnPosition.x = UnityEngine.Random.Range(-Settings.stageBoundary, Settings.stageBoundary); 
        spawnPosition.y = UnityEngine.Random.Range(-Settings.stageBoundary, Settings.stageBoundary);
        return spawnPosition;
    }
}