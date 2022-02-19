using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.DeleteColor
{
    public class DeleteColorCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteCarCommandHandler : IRequestHandler<DeleteColorCommand, IResult>
        {
            IColorRepository _colorRepository;
            IMapper _mapper;

            public DeleteCarCommandHandler(IColorRepository colorRepository, IMapper mapper)
            {
                _colorRepository = colorRepository;
                _mapper = mapper;
            }

            public async Task<IResult> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
            {
                var colorToDelete = await this._colorRepository.GetAsync(c => c.Id == request.Id);
                if (colorToDelete is null) throw new BusinessException(Messages.ColorNotFound);

                await _colorRepository.DeleteAsync(colorToDelete);
                return new SuccessResult("Renk silme işlemi başarılı oldu");
            }
        }
    }
}
