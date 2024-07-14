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

        // ����� ��ŵ
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');

            // CSV ������ �� ������ �°� ������ �Ľ�
            float prefixTime = float.Parse(values[0]);
            float hardTime = float.Parse(values[1]);
            int hardSpawningPoint = int.Parse(values[2]);
            float spawnSpeed = float.Parse(values[3]);

            // MusicData ����ü ����
            MusicData data = new MusicData
            {
                time = prefixTime,  // ���⼭ ���ϴ� �ð� �����͸� ����
                spawnPoint = hardSpawningPoint,
                speed = spawnSpeed
            };

            // ��ųʸ��� �߰�
            string mode = "your_mode";  // mode�� ��Ȳ�� �°� ����

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

        // ù ��° ���� ����̹Ƿ� ��ŵ
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');

            MusicData data = new MusicData();

            // �� �Ľ� �� ��ȿ�� �˻�
            if (!float.TryParse(values[0], out data.time)) continue;
            if (!int.TryParse(values[2], out data.spawnPoint)) continue;
            if (!float.TryParse(values[3], out data.speed)) continue;

            // LevelMode.Hard�� ������ �߰�
            /*            if (!GameManager.MusicPattern.ContainsKey(LevelMode.Hard))
                        {
                            GameManager.MusicPattern[LevelMode.Hard] = new List<MusicData>();
                        }*/

            GameManager.MusicPattern[level].Add(data);
        }

        Debug.Log($"count : {GameManager.MusicPattern[level].Count}");
    }

}
