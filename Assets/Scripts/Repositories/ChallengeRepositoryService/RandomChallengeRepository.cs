using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Extensions;
using Newtonsoft.Json;
using UnityEngine;

public class RandomChallengeRepository : IRandomChallengeRepository
{
    private const string CHALLENGES_FILE_PATH = "random_challenges";
    
    private RandomChallengesData _challengesData;

    public List<string> GetRandomChallenges(int count, string language)
    {
        if (_challengesData == null)
        {
            LoadChallenges();
        }

        if (_challengesData?.RandomChallenges == null || _challengesData.RandomChallenges.Count == 0)
        {
            Debug.LogWarning("No challenges available.");
            return new List<string>();
        }

        if (count <= 0)
        {
            Debug.LogWarning("Count must be greater than zero.");
            return new List<string>();
        }

        _challengesData.RandomChallenges.Shuffle();
        var selectedCount = Mathf.Min(count, _challengesData.RandomChallenges.Count);
        var result = new List<string>(selectedCount);

        for (int i = 0; i < selectedCount; i++)
        {
            var challenge = _challengesData.RandomChallenges[i];
            string text = GetChallengeText(challenge, language);
            result.Add(text);
        }

        return result;
    }

    private void LoadChallenges()
    {
        try
        {
            TextAsset textAsset = Resources.Load<TextAsset>(CHALLENGES_FILE_PATH);
            
            if (textAsset == null)
            {
                Debug.LogError($"Failed to load challenges from Resources/{CHALLENGES_FILE_PATH}. Make sure the file is in a Resources folder.");
                return;
            }

            _challengesData = JsonConvert.DeserializeObject<RandomChallengesData>(textAsset.text);
            
            Debug.Log($"Loaded {_challengesData?.RandomChallenges?.Count ?? 0} challenges from {CHALLENGES_FILE_PATH}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load challenges: {ex}");
        }
    }

    private string GetChallengeText(RandomChallengeModel challenge, string language)
    {
        return language.ToLower() switch
        {
            "english" => challenge.English,
            "russian" => challenge.Russian,
            _ => challenge.English
        };
    }
}
