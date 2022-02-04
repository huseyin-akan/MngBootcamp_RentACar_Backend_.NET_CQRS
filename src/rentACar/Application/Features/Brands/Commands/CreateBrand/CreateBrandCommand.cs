using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Mailing;
using Core.Utilities.Results;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public  class CreateBrandCommand :IRequest<DataResult<Brand>>
    {
        public string Name {get; set;}

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, DataResult<Brand>>
        {
            IBrandRepository _brandRepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;
            IMailService _mailService;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                BrandBusinessRules brandBusinessRules, IMailService mailService)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
                _mailService = mailService;
            }

            public async Task<DataResult<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //business rules
                await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);
                
                var mappedBrand =  _mapper.Map<Brand>(request);

                var createdBrand = await _brandRepository.AddAsync(mappedBrand);

                var mail = new Mail
                {
                    ToFullName="system admins",
                    ToEmail = "admins@mngkargo.com.tr",
                    Subject = "New Brand Added!!",
                    HtmlBody = "<p> Hey, check the system. A new brand is added </p>"
                };
                _mailService.SendMail(mail);

                return new SuccessDataResult<Brand>(createdBrand);
            } 
        }
    }
}
