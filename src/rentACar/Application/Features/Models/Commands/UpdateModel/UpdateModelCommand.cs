using Application.Features.Models.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Commands.UpdateModel
{
    public class UpdateModelCommand :IRequest<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double DailyPrice { get; set; }

        public int TransmissionId { get; set; }

        public int FuelId { get; set; }

        public int BrandId { get; set; }

        public string ImageUrl { get; set; }

        public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, Model>
        {
            IModelRepository _modelRepository;
            IMapper _mapper;
            ModelBusinessRules _modelBusinessRules;

            public UpdateModelCommandHandler(IModelRepository modelRepository,
                IMapper mapper, ModelBusinessRules modelBusinessRules)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;
                _modelBusinessRules = modelBusinessRules;
            }

            public async Task<Model> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
            {
                await _modelBusinessRules.ModelNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedModel = _mapper.Map<Model>(request);

                var updatedModel = await _modelRepository.UpdateAsync(mappedModel);
                return updatedModel;
            }
        }


    }
}
