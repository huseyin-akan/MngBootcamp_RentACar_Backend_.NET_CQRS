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

namespace Application.Features.Colors.Commands.UpdateColor
{
    public class UpdateColorCommand : IRequest<Color>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, Color>
        {
            IColorRepository _colorRepository;
            IMapper _mapper;
            ColorBusinessRules _colorBusinessRules;

            public UpdateColorCommandHandler(IColorRepository colorRepository, IMapper mapper, ColorBusinessRules colorBusinessRules)
            {
                _colorRepository = colorRepository;
                _mapper = mapper;
                _colorBusinessRules = colorBusinessRules;
            }

            public async Task<Color> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
            {
                await _colorBusinessRules.ColorNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedColor = _mapper.Map<Color>(request);

                var updatedColor = await _colorRepository.UpdateAsync(mappedColor);
                return updatedColor;
            }
        }
    }
}
