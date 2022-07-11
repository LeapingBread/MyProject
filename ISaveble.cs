
public interface ISaveble
{
    void Register()
    {
        SaveLoadManager.Instance.Register(this);
    }
    GameSaveData GenerateSaveData();
    void ResoreGameData(GameSaveData data);

}
