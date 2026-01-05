using AutoMapper;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Project, ProjectVM>();
            CreateMap<ProjectVM, Project>();



            CreateMap<Project, Investor_ProjectVM>()

                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src))

                .ForMember(dest => dest.Investors, opt => opt.MapFrom(src => src.ProjectInvestors.Select(pi => pi.Investor)));
            CreateMap<Investor, InvestorVM>()
           // حل مشكلة الجنسية
           .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality));
            //-----------------------

            CreateMap<CoordinatorReport, CoordinatorReportVM>();
            CreateMap<CoordinatorReportVM, CoordinatorReport>();
            //------------------------------------------------
            CreateMap<Nationality, NationalityVM>();
            CreateMap<NationalityVM, Nationality>();
            //---------------------------------
            CreateMap<Employee, EmployeeVM>();
            CreateMap<EmployeeVM, Employee>();
            ////---------------------------------
            CreateMap<Advertisement, AdvertisementVM>();
            CreateMap<AdvertisementVM, Advertisement>();
            ////---------------------------------
            CreateMap<Investor, InvestorVM>();
            CreateMap<InvestorVM, Investor>();
            ////---------------------------------
            CreateMap<License, LicenseVM>();

            CreateMap<LicenseVM, License>();
            ////---------------------------------
            CreateMap<ProjectInvestor, ProjectInvestorVM>();
            CreateMap<ProjectInvestorVM, ProjectInvestor>();
            ////---------------------------------
            CreateMap<ProjectCoordinator, ProjectCoordinatorVM>();
            CreateMap<ProjectCoordinatorVM, ProjectCoordinator>();
            ////---------------------------------
        }

    }
}
