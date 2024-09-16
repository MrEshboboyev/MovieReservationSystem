using MovieReservationSystem.Application.Common.Interfaces;
using MovieReservationSystem.Domain.Entities;
using MovieReservationSystem.Infrastructure.Data;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public class PaymentRepository(AppDbContext db) : Repository<Payment>(db), IPaymentRepository
    {
        private readonly AppDbContext _db = db;

        public void Update(Payment payment)
        {
            _db.Payments.Update(payment);
        }
    }
}
