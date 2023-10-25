using FluentValidation;
using HomeApi.Contracts.Models.Rooms;

namespace HomeApi.Contracts.Validation;

public class EditRoomsRequestValidator : AbstractValidator<EditRoomRequest>
{
    public EditRoomsRequestValidator()
    {
        RuleFor(x => x.NewArea).NotEmpty();
        RuleFor(x => x.NewVoltage).NotEmpty();
        RuleFor(x => x.NewName).NotEmpty()
            .WithMessage($"Пожалуйста, выберите одно из следующих мест: {string.Join(", ", Values.ValidRooms)}");
    }
}