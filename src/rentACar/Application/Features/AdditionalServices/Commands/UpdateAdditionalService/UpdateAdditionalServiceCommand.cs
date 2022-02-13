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

namespace Application.Features.AdditionalServices.Commands.UpdateAdditionalService
{
    public class UpdateAdditionalServiceCommand : IRequest<UpdateAdditionalServiceDto>
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public byte ServicePoint { get; set; }
        public ServiceType ServiceType { get; set; }
        public class UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand, UpdateAdditionalServiceDto>
        {
            IAdditionalServiceRepository _additionalServiceRepository;
            IMapper _mapper;
            AdditionalServiceBusinessRules _additionalServiceBusinessRules;

            public UpdateAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper, AdditionalServiceBusinessRules additionalServiceBusinessRules)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
            }

            public async Task<UpdateAdditionalServiceDto> Handle(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken)
            {
                var addServiceToAdd = await this._additionalServiceRepository.GetAsync(ads => ads.Id == request.Id);
                addServiceToAdd = _mapper.Map(request, addServiceToAdd);

                var createdAS = await _additionalServiceRepository.UpdateAsync(addServiceToAdd);
                var aSToReturn = _mapper.Map<UpdateAdditionalServiceDto>(createdAS);
                return aSToReturn;
            }
        }
    }
}
