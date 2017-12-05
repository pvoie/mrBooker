using MRBooker.Data.Models.Entities;
using MRBooker.Data.Repository;
using System;

namespace MRBooker.Data.UoW
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationDbContext _dbContext;

        private IRepository<Reservation> _reservationRepository;
        private IRepository<Room> _roomRepository;
        private IRepository<Place> _placeRepository;

        private bool isDisposed = false;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Reservation> ReservationRepository
        {
            get { return _reservationRepository ?? new Repository<Reservation>(_dbContext); }
        }

        public IRepository<Room> RoomRepository
        {
            get { return _roomRepository ?? new Repository<Room>(_dbContext); }
        }

        public IRepository<Place> PlaceRepository
        {
            get { return _placeRepository ?? new Repository<Place>(_dbContext); }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
