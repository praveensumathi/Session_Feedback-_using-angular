using AutoMapper;
using BAL.Interfaces;
using BAL.ViewModels;
using RepositoryLayer.Models;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ModelServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string LoginWithGetToken(UserViewModel userViewModel)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(userViewModel);

            var token = _unitOfWork.Users.Login(applicationUser);

            return token;
        }
    }
}
