﻿using System;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;
        private IRoomRepository _roomRepositoryImplementation;

        public RoomRepository (HomeApiContext context)
        {
            var a = context;
            _context = a;
        }
        
        /// <summary>
        ///  Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            var model = await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
            return model;
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        /// <summary>
        ///  Добавить новую комнату
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Обновление существующего помещения
        /// </summary>
        /// <param name="room"></param>
        /// <param name="query"></param>
        public async Task UpdateRoom(Room room, UpdateRoomQuery query)
        {
            if (!string.IsNullOrEmpty(query.NewName))
                room.Name = query.NewName;

            if (query.NewArea != default)
                room.Area = query.NewArea;

            if (query.NewVoltage != default)
                room.Voltage = query.NewVoltage;

            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }
    }
}