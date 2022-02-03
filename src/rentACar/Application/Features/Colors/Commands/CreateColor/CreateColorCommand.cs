using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.CreateColor
{
    public class CreateColorCommand : IRequest<Color>
    {
        public string Name { get; set; }
        public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, Color>
        {
            IColorRepository _colorRepository;
            IMapper _mapper;
            ColorBusinessRules _colorBusinessRules;

            public CreateColorCommandHandler(IColorRepository colorRepository, IMapper mapper, ColorBusinessRules colorBusinessRules)
            {
                _colorRepository = colorRepository;
                _mapper = mapper;
                _colorBusinessRules = colorBusinessRules;
            }

            public async Task<Color> Handle(CreateColorCommand request, CancellationToken cancellationToken)
            {
                await _colorBusinessRules.ColorNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedColor = _mapper.Map<Color>(request);

                var createdColor = await _colorRepository.AddAsync(mappedColor);
                return createdColor;
            }
        }
    }
}
