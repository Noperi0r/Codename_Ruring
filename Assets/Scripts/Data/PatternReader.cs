using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using GoogleSheetsToUnity.ThirdPary;
using Unity.VisualScripting;

public class PatternReader : MonoBehaviour
{
    [Header("스프레드 시트 주소")] private string sheetID = "1lPHdtH27k1ShD0M3IEVtqN_YfnZEMzsLAs80--AZzYk";
    [Header("스프레드 시트 시작 행")] private string startCell = "A2";

    void Awake()
    {
        if (GameManager.MusicPattern.Count > 0) return;
        foreach (LevelMode mode in Enum.GetValues(typeof(LevelMode)))
        {
            GameManager.MusicPattern.Add(mode, new List<MusicData>());
        }
        
        // 데이터 요청을 위한 설정 - Read 딜레이가 긺(아마 비동기 처리). 무조건 로비에서 로드해야 접근 시 오류 생기지 않을 듯.
        SpreadsheetManager.Read(new GSTU_Search(sheetID, LevelMode.Easy.ToString()), Read_Easy);
        SpreadsheetManager.Read(new GSTU_Search(sheetID, LevelMode.Normal.ToString()), Read_Normal);
        SpreadsheetManager.Read(new GSTU_Search(sheetID, LevelMode.Hard.ToString()), Read_Hard);
    }
    
    void Read_Easy(GstuSpreadSheet spreadSheet)
    {
        foreach (var row in spreadSheet.rows.primaryDictionary)
        {
            MusicData data = new MusicData();
            
            if (!float.TryParse(row.Value[0].value, out data.time)) continue;

            if (!int.TryParse(row.Value[2].value, out data.spawnPoint)) continue;
            
            if (!float.TryParse(row.Value[3].value, out data.speed)) continue;

            GameManager.MusicPattern[LevelMode.Easy].Add(data);
        }
        Debug.Log($"count : {GameManager.MusicPattern[LevelMode.Easy].Count}");
    }

    // 데이터를 읽어온 후의 콜백 함수
    void Read_Normal(GstuSpreadSheet spreadSheet)
    {
        foreach (var row in spreadSheet.rows.primaryDictionary)
        {
            MusicData data = new MusicData();

            if (!float.TryParse(row.Value[0].value, out data.time)) continue;

            if (!int.TryParse(row.Value[2].value, out data.spawnPoint)) continue;

            if (!float.TryParse(row.Value[3].value, out data.speed)) continue;

            GameManager.MusicPattern[LevelMode.Normal].Add(data);
        }
        
        Debug.Log($"count : {GameManager.MusicPattern[LevelMode.Normal].Count}");
    }
    
    void Read_Hard(GstuSpreadSheet spreadSheet)
    {
        foreach (var row in spreadSheet.rows.primaryDictionary)
        {
            MusicData data = new MusicData();

            if (!float.TryParse(row.Value[0].value, out data.time)) continue;

            if (!int.TryParse(row.Value[2].value, out data.spawnPoint)) continue;

            if (!float.TryParse(row.Value[3].value, out data.speed)) continue;

            GameManager.MusicPattern[LevelMode.Hard].Add(data);
        }
        Debug.Log($"count : {GameManager.MusicPattern[LevelMode.Hard].Count}");
    }

}
