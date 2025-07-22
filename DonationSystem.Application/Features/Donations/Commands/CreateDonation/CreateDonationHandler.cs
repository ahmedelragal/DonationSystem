using AutoMapper;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Commands.CreateDonation
{
    public class CreateDonationHandler : IRequestHandler<CreateDonationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDonationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var donation = _mapper.Map<Donation>(request.Donation);
            await _unitOfWork.Repository<Donation>().AddAsync(donation);
            await _unitOfWork.CompleteAsync();
            return donation.Id;
        }
    }
}
