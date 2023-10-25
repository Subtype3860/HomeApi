using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _rooms;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository rooms, IMapper mapper)
        {
            _rooms = rooms;
            _mapper = mapper;
        }
        
        //TODO: Задание - добавить метод на получение всех существующих комнат
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _rooms.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _rooms.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromRoute]Guid id, [FromBody] EditRoomRequest request)
        {
            var room = await _rooms.GetRoomById(id);
            if(room == null)
                return StatusCode(400, $"Ошибка: Помещения с идентификатором {id} не существует.");
            var withSameName = _rooms.GetRoomByName(request.NewName);
            if(withSameName.Result != null)
                return StatusCode(400, $"Ошибка: Помещение с наименованием {request.NewName} уже подключено. Выберите другой вариант!");
            await _rooms.UpdateRoom(room, new UpdateRoomQuery(request.NewName, request.NewVoltage, request.NewArea));
            return StatusCode(200, $"Помещение обновлено! Наименование - {room.Name}, площадь - {room.Area}, напряжение - {room.Voltage}В");
        }
    }
}