namespace HomeApi.Contracts.Models.Rooms;

/// <summary>
/// Запрос на обновление данных помещения
/// </summary>
public class EditRoomRequest
{
    public string NewName { get; set; }
    public int NewArea { get; set; }
    public int NewVoltage { get; set; }
}