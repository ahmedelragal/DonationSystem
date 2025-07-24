using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Commands.DeleteDonation
{
    public class DeleteDonationCommandHandler : IRequestHandler<DeleteDonationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDonationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteDonationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Donation>();
            var donation = await repo.GetByIdAsync(request.Id);

            if (donation == null)
                throw new Exception("Donation Not Found");

            repo.Delete(donation);
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}
