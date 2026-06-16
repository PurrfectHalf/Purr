using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameStateManager
{
    public const string ReputationKey = "SavedReputation";
    public const string CustomerIndexKey = "CurrentCustomerIndex";
    public const string WrongMatchKey = "WrongMatchThisRound";
    public const string TotalCustomerCountKey = "TotalCustomerCount";
    public const string FinalScoreKey = "FinalScore";

    public const string StartSceneName = "GirisSahnesi";
    public const string ShelterSceneName = "BarinakSahnesi";
    public const string FinishSceneName = "BitisSahnesi";
    public const string GameOverSceneName = "GameOverScene";
    public const string MiniGameSceneName = "MiniGame_FlappyNot";

    public const int StartingReputation = 40;
    public const int WinReputation = 100;
    public const int WrongMatchPenalty = 10;
    public const int MiniGameFailPenalty = 15;
    public const int SuccessReward = 15;
    public const int DefaultCustomerCount = 7;

    public static int GetReputation()
    {
        return PlayerPrefs.GetInt(ReputationKey, StartingReputation);
    }

    public static void SetReputation(int value)
    {
        PlayerPrefs.SetInt(ReputationKey, value);
        PlayerPrefs.Save();
    }

    public static int GetCurrentCustomerIndex()
    {
        return PlayerPrefs.GetInt(CustomerIndexKey, 0);
    }

    public static void SetCurrentCustomerIndex(int value)
    {
        PlayerPrefs.SetInt(CustomerIndexKey, value);
        PlayerPrefs.Save();
    }

    public static void SetTotalCustomerCount(int value)
    {
        PlayerPrefs.SetInt(TotalCustomerCountKey, value);
        PlayerPrefs.Save();
    }

    public static int GetTotalCustomerCount()
    {
        return PlayerPrefs.GetInt(TotalCustomerCountKey, DefaultCustomerCount);
    }

    public static void SaveFinalScore()
    {
        PlayerPrefs.SetInt(FinalScoreKey, GetReputation());
        PlayerPrefs.Save();
    }

    public static int GetFinalScore()
    {
        return PlayerPrefs.GetInt(FinalScoreKey, GetReputation());
    }

    public static bool AdvanceCustomerAndCheckFinished(int customerCount)
    {
        int currentIndex = GetCurrentCustomerIndex();
        currentIndex++;

        PlayerPrefs.SetInt(CustomerIndexKey, currentIndex);
        PlayerPrefs.Save();

        if (customerCount > 0 && currentIndex >= customerCount)
        {
            SaveFinalScore();
            GoToFinishScene();
            return true;
        }

        return false;
    }

    public static void ResetGame()
    {
        PlayerPrefs.SetInt(ReputationKey, StartingReputation);
        PlayerPrefs.SetInt(CustomerIndexKey, 0);
        PlayerPrefs.SetInt(WrongMatchKey, 0);
        PlayerPrefs.SetInt(FinalScoreKey, StartingReputation);
        PlayerPrefs.Save();
    }

    public static void MarkWrongMatch()
    {
        PlayerPrefs.SetInt(WrongMatchKey, 1);
        PlayerPrefs.Save();
    }

    public static bool HadWrongMatchThisRound()
    {
        return PlayerPrefs.GetInt(WrongMatchKey, 0) == 1;
    }

    public static void ClearWrongMatchFlag()
    {
        PlayerPrefs.SetInt(WrongMatchKey, 0);
        PlayerPrefs.Save();
    }

    public static bool AddReputation(int amount)
    {
        int reputation = GetReputation();
        reputation += amount;

        PlayerPrefs.SetInt(ReputationKey, reputation);
        PlayerPrefs.Save();

        if (reputation < 0)
        {
            GoToGameOver();
            return true;
        }

        if (reputation >= WinReputation)
        {
            SaveFinalScore();
            GoToFinishScene();
            return true;
        }

        return false;
    }

    public static void GoToGameOver()
    {
        Time.timeScale = 1f;
        ResetGame();
        SceneManager.LoadScene(GameOverSceneName);
    }

    public static void GoToStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(StartSceneName);
    }

    public static void GoToShelter()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(ShelterSceneName);
    }

    public static void GoToFinishScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(FinishSceneName);
    }

    public static void GoToMiniGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MiniGameSceneName);
    }
}