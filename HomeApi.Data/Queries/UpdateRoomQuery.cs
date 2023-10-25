namespace HomeApi.Data.Queries;

public class UpdateRoomQuery
{
    public UpdateRoomQuery(int newVoltage, int newArea, string newName)
    {
        NewVoltage = newVoltage;
        NewArea = newArea;
        NewName = newName;
    }

    public int NewArea { get; }
    public int NewVoltage { get; }
    public string NewName { get; }
    
}