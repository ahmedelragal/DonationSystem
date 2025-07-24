using AutoMapper;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;

        public CreateDonationHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<Guid> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Donation;
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(dto.UserId);
            
            if (user == null)
                throw new Exception("User not found.");

            var donation = new Donation
            {
                Title = dto.Title,
                Amount = dto.Amount,
                UserId = dto.UserId,
                DonatedAt = DateTime.UtcNow,
                User = user!,
                Images = new List<DonationImage>()
            };

            var imageFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            Directory.CreateDirectory(imageFolder);

            if (dto.Images != null)
            {
                foreach (var file in dto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine(imageFolder, fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream, cancellationToken);

                    donation.Images.Add(new DonationImage
                    {
                        ImageUrl = $"/uploads/{fileName}"
                    });
                }
            }

            await _unitOfWork.Repository<Donation>().AddAsync(donation);
            await _unitOfWork.CompleteAsync();

            return donation.Id;
        }
    }
}
