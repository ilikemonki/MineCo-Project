using System;
using System.IO;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Runtime.Serialization.Formatters.Binary;

public class CloudSaving : MonoBehaviour
{
    public LoadData loadData;
    public BinaryFormatter formatter;
    public bool saveToCloud, deleteCloud, cloudDoneLoading, cloudDoneSaving;
    public bool cloudFileExist;
    public void Start()
    {
        formatter = new BinaryFormatter();
    }
    public void CloudSave()
    {
        if (Social.localUser.authenticated)
        {
            saveToCloud = true;
            cloudDoneSaving = false;
            OpenSavedGame("Game.dat");
        }
        else
            cloudDoneSaving = true;
    }
    public void CloudLoad()
    {
        if (Social.localUser.authenticated)
        {
            saveToCloud = false;
            cloudDoneLoading = false;
            OpenSavedGame("Game.dat");
        }
        else
        {
            cloudDoneLoading = true;
        }
    }
    public void CloudDelete()
    {
        if (Social.localUser.authenticated)
        {
            deleteCloud = true;
            OpenSavedGame("Game.dat");
        }
    }

    //Must open before saving/loading.
    public void OpenSavedGame(string filename)
    {
        if (Social.localUser.authenticated)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }
    }

    //Save Game
    void SaveGame(ISavedGameMetadata game, byte[] savedData)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedDescription("Saved game at " + DateTime.Now.ToString());
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    //Loading Game
    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (deleteCloud)
            {
                deleteCloud = false;
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.Delete(game);
                return;
            }
            if (saveToCloud)
            {
                byte[] data = SerializeData();
                SaveGame(game, data);
            }
            else
            {
                LoadGameData(game);
            }
        }
        else
        {
            Debug.Log("Error: " + status.ToString());
        }
    }
    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (data.Length == 0)    //check if there is no data, dont load.
            {
                cloudFileExist = false;
            }
            else
            {
                cloudFileExist = true;
                loadData.SetCloudData(DeserializeData(data));    //Set GameData.
            }
        }
        else
        {
            Debug.Log("Error: " + status.ToString());
        }
        cloudDoneLoading = true;
    }
    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved Successful");
        }
        else
        {
            Debug.Log("Error: " + status.ToString());
        }
        cloudDoneSaving = true;
    }

    public byte[] SerializeData()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            formatter.Serialize(ms, loadData.GetGameData());
            return ms.GetBuffer();
        }
    }

    public GameData DeserializeData(byte[] data)
    {
        using (MemoryStream ms = new MemoryStream(data))
        {
            return (GameData)formatter.Deserialize(ms);
        }
    }
}
