using Application.Features.Models.Models.GetModel;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries
{
    public class GetModelListQuery2 : IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetModelListHandler : IRequestHandler<GetModelListQuery2, ModelListModel>
        {
            IModelRepository _modelRepository;
            IMapper _mapper;

            public GetModelListHandler(IModelRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;
            }

            public async Task<ModelListModel> Handle(GetModelListQuery2 request, CancellationToken cancellationToken)
            {
                var models = await _modelRepository.GetAllModels();
                var mappedModels = _mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}
