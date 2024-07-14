using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GoogleSheetsToUnity;
using System;

public class PatternReaderLocal : MonoBehaviour
{
       void Awake()
        {
            if (GameManager.MusicPattern.Count > 0) return;
            foreach (LevelMode mode in Enum.GetValues(typeof(LevelMode)))
            {
                GameManager.MusicPattern.Add(mode, new List<MusicData>());
            }

        //LoadMusicData();

        LoadMusicData("Assets/MusicPattern_Easy.csv", LevelMode.Easy);
        LoadMusicData("Assets/MusicPattern_Normal.csv", LevelMode.Normal);
        LoadMusicData("Assets/MusicPattern_Hard.csv", LevelMode.Hard);

    }

/*    public static void LoadMusicData(string filePath, LevelMode level)
    {
        var lines = File.ReadAllLines(filePath);

        // 헤더를 스킵
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');

            // CSV 파일의 열 순서에 맞게 데이터 파싱
            float prefixTime = float.Parse(values[0]);
            float hardTime = float.Parse(values[1]);
            int hardSpawningPoint = int.Parse(values[2]);
            float spawnSpeed = float.Parse(values[3]);

            // MusicData 구조체 생성
            MusicData data = new MusicData
            {
                time = prefixTime,  // 여기서 원하는 시간 데이터를 선택
                spawnPoint = hardSpawningPoint,
                speed = spawnSpeed
            };

            // 딕셔너리에 추가
            string mode = "your_mode";  // mode는 상황에 맞게 설정

            if (!GameManager.MusicPattern.ContainsKey(level))
            {
                GameManager.MusicPattern[level] = new List<MusicData>();
            }
            GameManager.MusicPattern[level].Add(data);
        }
    }*/

    public static void LoadMusicData(string filePath, LevelMode level)
    {
        var lines = File.ReadAllLines(filePath);

        // 첫 번째 줄은 헤더이므로 스킵
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');

            MusicData data = new MusicData();

            // 값 파싱 및 유효성 검사
            if (!float.TryParse(values[0], out data.time)) continue;
            if (!int.TryParse(values[2], out data.spawnPoint)) continue;
            if (!float.TryParse(values[3], out data.speed)) continue;

            // LevelMode.Hard에 데이터 추가
            /*            if (!GameManager.MusicPattern.ContainsKey(LevelMode.Hard))
                        {
                            GameManager.MusicPattern[LevelMode.Hard] = new List<MusicData>();
                        }*/

            GameManager.MusicPattern[level].Add(data);
        }

        Debug.Log($"count : {GameManager.MusicPattern[level].Count}");
    }

}
