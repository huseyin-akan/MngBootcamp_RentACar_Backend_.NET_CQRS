using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Commands.CreateAdditionalService
{
    public class CreateAdditionalServiceCommand : IRequest<CreateAdditionalServiceDto>
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }

        public byte ServicePoint { get; set; }
        public ServiceType ServiceType { get; set; }
        public class CreateAdditionalServiceCommandHandler : IRequestHandler<CreateAdditionalServiceCommand, CreateAdditionalServiceDto>
        {
            IAdditionalServiceRepository _additionalServiceRepository;
            IMapper _mapper;
            AdditionalServiceBusinessRules _additionalServiceBusinessRules;

            public CreateAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper, AdditionalServiceBusinessRules additionalServiceBusinessRules)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
            }

            public async Task<CreateAdditionalServiceDto> Handle(CreateAdditionalServiceCommand request, CancellationToken cancellationToken)
            {
                await this._additionalServiceBusinessRules.AdditionalServiceNameCannotBeDuplicated(request.ServiceName);

                var mappedAS = _mapper.Map<AdditionalService>(request);

                var createdAS = await _additionalServiceRepository.AddAsync(mappedAS);
                var aSToReturn = _mapper.Map<CreateAdditionalServiceDto>(createdAS);
                return aSToReturn;
            }
        }
    }
}
